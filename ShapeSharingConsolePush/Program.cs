using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace ShapeSharingConsolePush
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:58607";

            string hubName = "ShareHub";
            //string hubName = "Test";

            var hubConnection = new HubConnection(url);

            IHubProxy hubProxy = hubConnection.CreateHubProxy(hubName);

            hubConnection.Start().ContinueWith(task1 =>
            {
                double x = 1;
                double direction = 1;
                bool running = true;

                while (running)
                {
                    x += direction;

                    if (x > 100 || x < 1) direction = - direction;

                    hubProxy.Invoke("ShareCoordinates", x, 50.0).ContinueWith(task2 =>
                        {
                            if (task2.IsFaulted)
                            {
                                running = false;
                                Console.WriteLine("Error: " + task2.Exception.ToString());
                                Console.ReadLine();
                            }
                        });
                    System.Threading.Thread.Sleep(5);
                }

            }).Wait();
        }
    }
}
