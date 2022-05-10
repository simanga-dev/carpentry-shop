using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarpentryShop.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string Message { get; private set; } = "PageModel in C#";

    public IndexModel(ILogger<IndexModel> logger)
    {

        _logger = logger;
    }

    public void OnGet()
    {
        Message += $" Server time is { DateTime.Now }";

    }


    [BindProperty]
    public List<BoxItem> Boxes { get; set; }
    [BindProperty]
    public Customer Customer { get; set; } = new Customer();

    public async Task<IActionResult> OnPostAsync()
    {
        for (int i = 0; i < Boxes.Count(); i++)
        {
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

        return RedirectToPage("./Index");
    }

}

public class BoxItem
{
    public string InsideLength { get; set; } = "0001";
    public string InsideWidth { get; set; } = "00";
    public string InsideHeight { get; set; } = "00";
    public string NumberOfBox { get; set; } = "00";
    public Boolean isLid { get; set; } = false;
    public Boolean isFoot { get; set; } = false;
}


public class Customer
{
    public string Email { get; set; } = "";
    public string Name { get; set; } = "";
    public string Department { get; set; } = "";
        
}
