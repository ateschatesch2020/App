using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Test.Factory
{
    internal class TestPenCreator : Creator
    {
        public override TestProduct FactoryMethod()
        {
            return new TestPencil();
        }
    }
}
