using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Creational.FactoryMethod
{
    public class BookCreator : Creator
    {
        public override Product FactoryMethod()
        {
            return new Book();
        }
    }
}
