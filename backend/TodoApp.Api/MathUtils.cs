namespace TodoApp.Api;

public static class MathUtils
{
    public static double Add(double op1, double op2) => op1 + op2;

    public static double Subtract(double op1, double op2) => op1 - op2;

    public static double Multiply(double op1, double op2) => op1 * op2;

    public static double Divide(double op1, double op2)
    {
        if (op2 == 0)
        {
            throw new ArgumentException("Cannot divide by zero");
        }
        return op1 / op2;
    }

    public static long Factorial(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Factorial is not defined for negative numbers");
        }
        if (n <= 1)
        {
            return 1;
        }
        return n * Factorial(n - 1);
    }
}
