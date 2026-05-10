using Microsoft.AspNetCore.Mvc;
using GerenciadorEnderecos.Data;

namespace GerenciadorEnderecos.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string nomeUsuario, string senha)
        {
            var user = _context.Usuarios
                .FirstOrDefault(x => x.NomeUsuario == nomeUsuario && x.Senha == senha);

            if (user != null)
            {
                HttpContext.Session.SetString("UsuarioLogado", user.Nome);
                HttpContext.Session.SetInt32("UsuarioId", user.Id);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuário ou senha inválidos";
            return View();
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}