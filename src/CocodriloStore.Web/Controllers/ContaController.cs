using CocodriloStore.Web.Models;
using CocodriloStore.Web.Services;
using CocodriloStore.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CocodriloStore.Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly VendedorService _vendedorService;

        public ContaController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            VendedorService vendedorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _vendedorService = vendedorService;
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser
            {
                UserName = model.Email, // login será feito com o email
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _vendedorService.CriarVendedorAsync(user, model.Nome);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

// 🧪 LOGAR OS ERROS NO CONSOLE
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"ERRO AO REGISTRAR: {error.Code} - {error.Description}");
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Redirect(returnUrl ?? "/");
                }
            }

            // Mensagem clara de erro
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}