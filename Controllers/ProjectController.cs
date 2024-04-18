using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dr_JonesBugTracker.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Dr_JonesBugTracker.Controllers;

// [SessionCheck]
public class ProjectController : Controller
{
    private readonly ILogger<ProjectController> _logger;

    private MyContext _context;

    public ProjectController(ILogger<ProjectController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

    [HttpGet("projects/home")]
    public IActionResult HomePage()
    {
        return View();
    }

    [HttpGet("projects/dashboard")]
    public IActionResult Dashboard()
    {
        return View("Dashboard");
    }

    [HttpPost("projects/create")]
    public IActionResult CreateProject(Project newProject)
    {
        if (!ModelState.IsValid)
        {
            var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
            Console.WriteLine(message);

            ViewData["openForm"] = true;

            return Dashboard();
        }
        else
        {
            newProject.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newProject);
            _context.SaveChanges();

            return RedirectToAction("Dashboard", new{projectId = newProject.ProjectId});
        }
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
