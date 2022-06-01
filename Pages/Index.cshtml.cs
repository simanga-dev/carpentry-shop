using System.Diagnostics;
using CarpentryShop.Data;
using CarpentryShop.Models;
using CarpentryShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;

namespace CarpentryShop.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    private readonly IEmailSender _emailSender;

    public string Message { get; private set; } = "PageModel in C#";

    public IndexModel(
            ILogger<IndexModel> logger,
            ApplicationDbContext context,
            IEmailSender emailSender)
    {

        _context = context;
        _logger = logger;
        _emailSender = emailSender;
    }

    public void OnGet()
    {

    }


    [BindProperty]
    public List<Box> Boxes { get; set; }
    [BindProperty]
    public Customer Customer { get; set; }
    [BindProperty]
    public Order Order { get; set; }
    public OrderBox OrderBox { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        // check if customer alredad exist in database..
        // Create one if doesn't
        var customer = _context.Customers.FirstOrDefault(c => c.Email == Customer.Email);
        if (customer == null)
        {
            customer = _context.Customers.Add(Customer).Entity;
            await _context.SaveChangesAsync();
        }

        Order.Customer = customer;
        _context.Orders.Add(Order);
        await _context.SaveChangesAsync();


        // save product
        for (int i = 0; i < Boxes.Count(); i++)
        {
            var orderBox = new OrderBox();

            Boxes[i].Description = $"A Box of { Boxes[i].InsideLength } x { Boxes[i].InsideWidth } x { Boxes[i].InsideHeight } dimesion";
            _context.Boxes.Add(Boxes[i]);
            await _context.SaveChangesAsync();

            orderBox.Box = Boxes[i];
            orderBox.Order = Order;
            _context.OrderBoxes.Add(orderBox);
            await _context.SaveChangesAsync();
        }

        // Process process = new Process
        // {
        //     StartInfo = new ProcessStartInfo
        //     {
        //         FileName = "bash",
        //         RedirectStandardInput = true,
        //         RedirectStandardOutput = true,
        //         RedirectStandardError = true,
        //         UseShellExecute = false
        //     }
        // };

        // process.Start();
        // await process.StandardInput.WriteLineAsync("pwd");
        // var output = await process.StandardOutput.ReadLineAsync();
        // Console.WriteLine(output);

        // try
        // {
        //     InternetAddressList list = new InternetAddressList();
        //     list.Add(MailboxAddress.Parse("hendry@copalcor.co.za"));
        //     // list.Add(MailboxAddress.Parse("benson@copalcor.co.za"));
        //     // list.Add(MailboxAddress.Parse("Thembisile@copalcor.co.za "));
        //
        //     // Send Email to supervisor and requesting user
        //     await _emailSender.SendEmailAsync(list, $" { customer.Name } Placed an Order for Carpentry Shop",
        //             $@" Order information blah blah blah");
        // }
        // catch (System.Exception)
        // {
        //
        //     throw;
        // }
        //

        return RedirectToPage("./Success");
    }

}

