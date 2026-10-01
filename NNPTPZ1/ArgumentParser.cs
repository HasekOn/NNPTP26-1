namespace NNPTPZ1
{
    public static class ArgumentParser
    {
        public static FractalSettings Parse(string[] args)
        {
            int[] intargs = new int[2];
            for (int i = 0; i < intargs.Length; i++)
            {
                intargs[i] = int.Parse(args[i]);
            }
            double[] doubleargs = new double[4];
            for (int i = 0; i < doubleargs.Length; i++)
            {
                doubleargs[i] = double.Parse(args[i + 2]);
            }
            string output = args[6];

            return new FractalSettings(intargs[0], intargs[1], doubleargs[0], doubleargs[1], doubleargs[2], doubleargs[3], output);
        }
    }
}
