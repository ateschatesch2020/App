using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.Observer
{
    public class YoutubeChannelPublisher : Publisher
    {
        public void UploadVideo(string title)
        {
            Console.WriteLine($"New video uploaded: {title}");

            OnVideoPublished(title);
        }
    }
}
