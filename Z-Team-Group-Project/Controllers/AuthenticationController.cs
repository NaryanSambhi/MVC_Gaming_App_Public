using Z_Team_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z_Team_Group_Project.Data;

namespace Z_Team_Group_Project.Controllers
{
    public class AuthenticationController : Controller
    {

        private readonly Z_Team_Group_ProjectContext _context;

        // Here the GET version of the Index action
        [HttpGet()]
        public IActionResult Index()
        {

            //using sessions check if user is logged in or not
            var username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrEmpty(username))
            {
                return View(new AuthenticationViewModel { Username = username, Result = "Already logged in!" });
            }

            return View(new AuthenticationViewModel());
        }

        // POST version of the Index action
        [HttpPost()]
        public async Task<IActionResult> Index(AuthenticationViewModel authenticationViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check credentials  for login
                var result = await authenticationViewModel.GetResult(_context);

                if (result == "Logged In!")
                {
                    // Store the username in the session if login is successful
                    HttpContext.Session.SetString("Username", authenticationViewModel.Username);

                    // Store the password in the session if login is successful
                    HttpContext.Session.SetString("Password", authenticationViewModel.Password);

                    authenticationViewModel.Result = result;
                }
                return View(authenticationViewModel);

            }
            else
            {
                return View(authenticationViewModel);
            }
        }

        // logout
        public IActionResult Logout()
        {
            // Clear the session data
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public AuthenticationController(Z_Team_Group_ProjectContext context)
        {
            _context = context;
        }
    }
}
