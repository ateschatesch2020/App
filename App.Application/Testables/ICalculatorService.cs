using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Testables
{
    public interface ICalculatorService
    {
        int Add(int a, int b);

        int Multiply(int a, int b);
    }
}
