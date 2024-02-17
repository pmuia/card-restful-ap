using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Management.Common;

namespace Cards.WEB.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, IdentityUserLogin<string>, ApplicationRoleClaim, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetUsers");
            modelBuilder.Entity<ApplicationRole>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetRoles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetUserRoles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetUserLogins");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable($"{DefaultSettings.Instance.TablePrefix}AspNetUserTokens");

        }
    }
}
