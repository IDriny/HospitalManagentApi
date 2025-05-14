using System.ComponentModel.DataAnnotations;

namespace HospitalManagentApi.Models.User
{
    public class GetUserModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
