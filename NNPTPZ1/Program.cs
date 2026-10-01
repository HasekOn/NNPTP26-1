using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using System.Threading;
using NNPTPZ1.Mathematics;

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
            FractalSettings settings = ArgumentParser.Parse(args);
            // TODO: add parameters from args?
            using (Bitmap bmp = new Bitmap(settings.Width, settings.Height))
            {
                NewtonFractalRenderer renderer = new NewtonFractalRenderer(settings, new ColorPalette());
                renderer.Render(bmp);

                bmp.Save(settings.OutputPath ?? "../../../out.png");
            }
            //Console.ReadKey();
        }
    }
}
