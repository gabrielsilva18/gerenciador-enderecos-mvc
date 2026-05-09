using Microsoft.AspNetCore.Mvc;

namespace GerenciadorEnderecos.Controllers
{
    public class RegistroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}