using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarpentryShop.Models;

public class Customer : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
}
