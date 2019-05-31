using NUnit.Framework;
using RatioLibrary;

namespace RatioLibrary.Tests {
    [TestFixture]
    public class Tests {

        private Ratio r1;
        private Ratio r2;

        [TestCase(8, 10, ExpectedResult = 0.8)]
        [TestCase(3, 4, ExpectedResult = 0.75)]
        [TestCase(1, 2, ExpectedResult = 0.5)]
        public double CreateRatio(int n, int d) {
            return new Ratio(n, d).ToDouble();
        }

        [Test]
        public void ExceptionAtCreate() {
            Assert.Throws<DenominatorException>(() => new Ratio(3, 0));
        }

        [Test]
        public void ExceptionAtCalculate() {
            Assert.Throws<DenominatorException>(() => {
                var result = new Ratio(3, 4) / new Ratio(0, 5);
            });
        }

        [TestCase(8, 10, ExpectedResult = "4/5")]
        [TestCase(21, 7, ExpectedResult = "3/1")]
        [TestCase(32, 64, ExpectedResult = "1/2")]
        public string Reduction(int n, int d) {
            return new Ratio(n, d).ToString();
        }

        [Test]
        public void Addition() {
            r1 = new Ratio(1, 4);
            r2 = new Ratio(2, 6);
            Assert.AreEqual("7/12", (r1 + r2).ToString());
        }

        [Test]
        public void Subtraction() {
            r1 = new Ratio(3, 7);
            r2 = new Ratio(1, 14);
            Assert.AreEqual("5/14", (r1 - r2).ToString());
        }

        [Test]
        public void Multiplication() {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 3);
            Assert.AreEqual("1/6", (r1 * r2).ToString());
        }

        [Test]
        public void Division() {
            r1 = new Ratio(1, 4);
            r2 = new Ratio(1, 2);
            Assert.AreEqual("1/2", (r1 / r2).ToString());
        }
    }
}