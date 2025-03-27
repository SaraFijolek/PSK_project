using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Pages.Models;

namespace WebApplication1.Pages
{
    public class Index1Model : PageModel
    {
        public double CovarianceResult { get; private set; }
        public double CorrelationResult { get; private set; }
        public bool IsIndependent { get; private set; }
        public bool IsLinearlyDependent { get; private set; }

        public void OnGet()
        {
            double[,] P = { { 0.1, 0.2, 0.3 }, { 0.1, 0.1, 0.2 } };
            double[] X = { 1, 2 };
            double[] Y = { 3, 2, 1 };

            CovarianceResult = StatisticalService.Covariance(P, X, Y);
            CorrelationResult = StatisticalService.Correlation(P, X, Y);
            IsIndependent = StatisticalService.AreIndependent(P, X, Y);
            IsLinearlyDependent = StatisticalService.AreLinearlyDependent(CorrelationResult);
        }
    }
}
