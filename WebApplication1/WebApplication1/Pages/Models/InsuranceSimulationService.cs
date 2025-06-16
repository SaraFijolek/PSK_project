using System;

namespace WebApplication1.Pages.Models
{
    public class InsuranceSimulationService
    {
        private static readonly Random _random = new();

        public double RunMultipleSimulations(int numSimulations, int days, double initialCapital, double claimRate, double avgClaim, double dailyIncome)
        {
            int successfulSimulations = 0;

            for (int i = 0; i < numSimulations; i++)
            {
                if (RunSingleSimulation(days, initialCapital, claimRate, avgClaim, dailyIncome))
                    successfulSimulations++;
            }

            return (double)successfulSimulations / numSimulations;
        }

        private bool RunSingleSimulation(int days, double capital, double claimRate, double avgClaim, double dailyIncome)
        {
            for (int day = 0; day < days; day++)
            {
                int claims = SamplePoisson(claimRate);
                double totalClaims = 0;

                for (int i = 0; i < claims; i++)
                    totalClaims += SampleExponential(avgClaim);

                capital += dailyIncome - totalClaims;

                if (capital < 0)
                    return false;
            }
            return true;
        }

        private int SamplePoisson(double lambda)
        {
            double L = Math.Exp(-lambda);
            double p = 1.0;
            int k = 0;

            while (p > L)
            {
                k++;
                p *= _random.NextDouble();
            }

            return k - 1;
        }

        private double SampleExponential(double mean)
        {
            double u = _random.NextDouble();
            return -mean * Math.Log(1 - u);
        }
    }
}
