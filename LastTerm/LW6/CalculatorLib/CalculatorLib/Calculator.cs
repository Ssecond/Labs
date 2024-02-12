namespace CalculatorLib
{
    public class Calculator
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        public double Divide(double a, double b)
        {
            double result = a / b;
            if (double.IsInfinity(result))
                throw new DivideByZeroException();
            return result;
        }
        public int Substuct(int a, int b)
        {
            return a - b;
        }
    }
}
