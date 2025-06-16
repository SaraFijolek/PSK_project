using System;
using System.Linq;

namespace WebApplication1.Pages.Models
{
    public class BinaryDistributionService
    {
        // Jeden Random na całą aplikację - unika powtarzających się ziaren
        private static readonly Random _random = new();

        /// <summary>
        /// Generuje serię długości `length` z rozkładu Bernoulliego(p).
        /// </summary>
        public int[] GenerateBinarySeries(int length, double p)
        {
            if (length < 1)
                throw new ArgumentException("Length musi być ≥ 1", nameof(length));
            if (p < 0.0 || p > 1.0)
                throw new ArgumentException("p musi być w [0,1]", nameof(p));

            var series = new int[length];
            for (int i = 0; i < length; i++)
                series[i] = (_random.NextDouble() < p) ? 1 : 0;
            return series;
        }
    }
}


