using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HospitalManagentApi.Persistence.Repository
{
    public class AuthManager:IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(IMapper mapper,UserManager<ApiUser> userManager,IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IEnumerable<IdentityError>> SignUp(SignUpModel userModel)
        {
            _user = _mapper.Map<ApiUser>(userModel);
            _user.UserName = userModel.Email;
            var result = await _userManager.CreateAsync(_user,userModel.Password);

            return result.Errors;
        }

        public async Task<AuthResponseModel> SignIn(LogInModel logInModel)
        {
            _user = await _userManager.FindByEmailAsync(logInModel.Email);
            var isValidUser = await _userManager.CheckPasswordAsync(_user, logInModel.Password);
            if ( _user == null || isValidUser==false)
            {
                return null;
            }

            var token = await GenerateToken(_user);
            return new AuthResponseModel
            {
                UserId = _user.Id,
                Email = _user.Email,
                Token = token
            };
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.FirstName+" "+user.LastName),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId",user.Id)
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims:claims,
                expires:DateTime.Now.AddHours(Convert.ToInt32(_configuration["JwtSettings:DurationInDays"])),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserTokenModel> GetUserInfoFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidAudience = _configuration["JwtSettings:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]))
            };
            var principal = handler.ValidateTokenAsync(token, validationParams);
            if (!principal.Result.IsValid)
            {
                return null;
            }
            var jsonToken = handler.ReadJwtToken(token);
            var tokens = jsonToken as JwtSecurityToken;
            var user = new UserTokenModel
            {
                UserId = tokens.Claims.First(c => c.Type == "UserId").Value,
                Email = tokens.Claims.First(c => c.Type == "email").Value
            };
            return user;
        }

        public async Task<GetUserModel> IsValidUser(UserTokenModel userToken)
        {

            _user = await _userManager.FindByIdAsync(userToken.UserId);
            //var validUser =await _userManager.FindByIdAsync(userToken.UserId);
            if (_user.Email != userToken.Email || _user.Id != userToken.UserId)
            {
                return null;
            }
            var user = _mapper.Map<GetUserModel>(_user);
            return user;
        }
        
    }
}
