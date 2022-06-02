using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarpentryShop.Models;

public class Box
{
    [Key]
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string InsideLength { get; set; }
    public string InsideWidth { get; set; }
    public string InsideHeight { get; set; }
    public Boolean isLid { get; set; }
    public Boolean isFoot { get; set; }
    public Boolean isComplete { get; set; }
    // this does not belong here.. but hey 
    public int Quantity { get; set; }
    public DateTime ExpectedDate { get; set; }
}
