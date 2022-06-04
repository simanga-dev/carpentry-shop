using CarpentryShop.Areas.Identity.Data;
using CarpentryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarpentryShop.Pages;

public class DetailsModel : PageModel
{

    private readonly ILogger<DetailsModel> _logger;
    private readonly CarpentryShopIdentityDbContext _context;

    public DetailsModel(CarpentryShopIdentityDbContext context)
    {
        _context = context;
    }

    public List<OrderBox> OrderBoxes { get; set; } = new List<OrderBox>();
    [BindProperty]
    public Order Order { get; set; } = new Order();

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
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

    [BindProperty]
    public List<item> Items { get; set; } = new List<item>();
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Value == "on")
            {
                var box = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == Items[i].Id);
                box.isComplete = true;
                _context.Boxes.Update(box);
                await _context.SaveChangesAsync();

            }
        }
        Order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
        OrderBoxes = await _context.OrderBoxes.Include(q => q.Box).Where(q => q.Order.Id == Order.Id).ToListAsync();

        for (int i = 0; i < OrderBoxes.Count; i++)
        {
            if (!OrderBoxes[i].Box.isComplete)
                return Page();

            Order.isComplete = true;
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();
        }

        return Page();
    }

    public class item
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}

