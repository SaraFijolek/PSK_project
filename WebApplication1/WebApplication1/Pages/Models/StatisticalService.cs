using System;
using System.Linq;
namespace WebApplication1.Pages.Models
{
    public class StatisticalService
    {
        // 1) Oblicza EX i EY raz, wykorzystując poprawne marginesy
        public static (double EX, double EY) Expectations(double[,] P, double[] X, double[] Y)
        {
            double EX = 0, EY = 0;
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                {
                    EX += X[i] * P[i, j];
                    EY += Y[j] * P[i, j];
                }
            return (EX, EY);
        }

        public static double Covariance(double[,] P, double[] X, double[] Y)
        {
            var (EX, EY) = Expectations(P, X, Y);
            double cov = 0;
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                    cov += (X[i] - EX) * (Y[j] - EY) * P[i, j];
            return cov;
        }

        public static double Correlation(double[,] P, double[] X, double[] Y)
        {
            var (EX, EY) = Expectations(P, X, Y);

            double varX = 0, varY = 0;
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                {
                    varX += Math.Pow(X[i] - EX, 2) * P[i, j];
                    varY += Math.Pow(Y[j] - EY, 2) * P[i, j];
                }

            return Covariance(P, X, Y) / Math.Sqrt(varX * varY);
        }

        public static bool AreIndependent(double[,] P, double[] X, double[] Y, double tol = 1e-6)
        {
            // 1) Obliczamy marginesy
            double[] PX = new double[X.Length];
            double[] PY = new double[Y.Length];

            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                    PX[i] += P[i, j];

            for (int j = 0; j < Y.Length; j++)
                for (int i = 0; i < X.Length; i++)
                    PY[j] += P[i, j];

            // 2) Porównujemy P[i,j] z PX[i]*PY[j]
            for (int i = 0; i < X.Length; i++)
                for (int j = 0; j < Y.Length; j++)
                    if (Math.Abs(P[i, j] - PX[i] * PY[j]) > tol)
                        return false;

            return true;
        }

        public static bool AreLinearlyDependent(double correlation)
            => Math.Abs(correlation - 1) < 1e-12 || Math.Abs(correlation + 1) < 1e-12;
    }
}
