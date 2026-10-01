using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Tests
{
    [TestClass]
    public class ArgumentParserTests
    {
        private static string[] ValidArguments()
        {
            return new[] { "300", "200", "-2", "1", "-1", "3", "out.png" };
        }

        [TestMethod]
        public void Parse_ValidArguments_ReturnsSettings()
        {
            string[] args = ValidArguments();

            FractalSettings settings = ArgumentParser.Parse(args);

            Assert.AreEqual(300, settings.Width);
            Assert.AreEqual(200, settings.Height);
            Assert.AreEqual(-2.0, settings.XMin);
            Assert.AreEqual(1.0, settings.XMax);
            Assert.AreEqual(-1.0, settings.YMin);
            Assert.AreEqual(3.0, settings.YMax);
            Assert.AreEqual("out.png", settings.OutputPath);
        }

        [TestMethod]
        public void Parse_DecimalInCurrentCulture_ReturnsSettings()
        {
            string[] args = ValidArguments();
            args[2] = (-1.5).ToString(CultureInfo.CurrentCulture);
            args[3] = 1.5.ToString(CultureInfo.CurrentCulture);

            FractalSettings settings = ArgumentParser.Parse(args);

            Assert.AreEqual(-1.5, settings.XMin);
            Assert.AreEqual(1.5, settings.XMax);
        }

        [TestMethod]
        public void Parse_MoreArgumentsThanExpected_IgnoresExtraArguments()
        {
            string[] args = new[] { "300", "200", "-2", "1", "-1", "3", "out.png", "extra" };

            FractalSettings settings = ArgumentParser.Parse(args);

            Assert.AreEqual("out.png", settings.OutputPath);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(6)]
        public void Parse_MissingArguments_ThrowsWithArgumentCount(int count)
        {
            string[] args = new string[count];
            Array.Copy(ValidArguments(), args, count);

            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => ArgumentParser.Parse(args));

            StringAssert.Contains(ex.Message, $"got {count}");
        }

        [TestMethod]
        public void Parse_NullArguments_ThrowsWithArgumentCount()
        {
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => ArgumentParser.Parse(null));

            StringAssert.Contains(ex.Message, "got 0");
        }

        [DataTestMethod]
        [DataRow(0, "abc", "<width>")]
        [DataRow(1, "", "<height>")]
        [DataRow(1, "2.5x", "<height>")]
        [DataRow(2, "abc", "<xmin>")]
        [DataRow(3, "1..5", "<xmax>")]
        [DataRow(4, "y", "<ymin>")]
        [DataRow(5, null, "<ymax>")]
        public void Parse_NonNumericValue_ThrowsNamingArgument(int index, string value, string expectedName)
        {
            string[] args = ValidArguments();
            args[index] = value;

            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => ArgumentParser.Parse(args));

            StringAssert.Contains(ex.Message, expectedName);
        }

        [DataTestMethod]
        [DataRow(0, "0", "<width>")]
        [DataRow(0, "-1", "<width>")]
        [DataRow(1, "0", "<height>")]
        [DataRow(1, "-5", "<height>")]
        public void Parse_NonPositiveSize_ThrowsNamingArgument(int index, string value, string expectedName)
        {
            string[] args = ValidArguments();
            args[index] = value;

            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => ArgumentParser.Parse(args));

            StringAssert.Contains(ex.Message, expectedName);
        }

        [DataTestMethod]
        [DataRow("1", "1", "-1", "3", "<xmin>")]
        [DataRow("2", "1", "-1", "3", "<xmin>")]
        [DataRow("-2", "1", "3", "3", "<ymin>")]
        [DataRow("-2", "1", "4", "3", "<ymin>")]
        public void Parse_MinNotLessThanMax_ThrowsNamingArgument(string xMin, string xMax, string yMin, string yMax, string expectedName)
        {
            string[] args = ValidArguments();
            args[2] = xMin;
            args[3] = xMax;
            args[4] = yMin;
            args[5] = yMax;

            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => ArgumentParser.Parse(args));

            StringAssert.Contains(ex.Message, expectedName);
        }
    }
}
