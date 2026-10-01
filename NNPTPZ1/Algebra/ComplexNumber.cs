using System;

namespace NNPTPZ1.Algebra
{
    /// <summary>
    /// Complex number with a real and an imaginary part.
    /// </summary>
    public class ComplexNumber
    {
        public double Real { get; set; }
        public float Imaginary { get; set; }

        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber other = obj as ComplexNumber;
                return other.Real == Real && other.Imaginary == Imaginary;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode() * 397) ^ Imaginary.GetHashCode();
            }
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber b)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                Real = Real * b.Real - Imaginary * b.Imaginary,
                Imaginary = (float)(Real * b.Imaginary + Imaginary * b.Real)
            };
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                Real = Real + b.Real,
                Imaginary = Imaginary + b.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            return new ComplexNumber()
            {
                Real = Real - b.Real,
                Imaginary = Imaginary - b.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            var numerator = Multiply(new ComplexNumber() { Real = b.Real, Imaginary = -b.Imaginary });
            var denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new ComplexNumber()
            {
                Real = numerator.Real / denominator,
                Imaginary = (float)(numerator.Imaginary / denominator)
            };
        }
    }
}
