def Add(op1,op2):
    """Return the sum of two numbers."""
    return op1+op2

def Subtract(op1,op2):
    """Subtract the second number from the first."""
    return op1-op2

def Multiply(op1,op2):
    """Return the product of two numbers."""
    return op1*op2

def Divide(op1,op2):
    """Divide the first number by the second, raising ValueError for zero."""
    if op2 == 0:
        raise ValueError("Cannot divide by zero")
    return op1/op2

def Factorial(n):
   """Return the factorial of a non-negative integer using recursion."""
   if n < 0:
       raise ValueError("Factorial is not defined for negative numbers")
   if n == 0 or n == 1:
       return 1
   return n * Factorial(n-1)
