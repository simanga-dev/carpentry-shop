using CarpentryShop.Data;
using CarpentryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarpentryShop.Pages;

public class AdminModel : PageModel
{
    private readonly ILogger<AdminModel> _logger;
    private readonly ApplicationDbContext _context;

    public AdminModel(ILogger<AdminModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<OrderBox> OrderBoxes { get; set; }
    public List<Order> Orders { get; set; }

    public async Task OnGetAsync()
    {
        OrderBoxes = await _context.OrderBoxes
            .Include(q => q.Box)
            .Include(q => q.Order)
            .ThenInclude(Order => Order.Customer)
            .ToListAsync();

        Orders = await _context.Orders.Include(q => q.Customer).ToListAsync();

        // foreach (var item in OrderBoxes)
        // {
        //    System.Console.WriteLine(item.Box.Id); 
        //    System.Console.WriteLine(item.Order.Id); 
        //    System.Console.WriteLine(item.Order.Customer.Id); 
        // }

    }
}

