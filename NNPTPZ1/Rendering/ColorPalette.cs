using System;
using System.Drawing;

namespace NNPTPZ1.Rendering
{
    public class ColorPalette
    {
        private readonly Color[] colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public Color GetColor(int rootIndex, float iterationCount)
        {
            var color = colors[rootIndex % colors.Length];
            color = Color.FromArgb(color.R, color.G, color.B);
            color = Color.FromArgb(Math.Min(Math.Max(0, color.R - (int)iterationCount * 2), 255), Math.Min(Math.Max(0, color.G - (int)iterationCount * 2), 255), Math.Min(Math.Max(0, color.B - (int)iterationCount * 2), 255));
            return color;
        }
    }
}
