using System.ComponentModel.DataAnnotations;

namespace CarpentryShop.Models;

public class Customer
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
}
