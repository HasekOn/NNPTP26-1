using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class NewtonFractalRenderer
    {
        private readonly FractalSettings settings;
        private readonly ColorPalette palette;

        public NewtonFractalRenderer(FractalSettings settings, ColorPalette palette)
        {
            this.settings = settings;
            this.palette = palette;
        }

        public void Render(Bitmap bmp)
        {
            double xstep = (settings.XMax - settings.XMin) / settings.Width;
            double ystep = (settings.YMax - settings.YMin) / settings.Height;

            List<Cplx> koreny = new List<Cplx>();
            Poly p = new Poly();
            p.Coe.Add(new Cplx() { Re = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx() { Re = 1 });
            Poly pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            // for every pixel in image...
            for (int i = 0; i < settings.Width; i++)
            {
                for (int j = 0; j < settings.Height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = settings.YMin + i * ystep;
                    double x = settings.XMin + j * xstep;

                    Cplx ox = new Cplx()
                    {
                        Re = x,
                        Imaginari = (float)(y)
                    };

                    if (ox.Re == 0)
                        ox.Re = 0.0001;
                    if (ox.Imaginari == 0)
                        ox.Imaginari = 0.0001f;

                    // find solution of equation using newton's iteration
                    ox = FindSolution(p, pd, ox, out float it);

                    // find solution root number
                    var id = FindRootIndex(koreny, ox);

                    // colorize pixel according to root number
                    bmp.SetPixel(j, i, palette.GetColor(id, it));
                }
            }
        }

        private static Cplx FindSolution(Poly p, Poly pd, Cplx ox, out float it)
        {
            it = 0;
            for (int q = 0; q< 30; q++)
            {
                var diff = p.Eval(ox).Divide(pd.Eval(ox));
                ox = ox.Subtract(diff);

                if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
                {
                    q--;
                }
                it++;
            }

            return ox;
        }

        private static int FindRootIndex(List<Cplx> koreny, Cplx ox)
        {
            var known = false;
            var id = 0;
            for (int w = 0; w <koreny.Count;w++)
            {
                if (Math.Pow(ox.Re- koreny[w].Re, 2) + Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= 0.01)
                {
                    known = true;
                    id = w;
                }
            }
            if (!known)
            {
                koreny.Add(ox);
                id = koreny.Count;
            }

            return id;
        }
    }
}
