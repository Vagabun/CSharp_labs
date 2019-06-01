using NUnit.Framework;

namespace RatioLibrary.Tests {

    [TestFixture]
    public class Tests {

        [TestCase(7, 8, 7, 8)]
        [TestCase(3, 4, 3, 4)]
        [TestCase(1, 3, 1, 3)]
        public void CreateRatio(int n, int d, int resN, int resD) {
            Ratio r = new Ratio(n, d);
            Assert.Multiple(() => {
                Assert.AreEqual(r.Numerator, resN);
                Assert.AreEqual(r.Denominator, resD);
            });
        }

        [Test]
        public void ExceptionAtCreate() => Assert.Throws<DenominatorException>(() => new Ratio(3, 0));

        [Test]
        public void ExceptionAtCalculate() => Assert.Throws<DenominatorException>(() => {
            var result = new Ratio(3, 4) / new Ratio(0, 5);
        });

        [TestCase(8, 10, 4, 5)]
        [TestCase(21, 7, 3, 1)]
        [TestCase(32, 64, 1, 2)]
        public void Reduction(int n, int d, int resN, int resD) {
            Ratio r = new Ratio(n, d);
            Assert.Multiple(() => {
                Assert.AreEqual(r.Numerator, resN);
                Assert.AreEqual(r.Denominator, resD);
            });
        }

        [TestCase(8, 10, ExpectedResult = "4/5")]
        [TestCase(21, 7, ExpectedResult = "3/1")]
        [TestCase(32, 64, ExpectedResult = "1/2")]
        public string ToStringMethod(int n, int d) => new Ratio(n, d).ToString();

        [TestCase(8, 10, ExpectedResult = 0.8)]
        [TestCase(3, 4, ExpectedResult = 0.75)]
        [TestCase(32, 64, ExpectedResult = 0.5)]
        public double ToDoubleMethod(int n, int d) => new Ratio(n, d).ToDouble();

        [Test]
        public void Addition() {
            Ratio r1 = new Ratio(1, 4);
            Ratio r2 = new Ratio(2, 6);
            Ratio result = r1 + r2;
            Assert.Multiple(() => {
                Assert.AreEqual(result.Numerator, 7);
                Assert.AreEqual(result.Denominator, 12);
            });
        }

        [Test]
        public void Subtraction() {
            Ratio r1 = new Ratio(3, 7);
            Ratio r2 = new Ratio(1, 14);
            Ratio result = r1 - r2;
            Assert.Multiple(() => {
                Assert.AreEqual(result.Numerator, 5);
                Assert.AreEqual(result.Denominator, 14);
            });
        }

        [Test]
        public void Multiplication() {
            Ratio r1 = new Ratio(1, 2);
            Ratio r2 = new Ratio(1, 3);
            Ratio result = r1 * r2;
            Assert.Multiple(() => {
                Assert.AreEqual(result.Numerator, 1);
                Assert.AreEqual(result.Denominator, 6);
            });
        }

        [Test]
        public void Division() {
            Ratio r1 = new Ratio(1, 4);
            Ratio r2 = new Ratio(1, 2);
            Ratio result = r1 / r2;
            Assert.Multiple(() => {
                Assert.AreEqual(result.Numerator, 1);
                Assert.AreEqual(result.Denominator, 2);
            });
        }
    }
}