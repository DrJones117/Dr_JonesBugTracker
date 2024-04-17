using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dr_JonesBugTracker.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Dr_JonesBugTracker.Controllers;

public class ProjectController : Controller
{
    private readonly ILogger<ProjectController> _logger;

    private MyContext _context;

    public ProjectController(ILogger<ProjectController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
