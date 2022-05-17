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

    public Order()
    {
        this.Date = DateTime.UtcNow;
    }
}
