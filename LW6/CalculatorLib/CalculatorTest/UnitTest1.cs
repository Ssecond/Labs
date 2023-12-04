using CalculatorLib;

namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        int a, b;
        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            Random rand = new Random();
            a = rand.Next(int.MinValue, int.MaxValue);
            b = rand.Next(int.MinValue, int.MaxValue);
        }
        [TestMethod]
        public void TestSumRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int expend = a + b;
            int actual = calculator.Sum(a, b);
            Assert.AreEqual(expend, actual);
        }
        [TestMethod]
        public void TestMultiplyRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int expend = a * b;
            int actual = calculator.Multiply(a, b);
            Assert.AreEqual(expend, actual);
        }
        [TestMethod]
        public void TestDivideRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            double expend = (double)a / (double)b;
            double actual = calculator.Divide(a, b);
            Assert.AreEqual(expend, actual);
        }
        [TestMethod]
        public void TestSubstructRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int expend = a - b;
            int actual = calculator.Substuct(a, b);
            Assert.AreEqual(expend, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivideRandomOnZeroException()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            b = 0;
            calculator.Divide(a, b);
        }
    }
}