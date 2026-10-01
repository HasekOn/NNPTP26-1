using System;

namespace NNPTPZ1.Configuration
{
    public static class ArgumentParser
    {
        public const string Usage = "Usage: NNPTPZ1.exe <width> <height> <xmin> <xmax> <ymin> <ymax> <output>";

        private const int ExpectedArgumentCount = 7;

        public static RenderOptions Parse(string[] args)
        {
            int argumentCount = args == null ? 0 : args.Length;
            if (argumentCount < ExpectedArgumentCount)
            {
                throw new ArgumentException($"Expected {ExpectedArgumentCount} arguments, but got {argumentCount}.");
            }

            int width = ParseInt(args[0], "width");
            int height = ParseInt(args[1], "height");
            double xMin = ParseDouble(args[2], "xmin");
            double xMax = ParseDouble(args[3], "xmax");
            double yMin = ParseDouble(args[4], "ymin");
            double yMax = ParseDouble(args[5], "ymax");
            string output = args[6];

            if (width <= 0)
            {
                throw new ArgumentException($"Argument <width> must be greater than 0, but was {width}.");
            }
            if (height <= 0)
            {
                throw new ArgumentException($"Argument <height> must be greater than 0, but was {height}.");
            }
            if (!(xMin < xMax))
            {
                throw new ArgumentException($"Argument <xmin> must be less than <xmax>, but was {xMin} >= {xMax}.");
            }
            if (!(yMin < yMax))
            {
                throw new ArgumentException($"Argument <ymin> must be less than <ymax>, but was {yMin} >= {yMax}.");
            }

            return new RenderOptions(width, height, xMin, xMax, yMin, yMax, output);
        }

        private static int ParseInt(string value, string name)
        {
            if (!int.TryParse(value, out int result))
            {
                throw new ArgumentException($"Invalid value '{value}' for argument <{name}>: expected an integer.");
            }
            return result;
        }

        private static double ParseDouble(string value, string name)
        {
            if (!double.TryParse(value, out double result))
            {
                throw new ArgumentException($"Invalid value '{value}' for argument <{name}>: expected a number.");
            }
            return result;
        }
    }
}
