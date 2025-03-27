using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Pages.Models;

namespace WebApplication1.Pages
{
    public class BinaryDistributionServiceModel : PageModel
    {
        private readonly BinaryDistributionService _service;

        public BinaryDistributionServiceModel(BinaryDistributionService service) 
        {
            _service = service;
        }

        [BindProperty]
        public int Length { get; set; } = 5;  

        public int[]? GeneratedSeries { get; private set; }

        public void OnPost() 
        {
            GeneratedSeries = _service.GenerateBinarySeries(Length, 0.3);
        }
    }
}

