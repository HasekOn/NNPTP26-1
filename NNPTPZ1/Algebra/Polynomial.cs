using System.Collections.Generic;

namespace NNPTPZ1.Algebra
{
    /// <summary>
    /// Polynomial with complex coefficients.
    /// </summary>
    public class Polynomial
    {
        private readonly List<ComplexNumber> coefficients = new List<ComplexNumber>();

        /// <summary>
        /// Coefficients ordered from the constant term up to the highest power.
        /// </summary>
        public IReadOnlyList<ComplexNumber> Coefficients => coefficients;

        public void Add(ComplexNumber coefficient) =>
            coefficients.Add(coefficient);

        /// <summary>
        /// Creates the derivative of this polynomial.
        /// </summary>
        /// <returns>New polynomial representing the derivative</returns>
        public Polynomial Derivative()
        {
            Polynomial derivative = new Polynomial();
            for (int power = 1; power < coefficients.Count; power++)
            {
                derivative.Add(coefficients[power].Multiply(new ComplexNumber() { Real = power }));
            }

            return derivative;
        }

        /// <summary>
        /// Evaluates the polynomial at the given point.
        /// </summary>
        /// <param name="x">Point of evaluation</param>
        /// <returns>Value of the polynomial at <paramref name="x"/></returns>
        public ComplexNumber ValueAt(ComplexNumber x)
        {
            ComplexNumber sum = ComplexNumber.Zero;
            for (int power = 0; power < coefficients.Count; power++)
            {
                ComplexNumber term = coefficients[power];
                ComplexNumber xPower = x;

                if (power > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        xPower = xPower.Multiply(x);

                    term = term.Multiply(xPower);
                }

                sum = sum.Add(term);
            }

            return sum;
        }

        /// <summary>
        /// Formats the polynomial as a sum of terms, the power of x is written as repeated "x".
        /// </summary>
        /// <returns>Text representation of the polynomial</returns>
        public override string ToString()
        {
            string result = "";
            for (int power = 0; power < coefficients.Count; power++)
            {
                result += coefficients[power];
                for (int j = 0; j < power; j++)
                {
                    result += "x";
                }
                if (power + 1 < coefficients.Count)
                    result += " + ";
            }
            return result;
        }
    }
}
