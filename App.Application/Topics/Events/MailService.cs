using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App.Application.Topics.Events
{
    public class MailService
    {
        public MailService()
        {
            VideoEncoder videoEncoder = new();
            videoEncoder.VideoEncoded += OnVideoEncoded;
            videoEncoder.VideoEncodedWithEventHandler += OnVideoEncoded;
            videoEncoder.VideoEncodedWithParameter += OnVideoEncodedWithParameter;
            videoEncoder.VideoEncodedWithEventHandlerWithParameter += OnVideoEncodedWithParameter;
        }

        // Matches EventHandler (allows nullable sender)
        public void OnVideoEncoded(object? source, EventArgs e)
        {
            Debug.WriteLine("MailService: sending an email...");
        }

        // Overload to match VideoEncodedEventHandler (parameterless)
        public void OnVideoEncoded()
        {
            // Forward to the EventHandler-compatible overload
            Debug.WriteLine("MailService: sending an email...");
        }

        public void OnVideoEncodedWithParameter(object? source, VideoEventArgs e)
        {
            Debug.WriteLine("MailService: sending an email..." + e.Video);

        }
    }
}
