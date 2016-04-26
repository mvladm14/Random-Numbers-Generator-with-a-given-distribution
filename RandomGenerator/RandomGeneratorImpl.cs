using System;
using System.Linq;

namespace RandomGenerator
{
    public class RandomGeneratorImpl : IRandomGenerator
    {
        private readonly double[] _cumulative;
        private readonly Random _r;

        public RandomGeneratorImpl(double[] distribution)
        {
            _cumulative = new double[distribution.Length];
            Normalize(distribution);
            _r = new Random();
        }

        private void Normalize(double[] distribution)
        {
            double total = distribution.Sum();
            double sum = 0.0;
            for (int i = 0; i < distribution.Length; i++)
            {
                sum += distribution[i] / total;
                _cumulative[i] = sum;
            }

            if (_cumulative[distribution.Length - 1] > 0.0)
                _cumulative[distribution.Length - 1] = 1.0;
        }

        public int Generate()
        {            
            double diceRoll = _r.NextDouble();

            for (int i = 0; i < _cumulative.Length; i++)
            {
                if (diceRoll < _cumulative[i])
                {
                    return i;
                }
            }
            return -1;
        }
    }
}