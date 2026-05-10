using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.Observer
{
    public abstract class Publisher
    {
        public event EventHandler<string>? VideoPublished;

        protected virtual void OnVideoPublished(string title)
        {
            VideoPublished?.Invoke(this, title);
        }
    }
}
