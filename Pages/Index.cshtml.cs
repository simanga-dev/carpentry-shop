using System.Diagnostics;
using CarpentryShop.Areas.Identity.Data;
using CarpentryShop.Models;
using CarpentryShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;

namespace CarpentryShop.Pages;

public class IndexModel : PageModel
{
    private readonly CarpentryShopIdentityDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    private readonly IEmailSender _emailSender;

    public string Message { get; private set; } = "PageModel in C#";

    public IndexModel(
            ILogger<IndexModel> logger,
            CarpentryShopIdentityDbContext context,
            IEmailSender emailSender)
    {
        _context = context;
        _logger = logger;
        _emailSender = emailSender;
        // _userManager = userManager;
    }

    public void OnGet()
    {
            System.Console.WriteLine(this.Request.Host);
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
        var customer = _context.Customers.FirstOrDefault(u => u.Email == Customer.Email);
        if (customer == null)
        {
            Customer.UserName = Customer.Email;
            customer = _context.Customers.Add(Customer).Entity;
        }

        Order.Customer = customer;
        _context.Orders.Add(Order);
        await _context.SaveChangesAsync();

        var msbBoxList = "";

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

            msbBoxList += $@"
               Inside Width: {Boxes[i].InsideWidth} mm <br />
               Inside Height: {Boxes[i].InsideHeight} mm <br />
               Inside Length: {Boxes[i].InsideLength} mm <br />
               Number of Box: {Boxes[i].Quantity} <br />
               Required Date: {Boxes[i].ExpectedDate.ToString("dd MMMM yyyy") } <br />
               ________________________________________________ <br />
                ";
        }

        try
        {
            InternetAddressList list = new InternetAddressList();
            // list.Add(MailboxAddress.Parse("deneo@copalcor.co.za"));
            list.Add(MailboxAddress.Parse("hendry@copalcor.co.za"));
            list.Add(MailboxAddress.Parse(customer.Email));


            // Send Email to supervisor and requesting user
            await _emailSender.SendEmailAsync(list, $"[CarpentryShop] Order Placement Notification",
                    $@"
                    Good day

                    This email is to confirm that { customer.FirstName } Place an Order <br />
                    for the following boxes under the department of { customer.Department }. <br />
                    <br />
                   
                    Order Ref: { Order.Id } <br />
                    view order information on the following link: <a href='https://{this.Request.Host}/Print?id={Order.Id}'>https://{this.Request.Host}/Print?id={Order.Id}</a>

                    <br />

                    ");
        }
        catch (System.Exception)
        {

            throw;
        }

        return RedirectToPage("./Success");
    }

}

