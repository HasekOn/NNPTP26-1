using System.Collections.Generic;

namespace NNPTPZ1.Algebra
{
    /// <summary>
    /// Polynomial with complex coefficients.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Coefficients ordered from the constant term up to the highest power.
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        /// <summary>
        /// Creates the derivative of this polynomial.
        /// </summary>
        /// <returns>New polynomial representing the derivative</returns>
        public Polynomial Derivative()
        {
            Polynomial derivative = new Polynomial();
            for (int power = 1; power < Coefficients.Count; power++)
            {
                derivative.Coefficients.Add(Coefficients[power].Multiply(new ComplexNumber() { Real = power }));
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
            for (int power = 0; power < Coefficients.Count; power++)
            {
                ComplexNumber term = Coefficients[power];
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
            for (int power = 0; power < Coefficients.Count; power++)
            {
                result += Coefficients[power];
                if (power > 0)
                {
                    for (int j = 0; j < power; j++)
                    {
                        result += "x";
                    }
                }
                if (power + 1 < Coefficients.Count)
                    result += " + ";
            }
            return result;
        }
    }
}
