using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Algebra.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        private static Polynomial CreateOnePlusXSquared()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            polynomial.Add(new ComplexNumber() { Real = 0, Imaginary = 0 });
            polynomial.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });
            return polynomial;
        }

        [DataTestMethod]
        [DataRow(0.0, 1.0)]
        [DataRow(1.0, 2.0)]
        [DataRow(2.0, 5.0)]
        public void ValueAt_RealPoint_ReturnsPolynomialValue(double x, double expected)
        {
            Polynomial polynomial = CreateOnePlusXSquared();

            ComplexNumber actual = polynomial.ValueAt(new ComplexNumber() { Real = x, Imaginary = 0 });

            Assert.AreEqual(new ComplexNumber() { Real = expected, Imaginary = 0 }, actual);
        }

        [TestMethod]
        public void ToString_ThreeCoefficients_FormatsTermsWithPowersOfX()
        {
            Polynomial polynomial = CreateOnePlusXSquared();

            string actual = polynomial.ToString();

            Assert.AreEqual("(1 + 0i) + (0 + 0i)x + (1 + 0i)xx", actual);
        }
    }
}
