using CarpentryShop.Areas.Identity.Data;
using CarpentryShop.Models;
using CarpentryShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace CarpentryShop.Pages;

public class DetailsModel : PageModel
{

    private readonly ILogger<DetailsModel> _logger;
    private readonly CarpentryShopIdentityDbContext _context;
    private readonly IEmailSender _emailSender;

    public DetailsModel(CarpentryShopIdentityDbContext context, IEmailSender emailSender)
    {
        _context = context;
        _emailSender = emailSender;
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

    [BindProperty]
    public List<item> Items { get; set; } = new List<item>();
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        var msbBoxList = "";
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Value == "on")
            {
                var box = await _context.Boxes.FirstOrDefaultAsync(b => b.Id == Items[i].Id);
                box.isComplete = true;
                _context.Boxes.Update(box);
                await _context.SaveChangesAsync();

                msbBoxList += $@"
               Inside Width: {box.InsideWidth} <br />
               Inside Height: {box.InsideHeight} <br />
               Inside Length: {box.InsideLength} <br />
               Number of Box: {box.Quantity} <br />
               Required Date: {box.ExpectedDate} <br />
               ________________________________________________ <br />
                ";
            }
        }

        Order = await _context.Orders.Include(q => q.Customer).FirstOrDefaultAsync(m => m.Id == id);
        OrderBoxes = await _context.OrderBoxes.Include(q => q.Box).Where(q => q.Order.Id == Order.Id).ToListAsync();

        try
        {
            InternetAddressList list = new InternetAddressList();
            list.Add(MailboxAddress.Parse(Order.Customer.Email));
            list.Add(MailboxAddress.Parse("hendry@copalcor.co.za"));

            // Send Email to supervisor and requesting user
            await _emailSender.SendEmailAsync(list, $" [CarpentryShop] Order Completion Notification",
                    $@"
                    Good day

                    This email is to notify { Order.Customer.FirstName } from { Order.Customer.Department } <br />
                    That the following boxes are complete. <br />
                    <br />

                    Order Ref: { Order.Id } <br />
                    <br />

                    {msbBoxList}

                    ");
        }
        catch (System.Exception)
        {
            throw;
        }

        // check if all the boxes are comple to mark the whole order as complete
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

