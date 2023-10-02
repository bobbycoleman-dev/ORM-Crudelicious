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

    //! Index -> View all Dishes
    [HttpGet("")]
    public IActionResult Index()
    {
        
        ViewBag.AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        
        return View();
    }

    //! Create new Dish view
    [HttpGet("dishes/new")]
    public ViewResult New()
    {
        return View();
    }

    //! Create Dish post request
    [HttpPost("dishes/create")]
    public IActionResult Create(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return View("New");
        }

        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    //! Show Dish View
    [HttpGet("dishes/{id}")]
    public ViewResult Show(int id)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View(OneDish);
    }

    //! Edit a Dish View
    [HttpGet("dishes/{id}/edit")]
    public ViewResult Edit(int id)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View(OneDish);
    }

    //! Update a Dish
    [HttpPost("dishes/{id}/update")]
    public IActionResult Update(int id, Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return View("Show");
        }

        Dish? OldDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        
        OldDish.Name = newDish.Name;
        OldDish.Chef = newDish.Chef;
        OldDish.Tastiness = newDish.Tastiness;
        OldDish.Calories = newDish.Calories;
        OldDish.Description = newDish.Description;
        OldDish.UpdatedAt = DateTime.Now;
        
        _context.SaveChanges();

        return Redirect($"/dishes/{id}");
    }

    //! Destroy Dish
    [HttpPost("dishes/{id}/destroy")]
    public RedirectToActionResult Destroy(int id)
    {
        Dish? OneDish = _context.Dishes.SingleOrDefault(d => d.DishId == id);
        _context.Dishes.Remove(OneDish);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
