using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Test.Factory
{
    internal class TestBookCreator : Creator
    {
        public override TestProduct FactoryMethod()
        {
            return new TestBook();
        }
    }
}
