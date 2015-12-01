using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Topshelf;
using Microsoft.Owin.Host;

namespace Tailspin.Jobs
{
    class Program
    {
        private const string Endpoint = "http://localhost:12346";

        static void Main(string[] args)
        {
            // AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));

            HostFactory.Run(x =>
            {
                x.Service<Initializer>(s =>
                {
                    s.ConstructUsing(name => new Initializer());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription(Properties.Settings.Default.Description);
                x.SetDisplayName(Properties.Settings.Default.DisplayName);
                x.SetServiceName(Properties.Settings.Default.ServiceName);

            });
        }


    }

    class Initializer
    {
        readonly Timer _timer;
        public Initializer()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() {
            
            _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
