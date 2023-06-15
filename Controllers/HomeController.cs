using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

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
        List<Dish> AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        return View(AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View();
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish (Dish newDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("NewDish");
        }
    }

    [HttpGet("dishes/{id}")]
    public IActionResult ShowDish(int id)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View(OneDish);
    }

    [HttpGet("dishes/{id}/edit")]
    public IActionResult EditDish(int id)
    {
        Dish? DishToEdit = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View(DishToEdit);
    }

    [HttpPost ("dishes/{id}/update")]
    public IActionResult UpdateDish(int id, Dish UpdatedDish)
    {
        Dish? DishToUpdate = _context.Dishes.FirstOrDefault(d => d.DishId==id);
        if(DishToUpdate == null)
        {
            return RedirectToAction("Index");
        }

        if(ModelState.IsValid)
        {
            DishToUpdate.Name = UpdatedDish.Name;
            DishToUpdate.Chef = UpdatedDish.Chef;
            DishToUpdate.Calories = UpdatedDish.Calories;
            DishToUpdate.Tastiness = UpdatedDish.Tastiness;
            DishToUpdate.Description = UpdatedDish.Description;
            _context.SaveChanges();
            return Redirect($"/dishes/{id}");
        }

        else
        {
            return View("EditDish", DishToUpdate);
        }
    }

    [HttpPost("dishes/{id}/destroy")]
    public IActionResult DestroyDish(int id)
    {
        Dish? DishToDestroy = _context.Dishes.SingleOrDefault(d => d.DishId == id);
        _context.Dishes.Remove(DishToDestroy);
        _context.SaveChanges();
        return RedirectToAction("Index");
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
