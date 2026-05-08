using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App.Application.Topics.Events
{
    public class SmsService
    {
        public SmsService() 
        {
            VideoEncoder videoEncoder = new();

            videoEncoder.VideoEncoded += OnVideoEncoded;
        }

        public void OnVideoEncoded(object sender, EventArgs e)
        {
            Debug.WriteLine("SmsService: sendind sms...");
        }
    }
}
