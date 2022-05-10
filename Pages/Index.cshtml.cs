using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarpentryShop.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

        public string InsideLength { get; set; } = "00";
        public string InsideWidth { get; set; } = "00";
        public string InsideHeight { get; set; } = "00";
        public Boolean isLid { get; set; } = false;
        public Boolean isFoot { get; set; } = false;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
