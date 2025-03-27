using System;
using System.Linq;

namespace WebApplication1.Pages.Models
{
    public class BinaryDistributionService
    {
        private readonly Random _random = new Random();

        public int[] GenerateBinarySeries(int length, double p)
        {
            return Enumerable.Range(0, length)
                .Select(_ => _random.NextDouble() < p ? 1 : 0)
                .ToArray();
        }
    }
}

