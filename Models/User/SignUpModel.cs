using System.ComponentModel.DataAnnotations;

namespace HospitalManagentApi.Models.User
{
    public class SignUpModel:LogInModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
