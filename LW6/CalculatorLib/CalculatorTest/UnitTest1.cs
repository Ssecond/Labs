using CalculatorLib;

namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSumRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int a = rand.Next(int.MinValue, int.MaxValue);
            int b = rand.Next(int.MinValue, int.MaxValue);
            int expend = a + b;
            int actual = calculator.Sum(a, b);
            Assert.AreEqual(expend, actual);
        }
        [TestMethod]
        public void TestMultiplyRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int a = rand.Next(int.MinValue, int.MaxValue);
            int b = rand.Next(int.MinValue, int.MaxValue);
            int expend = a * b;
            int actual = calculator.Multiply(a, b);
            Assert.AreEqual(expend, actual);
        }
        [TestMethod]
        public void TestDivideRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            double a = rand.Next(int.MinValue, int.MaxValue);
            double b = rand.Next(int.MinValue, int.MaxValue);
            double expend = a / b;
            double actual = calculator.Divide(a, b);
            Assert.AreEqual(expend, actual);
        }
        [TestMethod]
        public void TestSubstructRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int a = rand.Next(int.MinValue, int.MaxValue);
            int b = rand.Next(int.MinValue, int.MaxValue);
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
            double a = rand.Next(int.MinValue, int.MaxValue);
            double b = 0;
            calculator.Divide(a, b);
        }
    }
}