using System;

namespace RatioLibrary {

    public class DenominatorException : ArgumentException {
        public DenominatorException(string msg) : base(msg) { }
    }

    public class Ratio {

        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        private static int GCD(int a, int b) {
            return (b == 0) ? a : GCD(b, a % b);
        }

        public Ratio(int numerator, int denominator) {
            if (denominator == 0)
                throw new DenominatorException("Error: Denominator can't be equal to 0");
            int gcd = GCD(numerator, denominator);

            if (gcd > 1) {
                numerator /= gcd;
                denominator /= gcd;
            }

            Numerator = numerator;
            Denominator = denominator;
        }

        public static Ratio operator +(Ratio r) {
            return new Ratio(r.Numerator, r.Denominator);
        }

        public static Ratio operator +(Ratio r1, Ratio r2) {
            int gcd = GCD(r1.Denominator, r2.Denominator);
            return new Ratio((r1.Numerator * (r2.Denominator / gcd)) +
                (r2.Numerator * (r1.Denominator / gcd)), r1.Denominator * r2.Denominator / gcd);
        }

        public static Ratio operator -(Ratio r) {
            return new Ratio(-r.Numerator, r.Denominator);
        }

        public static Ratio operator -(Ratio r1, Ratio r2) {
            int gcd = GCD(r1.Denominator, r2.Denominator);
            return new Ratio((r1.Numerator * (r2.Denominator / gcd)) -
                (r2.Numerator * (r1.Denominator / gcd)), r1.Denominator * r2.Denominator / gcd);
        }

        public static Ratio operator *(Ratio r1, Ratio r2) {
            int gcd1 = GCD(r1.Numerator, r2.Denominator);
            int gcd2 = GCD(r2.Numerator, r1.Denominator);

            return new Ratio(r1.Numerator / gcd1 * r2.Numerator / gcd2,
                r1.Denominator / gcd2 * r2.Denominator / gcd1);
        }

        public static Ratio operator /(Ratio r1, Ratio r2) {
            if (r2.Numerator == 0)
                throw new DenominatorException("Error: Resulting ratio has 0 as denominator");
            return r1 * new Ratio(r2.Denominator, r2.Numerator);
        }

        public double ToDouble() {
            if (Denominator == 0)
                throw new DenominatorException("Error: Denominator can't be equal to 0");
            return (double)Numerator / Denominator;
        }

        public override string ToString() {
            return string.Format("{0}/{1}", Numerator, Denominator);
        }
    }
}
