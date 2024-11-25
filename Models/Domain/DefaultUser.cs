using Microsoft.AspNetCore.Identity;

namespace OnlineBookStore.Models.Domain
{
    public class DefaultUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
