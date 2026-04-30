using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Testables
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int a, int b)
        {
            if (a == 0 && b == 0)
            {
                return 0;
            }
            else return a + b;
        }

        public int Multiply(int a, int b)
        {
            if(a==0 || b == 0)
            {
                throw new Exception("Multiplication by zero is not allowed.");
            }
            return a * b;
        }
    }
}
