namespace StoichiometryCalculator
{
    public class Calculations
    {
        private const double avagradoNum = 6.02e23;
        private static double moles;

        public static double mass;
        public static double[] molarMasses, coefficients;

        public static void Intermediate()
        {
            double moles1 = mass / molarMasses[0];
            moles = moles1 * (coefficients[1] / coefficients[0]);
        }

        public static double[] Calculate()
        {
            double[] output = new double[3];
            output[0] = ToMoles();
            output[1] = ToMass();
            output[2] = ToParticles();
            return output;
        }

        private static double ToMoles()
        {
            Intermediate();
            return moles;
        }

        private static double ToMass() => moles * molarMasses[1];
        private static double ToParticles() => moles * avagradoNum;
    }
}