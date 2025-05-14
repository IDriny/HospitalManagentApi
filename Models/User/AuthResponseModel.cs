using System.ComponentModel.DataAnnotations;

namespace HospitalManagentApi.Models.User
{
    public class AuthResponseModel
    {
        public string UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
