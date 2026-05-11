using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.Observer
{
    public abstract class Publisher
    {
        public event EventHandler<string>? VideoPublished;// eventhandler ile observer daki exception yakalanamaz.

        public event Func<string, Task>? VideoPublishedAsync; // async yazip publisher da hata yakalamak icin kendi
        //eventimizi yazariz EventHandler kullanmak yerine

        protected virtual void OnVideoPublished(string title)
        {//async yapmak zorunda kalirsak event handler i, EventHandler in delegate inin geri donus tipi void oldugu icin 
         //exception yakalanamaz ve uygulama coker. exception i observerda manuel yakalamaliyiz.

            try
            {
                VideoPublished?.Invoke(this, title);
            }
            catch(Exception ex) { Debug.WriteLine(ex); }
        }

        public async Task OnVideoPublishedAsync(string message)
        {
            var handlers = VideoPublishedAsync?.GetInvocationList();
            if (handlers == null) return;

            foreach (var handler in handlers)
            {
                try
                {
                    await ((Func<string, Task>)handler)(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Handler hata verdi: {ex.Message}");
                    // Diğer handler'lar çalışmaya devam eder
                }
            }


            //await VideoPublishedAsync(message);// bu sekilde olmasi yanlis olur cunku sadece son handler in Task ini bekler
        }
    }
}
