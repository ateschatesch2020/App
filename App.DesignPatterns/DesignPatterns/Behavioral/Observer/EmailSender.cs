using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.Observer
{
    public class EmailSender : Observer
    {
        public override void OnVideoPublished(object? sender, string title)
        {
            Console.WriteLine($"email: {title}");
        }
    }
}
