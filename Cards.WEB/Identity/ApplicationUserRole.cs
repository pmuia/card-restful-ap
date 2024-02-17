using Microsoft.AspNetCore.Identity;

namespace Cards.WEB.Identity
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUserRole() : base()
        {
        }

        public virtual ApplicationRole Role { get; set; }
    }
}
