using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpentryShop.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public string Description { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}
