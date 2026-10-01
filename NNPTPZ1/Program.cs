using System;
using System.Drawing;

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
            FractalSettings settings;
            try
            {
                settings = ArgumentParser.Parse(args);
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ArgumentParser.Usage);
                Environment.ExitCode = 1;
                return;
            }
            using (Bitmap bmp = new Bitmap(settings.Width, settings.Height))
            {
                NewtonFractalRenderer renderer = new NewtonFractalRenderer(settings, new ColorPalette());
                renderer.Render(bmp);

                bmp.Save(settings.OutputPath);
            }
        }
    }
}
