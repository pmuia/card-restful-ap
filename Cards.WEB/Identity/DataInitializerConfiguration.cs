using Core.Domain.Utils;
using Core.Management.Common;
using Microsoft.AspNetCore.Identity;

namespace Cards.WEB.Identity
{
    public static class DataInitializerConfiguration
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync(DefaultSettings.Instance.StandardUser).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = DefaultSettings.Instance.StandardFirstName;
                user.OtherNames = DefaultSettings.Instance.StandardLastName;
                user.UserName = DefaultSettings.Instance.StandardUser;
                user.Email = DefaultSettings.Instance.StandardUserEmail;
                user.EmailConfirmed = true;
                user.CreatedDate = DateTime.Now;
                user.RecordStatus = (byte)RecordStatus.Approved;
                user.IsEnabled = true;
                user.IsExternalUser = false;
                user.LastPasswordChangedDate = DateTime.Now;
                user.PhoneNumber = DefaultSettings.Instance.OptimaERPMobileNumber;
                user.PhoneNumberConfirmed = true;
                user.LockoutEnabled = false;

                IdentityResult result = userManager.CreateAsync
                (user, DefaultSettings.Instance.StandardPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        WellKnownUserRoles.StandardUser.ToString()).Wait();
                }
            }


            if (userManager.FindByNameAsync(DefaultSettings.Instance.RootUser).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = DefaultSettings.Instance.FirstName;
                user.OtherNames = DefaultSettings.Instance.LastName;
                user.UserName = DefaultSettings.Instance.RootUser;
                user.Email = DefaultSettings.Instance.RootEmail;
                user.EmailConfirmed = true;
                user.CreatedDate = DateTime.Now;
                user.RecordStatus = (byte)RecordStatus.Approved;
                user.IsEnabled = true;
                user.IsExternalUser = false;
                user.LastPasswordChangedDate = DateTime.Now;
                user.PhoneNumber = DefaultSettings.Instance.OptimaERPMobileNumber;
                user.PhoneNumberConfirmed = true;
                user.LockoutEnabled = false;


                IdentityResult result = userManager.CreateAsync
                (user, DefaultSettings.Instance.RootPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        WellKnownUserRoles.SuperAdministrator.ToString()).Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(WellKnownUserRoles.StandardUser.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = WellKnownUserRoles.StandardUser.ToString();
                role.Description = DefaultSettings.Instance.StandardUserDescription;
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(WellKnownUserRoles.SuperAdministrator.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = WellKnownUserRoles.SuperAdministrator.ToString();
                role.Description = DefaultSettings.Instance.SuperUserDescription;
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(WellKnownUserRoles.Administrator.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = WellKnownUserRoles.Administrator.ToString();
                role.Description = DefaultSettings.Instance.AdministratorUserDescription;
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(WellKnownUserRoles.APIAccount.ToString()).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = WellKnownUserRoles.APIAccount.ToString();
                role.Description = DefaultSettings.Instance.APIUserDescription;
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }

    public static class ApplicationDomainName
    {
        public static string CurrentDomainName { get; set; }
    }
}
