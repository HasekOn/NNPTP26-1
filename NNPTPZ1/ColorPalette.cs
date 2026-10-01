using System;
using System.Drawing;

namespace NNPTPZ1
{
    public class ColorPalette
    {
        private readonly Color[] clrs = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public Color GetColor(int id, float it)
        {
            var vv = clrs[id % clrs.Length];
            vv = Color.FromArgb(vv.R, vv.G, vv.B);
            vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));
            return vv;
        }
    }
}
