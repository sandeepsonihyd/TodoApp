namespace TodoApp.Api;

/// <summary>Provides basic arithmetic operations.</summary>
public static class MathUtils
{
    /// <summary>Returns the sum of two operands.</summary>
    public static double Add(double op1, double op2) => op1 + op2;

    /// <summary>Subtracts the second operand from the first.</summary>
    public static double Subtract(double op1, double op2) => op1 - op2;

    /// <summary>Returns the product of two operands.</summary>
    public static double Multiply(double op1, double op2) => op1 * op2;

    /// <summary>Divides the first operand by the second.</summary>
    /// <exception cref="ArgumentException">The divisor is zero.</exception>
    public static double Divide(double op1, double op2)
    {
        if (op2 == 0)
        {
            throw new ArgumentException("Cannot divide by zero");
        }
        return op1 / op2;
    }

    /// <summary>Calculates the factorial of a non-negative integer recursively.</summary>
    /// <exception cref="ArgumentException">The input is negative.</exception>
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
