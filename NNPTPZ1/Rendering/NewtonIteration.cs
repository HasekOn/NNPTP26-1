using System;
using System.Collections.Generic;
using NNPTPZ1.Algebra;

namespace NNPTPZ1.Rendering
{
    /// <summary>
    /// Finds roots of a polynomial using Newton's method and assigns an index to each distinct root found.
    /// </summary>
    public class NewtonIteration
    {
        private readonly List<ComplexNumber> roots = new List<ComplexNumber>();

        public NewtonIteration(Polynomial polynomial)
        {
            Polynomial = polynomial;
            Derivative = polynomial.Derivative();
        }

        public Polynomial Polynomial { get; }
        public Polynomial Derivative { get; }

        public ComplexNumber Solve(ComplexNumber z, out float iterationCount)
        {
            iterationCount = 0;
            for (int iteration = 0; iteration < 30; iteration++)
            {
                var step = Polynomial.ValueAt(z).Divide(Derivative.ValueAt(z));
                z = z.Subtract(step);

                if (Math.Pow(step.Real, 2) + Math.Pow(step.Imaginary, 2) >= 0.5)
                {
                    iteration--;
                }
                iterationCount++;
            }

            return z;
        }

        public int FindRootIndex(ComplexNumber z)
        {
            var found = false;
            var rootIndex = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(z.Real - roots[i].Real, 2) + Math.Pow(z.Imaginary - roots[i].Imaginary, 2) <= 0.01)
                {
                    found = true;
                    rootIndex = i;
                }
            }
            if (!found)
            {
                roots.Add(z);
                rootIndex = roots.Count;
            }

            return rootIndex;
        }
    }
}
