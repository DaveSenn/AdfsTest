using System.Diagnostics;
using AdfsMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdfsMvc.Controllers;

public class HomeController( ILogger<HomeController> logger ) : Controller
{
    [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
    public IActionResult Error() =>
        View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );

    [Authorize]
    public IActionResult Index()
    {
        logger.LogInformation( "Index.... " );
        return View();
    }

    public IActionResult Privacy() =>
        View();
}