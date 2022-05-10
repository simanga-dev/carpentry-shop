using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime ExpectedDate { get; set; }
    // [Index(IsUnique = true)]
    public string Ref { get; set; }

}
