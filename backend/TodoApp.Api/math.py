def Add(op1,op2):
    #adds two numbers   
    return op1+op2

def Subtract(op1,op2):
    #subtracts second number from first
    return op1-op2

def Multiply(op1,op2):
    #multiplies two numbers
    return op1*op2

def Divide(op1,op2):
    #divides first number by second
    if op2 == 0:
        raise ValueError("Cannot divide by zero")
    return op1/op2

def Factorial(n):
   #Implement using recursion for factorial calculation
   if n < 0:
       raise ValueError("Factorial is not defined for negative numbers")
   if n == 0 or n == 1:
       return 1
   return n * Factorial(n-1)

