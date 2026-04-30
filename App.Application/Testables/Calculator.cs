using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Testables
{
    public class Calculator
    {
        private readonly ICalculatorService _calculatorService;
        public Calculator()
        {
            
        }
        public Calculator(ICalculatorService calculatorService) { _calculatorService = calculatorService; }
 
        public int Add(int a, int b)
        {
           return _calculatorService.Add(a, b);
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return _calculatorService.Multiply(a,b);
        }

        public int Divide(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException();
            return a / b;
        }
    }
}
