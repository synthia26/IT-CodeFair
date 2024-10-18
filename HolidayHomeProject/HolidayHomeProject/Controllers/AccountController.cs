using System.Security.Claims;
using HolidayHomeProject.DataAccessLayer.Context;
using HolidayHomeProject.DataAccessLayer.Dto;
using HolidayHomeProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HolidayHomeProject.Controllers
{
    public class AccountController : Controller
	{
		private readonly AppDbContext _context;

		public	AccountController(AppDbContext context)
		{
			_context = context;
		}

		public	IActionResult Index()
		{
			return View(_context.UserAccounts.ToList());
		}

		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Registration(RegistrationViewModel model)
		{
			if (ModelState.IsValid)
			{
				UserAccount account = new UserAccount();
				account.Email = model.Email;
				account.FirstName = model.FirstName;
				account.LastName = model.LastName;
				account.Password = model.Password;
				account.IsHost = model.IsHost;
				try
				{
					_context.UserAccounts.Add(account);
					_context.SaveChanges();

					ModelState.Clear();
					ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login.";

				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError("", "Please enter valid Email or password!");
					return View(model);
				}
				return View();
			}

			return View(model);
		}

		public IActionResult Login()
        {
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			/*           if (ModelState.IsValid)
					   {
						   var user = await _userManager.FindByNameAsync(model.Email);

						   if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
						   {
							   // Sign in the user
							   var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

							   if (result.Succeeded)
							   {
								   // Add IsHost claim after login
								   var claims = new List<Claim>
						   {
							   new Claim("IsHost", user.IsHost.ToString())
						   };
								   var claimsIdentity = new ClaimsIdentity(claims, "CustomClaim");

								   await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);

								   return RedirectToAction("Index", "Home");
							   }
						   }

						   ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					   }*/
			if (ModelState.IsValid)
			{
				var user = _context.UserAccounts.Where(x => x.Email == model.Email
										&& x.Password == model.Password).FirstOrDefault();
				if (user != null)
				{
					// Store IsHost value in session after successful login
					TempData["IsHost"] = user.IsHost;
					// HttpContext.Session.SetString("IsHost", user.IsHost.ToString());

                    // Success, Create Cookie
                    var claims = new List<Claim>
								{
									new Claim(ClaimTypes.Name, user.Email),
									new Claim("Name", user.FirstName),
                                    new Claim("UserId", user.Id.ToString()),
									user.IsHost ? new Claim(ClaimTypes.Role, "Host") : new Claim(ClaimTypes.Role, "User"),
								};


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // 3. Create the claims principal
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false, // Set IsPersistent based on "remember me" checkbox
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1) // Optional: Set expiration time
                    };

                    // Sign the user in
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Email or Password is not correct!");
				}
			}
			return View();
		}


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
			// Clear the session on logout
			TempData.Clear();
            HttpContext.Session.Clear();
            // Sign the user out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

		[Authorize]
		public IActionResult SecurePage()
        {
			ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
