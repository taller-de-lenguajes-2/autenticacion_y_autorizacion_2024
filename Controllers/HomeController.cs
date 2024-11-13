using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ejemploAuth.Models;

namespace ejemploAuth.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthenticationService authenticationService;

    public HomeController(ILogger<HomeController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        this.authenticationService = authenticationService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        authenticationService.Login("admin","password123");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [AccessLevelAuthorize("Admin")] 
    public IActionResult SoloAdmin()
    {
        return View();
    }

    [AccessLevelAuthorize("User")] 
    public IActionResult Solo()
    {
        return View();
    }

    [AccessLevelAuthorize("Admin", "Editor", "Manager")]
    public IActionResult AnotherPage()
    {
        // Esta acción solo será accesible para usuarios con uno de los niveles de acceso especificados
        return View();
    }
}
