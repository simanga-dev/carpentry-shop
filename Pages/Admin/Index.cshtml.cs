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
    public IList<int> NumOfBoxes { get; set; } = new List<int>();
    public int[] num { get; set; }
    public List<int> CompBoxes { get; set; } = new List<int>();

    public async Task OnGetAsync()
    {
        try
        {
            OrderBoxes = await _context.OrderBoxes
                .Include(q => q.Box)
                .Include(q => q.Order)
                .ThenInclude(Order => Order.Customer)
                .ToListAsync();

            Orders = await _context.Orders.Include(q => q.Customer).ToListAsync();

            for (int i = 0; i < Orders.Count; i++)
            {
                int boxes = await _context.OrderBoxes.Where(q => q.Order.Id == Orders[i].Id).CountAsync();
                NumOfBoxes.Add(boxes);

            }

        }
        catch (System.Exception)
        {

            throw;
        }

    }
}

