using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarpentryShop.Pages;

public class SuccessModel : PageModel
{
    private ILogger<SuccessModel> _logger;

    public SuccessModel(ILogger<SuccessModel> logger)
    {
        _logger = logger;

    }

    public void OnGet()
    {
       
    }
    
}
