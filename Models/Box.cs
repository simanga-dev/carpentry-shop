
using System.ComponentModel.DataAnnotations;
namespace CarpentryShop.Models;

public class Box
{
    [Key]
    public Guid Id { get; set; }
    public string InsideLength { get; set; }
    public string InsideWidth { get; set; }
    public string InsideHeight { get; set; }
    public Boolean isLid { get; set; }
    public Boolean isFoot { get; set; }
}
