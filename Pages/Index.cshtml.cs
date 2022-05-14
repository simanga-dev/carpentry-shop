using CarpentryShop.Data;
using CarpentryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarpentryShop.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public string Message { get; private set; } = "PageModel in C#";

    public IndexModel(ILogger<IndexModel> logger, 
            ApplicationDbContext context)
    {

        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {

    }


    [BindProperty]
    public List<Box> Boxes { get; set; }
    [BindProperty]
    public Customer Customer { get; set; } = new Customer();

    public async Task<IActionResult> OnPostAsync()
    {
        for (int i = 0; i < Boxes.Count(); i++)
        {
            _context.Boxes.Add(Boxes[i]);
            await _context.SaveChangesAsync();
            System.Console.WriteLine("Hello World");
            System.Console.WriteLine(Customer.Name);
            System.Console.WriteLine(Customer.Department);
            System.Console.WriteLine(Customer.Email);
            // System.Console.WriteLine(Customer.);

        }

        // if (!ModelState.IsValid)
        // {
        //     return Page();
        // }
        //
        // if (Customer != null) _context.Customer.Add(Customer);
        // await _context.SaveChangesAsync();

        return RedirectToPage("./Success");
    }

}

public class Customer
{
    public string Email { get; set; } = "";
    public string Name { get; set; } = "";
    public string Department { get; set; } = "";

}
