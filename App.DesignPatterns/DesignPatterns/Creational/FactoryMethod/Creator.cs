using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Creational.FactoryMethod
{
    public abstract class Creator
    {
        public abstract Product FactoryMethod();
    }
}
