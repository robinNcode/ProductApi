using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.src.Data;

namespace ProductApi.Controllers;

public class ProductViewsController : Controller
{
    private readonly AppDbContext _context;

    public ProductViewsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .AsNoTracking()
            .ToListAsync();

        return View(products);
    }
}