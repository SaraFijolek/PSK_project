using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Pages.Models;

namespace WebApplication1.Pages
{
    public class InsuranceSimulationModel : PageModel
    {
        private readonly InsuranceSimulationService _simulationService;

        public InsuranceSimulationModel(InsuranceSimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        [BindProperty]
        public int Days { get; set; } = 365;

        [BindProperty]
        public double InitialCapital { get; set; } = 25000;

        [BindProperty]
        public double ClaimRate { get; set; } = 10;

        [BindProperty]
        public double AvgClaim { get; set; } = 1000;

        [BindProperty]
        public double DailyIncome { get; set; } = 11000;

        [BindProperty]
        public int NumSimulations { get; set; } = 10000;

        public string Result { get; set; }

        public void OnPost()
        {
            double probability = _simulationService.RunMultipleSimulations(NumSimulations, Days, InitialCapital, ClaimRate, AvgClaim, DailyIncome);
            Result = $"Prawdopodobieñstwo, ¿e kapita³ pozostanie dodatni przez {Days} dni: {probability * 100:F2}%";
        }
    }
}