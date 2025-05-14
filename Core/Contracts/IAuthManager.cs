using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> SignUp(SignUpModel userModel);
        Task<AuthResponseModel> SignIn(LogInModel logInModel);
        Task<UserTokenModel> GetUserInfoFromToken(string token);
        Task<GetUserModel> IsValidUser(UserTokenModel user);
    }
}
