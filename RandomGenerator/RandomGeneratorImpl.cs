using System;
using System.Linq;

namespace RandomGenerator
{
    public class RandomGeneratorImpl : IRandomGenerator
    {
        private readonly double[] _distribution;
        private readonly Random r;

        public RandomGeneratorImpl(double[] distribution)
        {
            _distribution = new double[distribution.Length];
            r = new Random();
            Normalize(distribution);
        }

        private void Normalize(double[] distribution)
        {
            double total = distribution.Sum();

            for (int i = 0; i < distribution.Length; i++)
            {
                _distribution[i] = distribution[i] / total;
            }
        }

        public int Generate()
        {            
            double diceRoll = r.NextDouble();
            double cumulative = 0.0;

            for (int i = 0; i < _distribution.Length; i++)
            {
                cumulative += _distribution[i];
                if (diceRoll < cumulative)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}