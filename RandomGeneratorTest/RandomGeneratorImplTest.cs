using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomGenerator;
using NUnit.Framework;
using System.Linq;

namespace RandomGeneratorTest
{
    [TestClass]
    public class RandomGeneratorImplTest
    {
        [TestCase(new double[] { 1.0, 2.0 }, TestName = "Test 1")]
        [TestCase(new double[] { 1.0, 1.0, 1.0, 1.0, 1.0 }, TestName = "Test 2")]
        [TestCase(new double[] { 2.0, 0.0, 2.0 }, TestName = "Test 3")]
        public void GenerateTest(double[] distribution)
        {
            RandomGeneratorImpl r = new RandomGeneratorImpl(distribution);
            double[] result = new double[distribution.Length];
            double total = distribution.Sum();

            int TOTAL = 10000;
            double DELTA = 0.1;

            for (int i = 0; i < TOTAL; i++)
            {
                var random = r.Generate();
                result[random]++;
            }

            for (int i = 0; i < distribution.Length; i++)
            {
                var expected = distribution[i] / total;
                var actual = result[i] / TOTAL;

                NUnit.Framework.Assert.AreEqual(expected, actual, DELTA);
            }
        }

        [TestCase(new double[] { 0.0, 0.0, 0.0 }, TestName = "Test with -1 generated values.")]
        public void GenerateTest2(double[] distribution)
        {
            RandomGeneratorImpl r = new RandomGeneratorImpl(distribution);

            int TOTAL = 10000;

            for (int i = 0; i < TOTAL; i++)
            {
                var random = r.Generate();
                NUnit.Framework.Assert.AreEqual(-1, random);
            }
        }
    }
}