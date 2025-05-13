using Microsoft.AspNetCore.Identity;

namespace HospitalManagentApi.Core.Domain
{
    public class ApiUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
