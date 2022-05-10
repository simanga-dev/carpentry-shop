
using System.ComponentModel.DataAnnotations;

public class Box
{
    public int Id { get; set; }

    [Required]
    public string? InsideLength { get; set; }
    [Required]
    public string? InsideWidth { get; set; }
    [Required]
    public string? InsideHeight { get; set; }
    [Required]
    public Boolean isLid { get; set; }
    [Required]
    public Boolean isFoot { get; set; }

}
