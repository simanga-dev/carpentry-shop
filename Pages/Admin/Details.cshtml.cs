using CarpentryShop.Data;
using CarpentryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarpentryShop.Pages;

public class DetailsModel : PageModel
{

    private readonly ILogger<DetailsModel> _logger;
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<OrderBox> OrderBoxes { get; set; } = new List<OrderBox>();
    public Order Order { get; set; } = new Order();

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
        OrderBoxes = await _context.OrderBoxes.Include(q => q.Box).Where(q => q.Order.Id == Order.Id).ToListAsync();

        for (int i = 0; i < OrderBoxes.Count; i++)
        {
           System.Console.WriteLine(OrderBoxes[i].Id); 
        }



        if (Order == null)
        {
            return NotFound();
        }

        return Page();
    }
}

