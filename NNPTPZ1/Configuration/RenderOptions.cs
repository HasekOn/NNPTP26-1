namespace NNPTPZ1.Configuration
{
    public class RenderOptions
    {
        public int Width { get; }
        public int Height { get; }
        public double XMin { get; }
        public double XMax { get; }
        public double YMin { get; }
        public double YMax { get; }
        public string OutputPath { get; }

        public RenderOptions(int width, int height, double xMin, double xMax, double yMin, double yMax, string outputPath)
        {
            Width = width;
            Height = height;
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            OutputPath = outputPath;
        }
    }
}
