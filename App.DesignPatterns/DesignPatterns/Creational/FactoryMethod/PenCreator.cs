using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Creational.FactoryMethod
{
    public class PenCreator : Creator
    {
        public override Product FactoryMethod()
        {
            return new Pen();
        }
    }
}
