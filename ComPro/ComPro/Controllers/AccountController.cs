using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ComPro.Models;
using ComPro.Interfaces;
using System.Collections.Generic;
using System.Web.Security;
using static ComPro.Models.Enums;

namespace ComPro.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private IUserProfile _UserAccountProfile;
        private readonly IUserProfile _UserProfile;
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AccountController()
        {
            _UserProfile = new UserProfileManager();
        }


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _UserAccountProfile = new UserProfileManager(userManager);
           // _UserProfile = userProfile;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //if ((_UserProfile.GetUserRole(model.Email) == UserRole.NewUser.ToString()) || !(_UserProfile.CheckEmailvarification(model.Email)))
            if ((_UserProfile.GetUserRole(model.Email) == UserRole.NewUser.ToString()) )

            {
                return RedirectToAction("New_Registration");
            }

            

                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
           else
            { 
                    if (!ModelState.IsValid)
                    {
                    return View(model);
                     }

                        var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                            switch (result)
                            {
                                case SignInStatus.Success:
                                    return RedirectToLocal(returnUrl, model.Email);
                                case SignInStatus.LockedOut:
                                    return View("Lockout");
                                case SignInStatus.RequiresVerification:
                                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                                case SignInStatus.Failure:
                                default:
                                    ModelState.AddModelError("", "Invalid login attempt.");
                                    return View(model);
                            }


                    }


        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MyPage", "UserProfile");
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(InternalRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                //TODO: have to fix for password.
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    //Send an email with this link

                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    // Note : _UserAccountProfile doesnt work as it has a constructor parameter
                    //_UserAccountProfile.AddationalInfo(model);
                    //_UserAccountProfile.SetUserRole(model.Email);

                    _UserProfile.AddationalInfo(model);
                    _UserProfile.SetUserRole(model.Email);

                    Email_Service_Model email=new Email_Service_Model();
                    email.ToEmail= System.Configuration.ConfigurationManager.AppSettings["BccEmail"];
                    email.EmailSubject = $"{model.Name} signed up.";
                    email.EMailBody = $"Name: {model.Name}. Email:{model.Email}";
                    
                    var emailmanager=new UtilityManager();
                    emailmanager.SendEmail(email);

                    return RedirectToAction("New_Registration", new { Id=user.Id});

                }
                logger.Error($"Error signing up. {model.Email}");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult New_Registration(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var userData = _UserProfile.UserDetail(id);
                if (userData != null)
                {
                    if (userData.ApprovalDate == null)
                    {
                        TempData["NewLogin"] = Helpers.Constants.NewRegistrationMessage.ToString();

                        return View(userData);
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
            return RedirectToAction("Login");
            
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordWarning");
                }

               
                 string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
               _UserProfile.ForgotPassword(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        public ActionResult AccountSettings()
        {
            ViewBag.type = _UserProfile.Accounttype();
            return View();
        }

        public ActionResult ResetMyPassword()
        {

            return PartialView("_MyPasswordPartialView");
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetMyPassword(ResetMyPasswordModel model)
        {
            if (ModelState.IsValid)
            {


              
                var UserId = User.Identity.GetUserId();
               string code = await UserManager.GeneratePasswordResetTokenAsync(UserId);
               //bool  aa = UserManager.CheckPasswordAsync(UserId, model.OldPassword);
                //var user = UserManager.Find(UserId, model.OldPassword);
                //if(user!=null)
                {
                var result = await UserManager.ResetPasswordAsync(UserId, code, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Content(true.ToString());

                    }
                }
               

            }


            return Content(false.ToString());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }
        
        
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
           
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            //TempData["UserRole"] = "User";
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
           var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            //string userinfo = loginInfo.Login.ProviderKey;

            /*
            string UserEmail = _UserProfile.CheckExternalUser(loginInfo.Login.ProviderKey);
            if (UserEmail!= null)
            {
                if ((_UserProfile.GetUserRole(UserEmail) == UserRole.NewUser.ToString()) || !(_UserProfile.CheckEmailvarification(UserEmail)))
                {
                    TempData["NewLogin"] = Helpers.Constants.NewRegistrationMessage.ToString();
                    return RedirectToAction("Login");
                }
            }

            
            */
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            
            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                     return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                   
                    return View("ExternalLoginConfirmation", new UserInfo { Email = loginInfo.Email,Name =loginInfo.ExternalIdentity.Name});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(UserInfo model, string returnUrl)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    /*
                    UserInfo uInfo = new UserInfo
                    {
                        Name = model.Name,
                        Email = model.Email,
                        CurrentJobTitle = model.CurrentJobTitle,
                        Gender = model.Gender,
                        UserId=user.Id
                    };


                    _db.UserInfo.Add(uInfo);
                    _db.SaveChanges();

                    */
                    model.UserId = user.Id;
                    _UserProfile.ExternalAddationalInfo(model);


                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //return RedirectToAction("Login");
                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl, string email)
        {
           
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

                 

        private ActionResult RedirectToLocal(string returnUrl)
        {
            TempData["NewLogin"] = Helpers.Constants.NewRegistrationMessage.ToString();
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        

        
        


    }
}