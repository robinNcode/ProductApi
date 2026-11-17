using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.src.Data;
using ProductApi.src.Models;

namespace ProductApi.Controllers;

public class ProductViewsController : Controller
{
    private readonly AppDbContext _context;

    public ProductViewsController(AppDbContext context)
    {
        _context = context;
    }

    // ─── GET /ProductViews ───────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return View(products);
    }

    // ─── GET /ProductViews/Details/{id} ─────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            TempData["ErrorMessage"] = $"Product with ID {id} was not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // ─── GET /ProductViews/Create ────────────────────────────────────────────
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Product());
    }

    // ─── POST /ProductViews/Create ───────────────────────────────────────────
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        // Additional backend business-rule validation
        if (!string.IsNullOrWhiteSpace(product.Name))
        {
            bool nameExists = await _context.Products
                .AnyAsync(p => p.Name.ToLower() == product.Name.ToLower().Trim());

            if (nameExists)
                ModelState.AddModelError(nameof(product.Name),
                    "A product with this name already exists.");
        }

        if (!ModelState.IsValid)
            return View(product);

        product.Name      = product.Name.Trim();
        product.CreatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Product \"{product.Name}\" was created successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ─── GET /ProductViews/Edit/{id} ─────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            TempData["ErrorMessage"] = $"Product with ID {id} was not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // ─── POST /ProductViews/Edit/{id} ────────────────────────────────────────
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        // Additional backend business-rule validation
        if (!string.IsNullOrWhiteSpace(product.Name))
        {
            bool nameExists = await _context.Products
                .AnyAsync(p => p.Id != product.Id &&
                               p.Name.ToLower() == product.Name.ToLower().Trim());

            if (nameExists)
                ModelState.AddModelError(nameof(product.Name),
                    "Another product with this name already exists.");
        }

        if (!ModelState.IsValid)
            return View(product);

        // Preserve CreatedAt — only update mutable fields
        var existing = await _context.Products.FindAsync(id);
        if (existing is null)
        {
            TempData["ErrorMessage"] = $"Product with ID {id} was not found.";
            return RedirectToAction(nameof(Index));
        }

        existing.Name        = product.Name.Trim();
        existing.Description = product.Description?.Trim();
        existing.Price       = product.Price;

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Product \"{existing.Name}\" was updated successfully.";
        return RedirectToAction(nameof(Details), new { id = existing.Id });
    }

    // ─── GET /ProductViews/Delete/{id} ───────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            TempData["ErrorMessage"] = $"Product with ID {id} was not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // ─── POST /ProductViews/Delete/{id} ──────────────────────────────────────
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            TempData["ErrorMessage"] = $"Product with ID {id} was not found.";
            return RedirectToAction(nameof(Index));
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Product \"{product.Name}\" was deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
