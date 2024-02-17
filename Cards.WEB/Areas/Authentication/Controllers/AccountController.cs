using Core.Domain.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cards.WEB.Controllers;
using Cards.WEB.Extensions;
using Cards.WEB.Identity;
using Cards.WEB.Models.Authentication;
using Cards.WEB.Models.JQueryDataTables;
using System.Security.Claims;
using static Cards.WEB.Models.AccountViewModels;

namespace Cards.WEB.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsersList(JQueryDataTablesModel jQueryDataTablesModel)
        {
            var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var userRole = await _userManager.GetRolesAsync(applicationUser);

            int totalRecordCount = 0;

            int searchRecordCount = 0;

            //var sortAscending = jQueryDataTablesModel.sSortDir_.First() == "asc" ? true : false;

            //var sortedColumns = (from s in jQueryDataTablesModel.GetSortedColumns() select s.PropertyName).ToList();            

            var administrativeUsers = _userManager.Users;

            if (administrativeUsers != null && administrativeUsers.Any())
            {
                totalRecordCount = administrativeUsers.Count();

                searchRecordCount = !string.IsNullOrWhiteSpace(jQueryDataTablesModel.sSearch)
                    ? administrativeUsers.Count()
                    : totalRecordCount;

                return this.DataTablesJson(items: administrativeUsers, totalRecords: totalRecordCount,
                    totalDisplayRecords: searchRecordCount, sEcho: jQueryDataTablesModel.sEcho);
            }
            else
                return this.DataTablesJson(items: new List<ApplicationUserDto> { }, totalRecords: totalRecordCount,
                    totalDisplayRecords: searchRecordCount, sEcho: jQueryDataTablesModel.sEcho);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // We do not want to use any existing identity information
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Store the originating URL so we can attach it to a form field
            var viewModel = new AccountLoginModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginModel viewModel, string returnUrl)
        {
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
                return View(viewModel);

            // Require the user to have a confirmed email before they can log on.
            var user = await _userManager.FindByNameAsync(viewModel.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");

                return View(viewModel);
            }

            if (user.RecordStatus != (byte)RecordStatus.Approved || !user.IsEnabled)
            {
                ModelState.AddModelError("", "Sorry, your status does not allow you to login");

                return View(viewModel);
            }

            if (user != null)
            {
                //check if user is an api user
                var userRole = await _userManager.GetRolesAsync(user);

                if (userRole.Any(x => x.Equals(EnumHelper.GetDescription(WellKnownUserRoles.APIAccount))))
                {
                    ModelState.AddModelError("", "Sorry, your role does not allow you to login");

                    return View(viewModel);
                }
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true

                var signInStatus = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, true);

                if (signInStatus.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("UserName", user.UserName)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
                    claimsIdentity = new ClaimsIdentity(claims, "Identity");

                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity[] { claimsIdentity });

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = new DateTimeOffset?(DateTime.UtcNow.AddMinutes(15))
                    });

                    return RedirectToAction("Index", "Dashboard", new { area = "" });

                }
                else if (signInStatus.IsLockedOut)
                {
                    //_logger.LogInformation("User logged in.");
                    return View("Lockout");
                }
                else if (signInStatus.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Contact System administrator!");
                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(viewModel);
                }
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegistrationModel accountRegistrationModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var applicationUser = new ApplicationUser
            {
                FirstName = accountRegistrationModel.FirstName,
                OtherNames = accountRegistrationModel.OtherNames,
                Email = accountRegistrationModel.Email,
                UserName = accountRegistrationModel.Username,
                PhoneNumber = accountRegistrationModel.MobileNumber,
                CreatedDate = DateTime.Now,
                IsEnabled = true,
                IsExternalUser = false,
                RecordStatus = (int)RecordStatus.Approved,
                LastPasswordChangedDate = DateTime.Now,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedBy = user.UserName
            };

            var result = await _userManager.CreateAsync(applicationUser, accountRegistrationModel.Password);

            if (result.Succeeded)
            {
                var assignRoles = await _userManager.AddToRoleAsync(applicationUser, WellKnownUserRoles.SuperAdministrator.ToString());

                if (assignRoles.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    TempData["Success"] = "Account created successfully. Kindly Login to with your credentials";

                    return RedirectToAction("index", "Account");
                }
                else
                {
                    TempData["Error"] = "Account Creation failed";

                    return RedirectToAction("index", "Account");
                }

            }
            else
            {
                TempData["Error"] = "Account Creation failed";

                return RedirectToAction("Register", "Account");
            }
        }


        #region helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //_logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account", new { area = "Authentication" });
        }

        #endregion
    }
}
