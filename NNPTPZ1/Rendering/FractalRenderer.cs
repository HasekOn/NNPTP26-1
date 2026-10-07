using System;
using System.Drawing;
using NNPTPZ1.Algebra;
using NNPTPZ1.Configuration;

namespace NNPTPZ1.Rendering
{
    public class FractalRenderer
    {
        private const double ZeroReplacement = 0.0001;

        private readonly RenderOptions options;
        private readonly ColorPalette palette;

        public FractalRenderer(RenderOptions options, ColorPalette palette)
        {
            this.options = options;
            this.palette = palette;
        }

        public void Render(Bitmap bitmap)
        {
            double xStep = (options.XMax - options.XMin) / options.Width;
            double yStep = (options.YMax - options.YMin) / options.Height;

            Polynomial polynomial = new Polynomial();
            polynomial.Add(new ComplexNumber() { Real = 1 });
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(ComplexNumber.Zero);
            polynomial.Add(new ComplexNumber() { Real = 1 });
            NewtonIteration newton = new NewtonIteration(polynomial);

            Console.WriteLine(newton.Polynomial);
            Console.WriteLine(newton.Derivative);

            // for every pixel in image...
            for (int y = 0; y < options.Height; y++)
            {
                for (int x = 0; x < options.Width; x++)
                {
                    // find "world" coordinates of pixel
                    double imaginary = options.YMin + y * yStep;
                    double real = options.XMin + x * xStep;

                    ComplexNumber z = new ComplexNumber()
                    {
                        Real = real,
                        Imaginary = (float)(imaginary)
                    };

                    if (z.Real == 0)
                        z.Real = ZeroReplacement;
                    if (z.Imaginary == 0)
                        z.Imaginary = (float)ZeroReplacement;

                    // find solution of equation using newton's iteration
                    z = newton.Solve(z, out int iterationCount);

                    // find solution root number
                    var rootIndex = newton.FindRootIndex(z);

                    // colorize pixel according to root number
                    bitmap.SetPixel(x, y, palette.GetColor(rootIndex, iterationCount));
                }
            }
        }
    }
}
