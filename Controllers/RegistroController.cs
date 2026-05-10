using GerenciadorEnderecos.Data;
using GerenciadorEnderecos.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorEnderecos.Controllers
{
    public class RegistroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistroController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            // evita duplicado
            var existe = _context.Usuarios
                .Any(x => x.NomeUsuario == usuario.NomeUsuario);

            if (existe)
            {
                ModelState.AddModelError("", "Usuário já existe.");
                return View(usuario);
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
} 