using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Azure.Core;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
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

            var token = await GenerateToken();
            return new AuthResponseModel
            {
                UserId = _user.Id,
                Email = _user.Email,
                Token = token
            };
        }

        public async Task<GetUserModel> GetUserInfoFromToken(AuthResponseModel request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = tokenHandler.ReadJwtToken(request.Token);
            var userId = tokenContent.Claims.ToList().FirstOrDefault(c => c.Type == "UserId").Value;
            _user = await _userManager.FindByIdAsync(userId);
            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var userInfo = _mapper.Map<GetUserModel>(_user);
            return userInfo;

        }

        private async Task<string> GenerateToken()
        {
            //Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"])
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("UserId",_user.Id)
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims:claims,
                expires:DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtSettings:DurationInDays"])),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
