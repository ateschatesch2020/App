using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Test.Observer
{
    public class Publisher
    {
        public event EventHandler<string>? Published;

        public void OnPublished(string message)
        {
            Published?.Invoke(this, message);
        }

        public void VideoUpload(string title)
        {
            Published?.Invoke(this, title);
        }
    }
}
