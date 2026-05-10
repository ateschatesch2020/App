using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Test.Observer
{
    public class Observer
    {
        public void OnPublished(object? sender, string e)
        {
            Console.WriteLine($"video published and observer observed {e.ToString()}");
        }
    }
}
