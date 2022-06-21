using CarpentryShop.Areas.Identity.Data;
using CarpentryShop.Models;
using CarpentryShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace CarpentryShop.Pages;

public class PrintModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly CarpentryShopIdentityDbContext _context;

    public PrintModel(ILogger<IndexModel> logger, CarpentryShopIdentityDbContext context = null)
    {
        _logger = logger;
        _context = context;
    }

    public List<OrderBox> OrderBoxes { get; set; } = new List<OrderBox>();
    [BindProperty]
    public Order Order { get; set; } = new Order();

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return RedirectToPage("Error");
        }

        Order = await _context.Orders.Include(q => q.Customer).FirstOrDefaultAsync(m => m.Id == id);
        OrderBoxes = await _context.OrderBoxes.Include(q => q.Box).Where(q => q.Order.Id == Order.Id).ToListAsync();

        // for (int i = 0; i < OrderBoxes.Count; i++)
        // {
        //     System.Console.WriteLine(OrderBoxes[i].Id);
        // }

        if (Order == null)
        {
            return NotFound();
        }

        return Page();
    }

}
    
