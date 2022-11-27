using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendaDeLanches.ViewModels;

namespace VendaDeLanches.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        // vai postar o formulario para o usuario preencher c a senha

        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                // passando usuario, senha, informa que nao vai persistir o cookie qnd a sessao fechar
                // se o login falhar, n bloquear usuario

                if(result.Succeeded)
                {
                    // ver se o login foi feito com sucesso
                    if(string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return Redirect(loginVM.ReturnUrl);
                }

            }

            ModelState.AddModelError("", "Falha ao realizar o login!");
            return View(loginVM);

        }

       public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if(result.Succeeded)
                {
                    // await _signInManager.SignInAsync(user, isPersistent: false);

                    await _userManager.AddToRoleAsync(user, "Member"); // todo usuario registrado vai pro perfil member
                    return RedirectToAction("Login", "Account");
                } else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuarío.");
                }

            }
            return View(registroVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear(); // limpar os valores da session
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
