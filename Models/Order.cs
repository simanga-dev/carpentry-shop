using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarpentryShop.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
    [NotMapped]
    public IFormFile? File { set; get; }
    // public User User { get; set; }
    public Boolean isComplete { get; set; }

    public Order()
    {
        this.Date = DateTime.UtcNow;
        isComplete = false;
    }
}
