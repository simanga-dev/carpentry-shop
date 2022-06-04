using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpentryShop.Models;

public class OrderBox
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    [ForeignKey("BoxId")]
    public Box Box { get; set; }
    public Boolean isComplete { get; set; }

}
