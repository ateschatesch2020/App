using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App.Application.Topics.Events
{
    public class VideoEventArgs : EventArgs
    {
        public Video? Video { get; set; }
    }
    public class VideoEncoder
    {
        public delegate void VideoEncodedEventHandlerWithParameter(object source, VideoEventArgs args);
        public event VideoEncodedEventHandlerWithParameter? VideoEncodedWithParameter;

        public delegate void VideoEncodedEventHandler(object source, EventArgs args);
        public event VideoEncodedEventHandler? VideoEncoded;

        public EventHandler? VideoEncodedWithEventHandler;
        public EventHandler<VideoEventArgs>? VideoEncodedWithEventHandlerWithParameter;

        public void Encode(Video video)
        {
            Debug.WriteLine("Encoding Video..");
            Thread.Sleep(3000);

            OnVideoEncoded();
            OnVideoEncodedWithParameter(video);
        }
        protected virtual void OnVideoEncoded()
        {
            if(VideoEncoded != null) VideoEncoded(this, EventArgs.Empty);
            //if(VideoEncodedWithEventHandler is not null) VideoEncodedWithEventHandler(this, EventArgs.Empty);
            
        }

        protected virtual void OnVideoEncodedWithParameter(Video video)
        {
            if (VideoEncodedWithParameter != null) VideoEncodedWithParameter(this, new VideoEventArgs() { Video = video });
            if(VideoEncodedWithEventHandlerWithParameter is not null) VideoEncodedWithEventHandlerWithParameter(this,
                new VideoEventArgs() { Video = video });
        }
    }
}
