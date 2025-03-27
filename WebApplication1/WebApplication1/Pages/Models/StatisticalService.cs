using System;
using System.Linq;
namespace WebApplication1.Pages.Models
{
    public class StatisticalService
    {
        public static double Covariance(double[,] P, double[] X, double[] Y)
        {
            double EX = 0, EY = 0;
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                {
                    EX += X[i] * P[i, j];
                    EY += Y[j] * P[i, j];
                }

            double covXY = 0;
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                    covXY += (X[i] - EX) * (Y[j] - EY) * P[i, j];

            return covXY;
        }

        public static double Correlation(double[,] P, double[] X, double[] Y)
        {
            double covXY = Covariance(P, X, Y);
            double varX = 0, varY = 0;

            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                {
                    varX += Math.Pow(X[i] - X.Average(), 2) * P[i, j];
                    varY += Math.Pow(Y[j] - Y.Average(), 2) * P[i, j];
                }

            return covXY / Math.Sqrt(varX * varY);
        }

        public static bool AreIndependent(double[,] P, double[] X, double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                {
                    double PX = P.Cast<double>().Sum() / X.Length;
                    double PY = P.Cast<double>().Sum() / Y.Length;
                    if (Math.Abs(P[i, j] - PX * PY) > 1e-6)
                        return false;
                }
            return true;
        }

        public static bool AreLinearlyDependent(double correlation)
        {
            return Math.Abs(correlation) == 1;
        }
    }
}
