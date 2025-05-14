using System.ComponentModel.DataAnnotations;

namespace HospitalManagentApi.Models.User
{
    public class UserTokenModel
    {
        public string UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
