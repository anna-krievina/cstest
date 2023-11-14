using CSTest.Db.Models;
using CSTest.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace CSTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public ActionResult Login()
        {
            UserModel model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                // I know the proper way would be something like this
                //var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

                using (var context = new CstestdbContext())
                {
                    var entity = context.Users.Where(i => i.Username == model.UserName && i.Password == model.Password).FirstOrDefault();
                    if (entity == null)
                    {
                        ModelState.AddModelError("UserName", "The username or password is incorrect");
                    }
                    else
                    {
                        return RedirectToAction("List", "Products");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // I know the proper way would be something like this
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
