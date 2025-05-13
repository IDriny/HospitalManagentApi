using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagentApi.Persistence.Repository
{
    public class AuthManager:IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private ApiUser _user;

        public AuthManager(IMapper mapper,UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IEnumerable<IdentityError>> SignUp(SignUpModel userModel)
        {
            _user = _mapper.Map<ApiUser>(userModel);
            _user.UserName = userModel.Email;
            var result = await _userManager.CreateAsync(_user,userModel.Password);

            return result.Errors;
        }
    }
}
