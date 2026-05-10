using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.Observer
{
    public class SmsSender : Observer
    {
        public override void OnVideoPublished(object? sender, string title)
        {
            Console.WriteLine($"sms: {title}");
        }
    }
}
