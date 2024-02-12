using CalculatorLib;
using System.Diagnostics;

namespace CalculatorTest
{
    [TestClass]
    public class CalculatorTest
    {
        private int a, b;
        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            Random rand = new Random();
            a = rand.Next(int.MinValue, int.MaxValue);
            b = rand.Next(int.MinValue, int.MaxValue);
            Debug.WriteLine("Input data.");
            Debug.WriteLine("a = " + a);
            Debug.WriteLine("b = " + b);
        }
        [TestMethod]
        public void TestSumRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int expected = a + b;
            int actual = calculator.Sum(a, b);
            Debug.WriteLine("[Sum] Expected: " + expected);
            Debug.WriteLine("[Sum] Actual: " + actual);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMultiplyRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int expected = a * b;
            int actual = calculator.Multiply(a, b);
            Debug.WriteLine("[Multiply] Expected: " + expected);
            Debug.WriteLine("[Multiply] Actual: " + actual);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestDivideRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            double expected = (double)a / (double)b;
            double actual = calculator.Divide(a, b);
            Debug.WriteLine("[Didide] Expected: " + expected);
            Debug.WriteLine("[Didide] Actual: " + actual);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSubstructRandom()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            int expected = a - b;
            int actual = calculator.Substuct(a, b);
            Debug.WriteLine("[Substruct] Expected: " + expected);
            Debug.WriteLine("[Substruct] Actual: " + actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivideRandomOnZeroException()
        {
            Calculator calculator = new Calculator();
            Random rand = new Random();
            b = 0;
            Debug.WriteLine("[DivideByZeroException test] a = " + a);
            Debug.WriteLine("[DivideByZeroException test] b = " + b);
            double actual = calculator.Divide(a, b);
            Debug.WriteLine("[DivideByZeroException test] Actual = " + b);
        }
    }
}