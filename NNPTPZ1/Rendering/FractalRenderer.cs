using System;
using System.Drawing;
using NNPTPZ1.Algebra;
using NNPTPZ1.Configuration;

namespace NNPTPZ1.Rendering
{
    public class FractalRenderer
    {
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
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            NewtonIteration newton = new NewtonIteration(polynomial);

            Console.WriteLine(newton.Polynomial);
            Console.WriteLine(newton.Derivative);

            // for every pixel in image...
            for (int i = 0; i < options.Width; i++)
            {
                for (int j = 0; j < options.Height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = options.YMin + i * yStep;
                    double x = options.XMin + j * xStep;

                    ComplexNumber z = new ComplexNumber()
                    {
                        Real = x,
                        Imaginary = (float)(y)
                    };

                    if (z.Real == 0)
                        z.Real = 0.0001;
                    if (z.Imaginary == 0)
                        z.Imaginary = 0.0001f;

                    // find solution of equation using newton's iteration
                    z = newton.Solve(z, out float iterationCount);

                    // find solution root number
                    var rootIndex = newton.FindRootIndex(z);

                    // colorize pixel according to root number
                    bitmap.SetPixel(j, i, palette.GetColor(rootIndex, iterationCount));
                }
            }
        }
    }
}
