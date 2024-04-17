using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dr_JonesBugTracker.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;


namespace Dr_JonesBugTracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;

        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if (!ModelState.IsValid)
        {
            var message = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
            Console.WriteLine(message);

            return View("Index");
        }
        else
        {
            PasswordHasher<User> Hasher = new();

            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

            HttpContext.Session.SetInt32("UserId", newUser.UserId);

            _context.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("HomePage", "Project");
        }
    }

    [HttpPost("users/login")]
    public IActionResult LogIn(LoginUser user)
    {   
        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == user.LogEmail);

            if (userInDb == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                return View("Index");
            }

            PasswordHasher<LoginUser> hasher = new(); 

            var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.LogPassword);

            if (result == 0)
            {
                ModelState.AddModelError("LogPassword", "Invalid Email/Password");
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("HomePage", "Project");
            }
        }

        return View("Index");
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
