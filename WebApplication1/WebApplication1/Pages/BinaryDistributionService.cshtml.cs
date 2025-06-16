using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Pages.Models;

namespace WebApplication1.Pages
{
    public class BinaryDistributionServiceModel : PageModel
    {
        private readonly BinaryDistributionService _service;

        public BinaryDistributionServiceModel(BinaryDistributionService service)
            => _service = service;

        [BindProperty]
        public int Length { get; set; } = 5;

        [BindProperty]
        public double P { get; set; } = 0.3;

        public int[]? GeneratedSeries { get; private set; }

        public string? ErrorMessage { get; private set; }

        public void OnPost()
        {
            if (Length < 1)
            {
                ErrorMessage = "Liczba powtórzeñ musi byæ co najmniej 1.";
                return;
            }
            if (P < 0 || P > 1)
            {
                ErrorMessage = "Parametr p musi byæ w przedziale [0,1].";
                return;
            }

            GeneratedSeries = _service.GenerateBinarySeries(Length, P);
        }
    }
}


