using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.Observer
{
    public abstract class Observer
    {
        public abstract void OnVideoPublished(object? sender, string title);
    }
}
