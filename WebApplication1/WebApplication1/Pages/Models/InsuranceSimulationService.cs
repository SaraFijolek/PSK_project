using System;

namespace WebApplication1.Pages.Models
{
    public class InsuranceSimulationService
    {
        private static readonly Random _random = new Random();

        public double RunMultipleSimulations(int numSimulations, int days, double initialCapital, double claimRate, double avgClaim, double dailyIncome)
        {
            int successfulSimulations = 0;

            for (int i = 0; i < numSimulations; i++)
            {
                if (RunSingleSimulation(days, initialCapital, claimRate, avgClaim, dailyIncome))
                {
                    successfulSimulations++;
                }
            }

            return (double)successfulSimulations / numSimulations;
        }

        private bool RunSingleSimulation(int days, double initialCapital, double claimRate, double avgClaim, double dailyIncome)
        {
            double capital = initialCapital;

            for (int day = 0; day < days; day++)
            {
                int claims = PoissonProcess(claimRate);
                double totalClaims = 0;

                for (int i = 0; i < claims; i++)
                {
                    totalClaims += ExponentialDistribution(avgClaim);
                }

                capital += dailyIncome - totalClaims;

                if (capital < 0) return false;
            }

            return true;
        }

        private int PoissonProcess(double rate)
        {
            double L = Math.Exp(-rate);
            int k = 0;
            double p = 1.0;

            do
            {
                k++;
                p *= _random.NextDouble();
            } while (p > L);

            return k - 1;
        }

        private double ExponentialDistribution(double mean)
        {
            return -mean * Math.Log(1 - _random.NextDouble());
        }
    }
}