using System;
using System.Drawing;

namespace NNPTPZ1.Rendering
{
    public class ColorPalette
    {
        private const int ShadeStepPerIteration = 2;
        private const int MaxColorComponent = 255;

        private readonly Color[] colors = new Color[]
        {
            Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
        };

        public Color GetColor(int rootIndex, int iterationCount)
        {
            var color = colors[rootIndex % colors.Length];
            return Color.FromArgb(Darken(color.R, iterationCount), Darken(color.G, iterationCount), Darken(color.B, iterationCount));
        }

        private static int Darken(int component, int iterationCount)
        {
            return Math.Min(Math.Max(0, component - iterationCount * ShadeStepPerIteration), MaxColorComponent);
        }
    }
}
