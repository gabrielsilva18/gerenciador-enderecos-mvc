using Microsoft.AspNetCore.Mvc;

namespace GerenciadorEnderecos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}