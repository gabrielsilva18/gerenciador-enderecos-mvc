using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorEnderecos.Models;

namespace GerenciadorEnderecos.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Usuario = HttpContext.Session.GetString("UsuarioLogado");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}