using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Structural.Adapter
{
    internal class Adaptee
    { 
        public string GetSpecificRequest()
        {
            return "specific request";
        }
    }
}
