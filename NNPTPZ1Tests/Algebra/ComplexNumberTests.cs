using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Algebra.Tests
{
    [TestClass]
    public class ComplexNumberTests
    {
        [TestMethod]
        public void Add_TwoNumbers_ReturnsSumOfParts()
        {
            ComplexNumber a = new ComplexNumber() { Real = 10, Imaginary = 20 };
            ComplexNumber b = new ComplexNumber() { Real = 1, Imaginary = 2 };

            ComplexNumber actual = a.Add(b);

            Assert.AreEqual(new ComplexNumber() { Real = 11, Imaginary = 22 }, actual);
        }

        [TestMethod]
        public void Add_Zero_ReturnsSameValue()
        {
            ComplexNumber a = new ComplexNumber() { Real = 1, Imaginary = -1 };
            ComplexNumber zero = new ComplexNumber() { Real = 0, Imaginary = 0 };

            ComplexNumber actual = a.Add(zero);

            Assert.AreEqual(new ComplexNumber() { Real = 1, Imaginary = -1 }, actual);
        }

        [DataTestMethod]
        [DataRow(10.0, 20f, "(10 + 20i)")]
        [DataRow(1.0, 2f, "(1 + 2i)")]
        [DataRow(1.0, -1f, "(1 + -1i)")]
        [DataRow(0.0, 0f, "(0 + 0i)")]
        public void ToString_IntegerParts_FormatsRealAndImaginaryPart(double real, float imaginary, string expected)
        {
            ComplexNumber number = new ComplexNumber() { Real = real, Imaginary = imaginary };

            string actual = number.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
