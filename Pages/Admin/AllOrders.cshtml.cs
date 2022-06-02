using CarpentryShop.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarpentryShop.Pages;

public class AllOrders : PageModel
{
    private readonly ILogger<AdminModel> _logger;
    private readonly ApplicationDbContext _context;

    public AllOrders(ILogger<AdminModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // public List<OrderBox> OrderBoxes { get; set; }
    // public List<Order> Orders { get; set; }
    public IList<int> NumOfBoxes { get; set; } = new List<int>();
    public int[] num { get; set; }
    public List<int> CompBoxes { get; set; } = new List<int>();

    public async Task OnGetAsync()
    {

    }
}
