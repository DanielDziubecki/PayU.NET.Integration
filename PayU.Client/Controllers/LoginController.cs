using System.Threading.Tasks;
using System.Web.Mvc;
using PayU.Client.Services;

namespace PayU.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
           var token = await loginService.GetToken(loginDto);

            if (!string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Product");


            return View("LoginFailed");
        }
    }
}