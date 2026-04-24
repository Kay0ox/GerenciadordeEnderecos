using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MeuSiteEmMVC.ViewModels;

namespace MeuSiteEmMVC.Controllers
{
    public class ContaController : Controller
    {
        private readonly AppDbContext _context;

        public ContaController(AppDbContext context)
        {
            _context = context;
        }
        // get para exibir a tela de login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Login == model.Login && u.Senha == model.Senha);
                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Login),
                       new Claim("UsuarioId", usuario.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Endereco");
                }
                ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
            }
            return View(model);
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
   

    // GET: /Conta/Registro
public IActionResult Registro()
        {
            return View();
        }

        // POST: /Conta/Registro
        [HttpPost]
        public IActionResult Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginExiste = _context.Usuarios.Any(u => u.Login == model.Login);
            if (loginExiste)
            {
                ModelState.AddModelError("Login", "Este usuário já existe!");
                return View(model);
            }

            var usuario = new Usuario
            {
                Nome = model.Nome,
                Login = model.Login,
                Senha = model.Senha
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
