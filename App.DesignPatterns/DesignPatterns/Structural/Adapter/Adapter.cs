using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Structural.Adapter
{
    internal class Adapter : ITarget
    {
        public readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }
        public string Request()
        {
            return $"this is {this._adaptee.GetSpecificRequest()}";
        }
    }
}
