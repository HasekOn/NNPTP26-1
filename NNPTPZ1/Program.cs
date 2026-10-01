using System;
using System.Drawing;
using NNPTPZ1.Configuration;
using NNPTPZ1.Rendering;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RenderOptions options;
            try
            {
                options = ArgumentParser.Parse(args);
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ArgumentParser.Usage);
                Environment.ExitCode = 1;
                return;
            }
            using (Bitmap bitmap = new Bitmap(options.Width, options.Height))
            {
                FractalRenderer renderer = new FractalRenderer(options, new ColorPalette());
                renderer.Render(bitmap);

                bitmap.Save(options.OutputPath);
            }
        }
    }
}
