using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using System.Windows.Threading;

namespace ShapeSharingConsoleSubscribe
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:58607/";

            //string hubName = "ShareHub";
            string hubName = "Test";

            var hubConnection = new HubConnection(url);

            IHubProxy hub = hubConnection.CreateHubProxy(hubName);

            int indexCounter = 0;

            //Subscribe to a hub message
            hub.On<double, double>("MoveShape", (x, y) =>
            {
                Console.WriteLine(string.Format("Coordinates ({0}, {1}) [{2}]", x, y, indexCounter++));

            });

            hubConnection.Start().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("Error: " + task.Exception.ToString());
                    }
                }).Wait();

            Console.ReadLine();
        }
    }
}
