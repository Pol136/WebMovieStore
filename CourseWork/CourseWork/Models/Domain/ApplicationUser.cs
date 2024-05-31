using Microsoft.AspNetCore.Identity;

namespace CourseWork.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}