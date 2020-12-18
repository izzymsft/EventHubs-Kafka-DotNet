using System;
using SweetStreams.Services;
using SweetStreams.Models;

namespace SweetStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            var brokerList = "sweetstreams.servicebus.windows.net:9093";
            string connectionString = "Endpoint=sb://sweetstreams.servicebus.windows.net/;SharedAccessKeyName=Izzy;SharedAccessKey=AIE54oZFiGNuz89FIqSjHTOfTcsNhVfMG+kyOnCmKx8=;EntityPath=salmonriver";
            string topic = "salmonriver";
            string cacertlocation = "cacert.pem";
            string consumerGroup = "riverdreams2";

            if (args.Length == 0) {

                Console.WriteLine("No arguments were passed. Exiting");

                Environment.Exit(0);
            }

            String service = args[0].Trim();

            int numberOfRequests = 16;

            if (service.Equals("consumer")) {

                Consumer2 consumer = new Consumer2(brokerList, connectionString, topic, cacertlocation, consumerGroup);

                consumer.requests = numberOfRequests;

                consumer.run();

            } else if (service.Equals("producer")) {

                Producer producer = new Producer(brokerList, connectionString, topic, cacertlocation);

                producer.requests = numberOfRequests;

                producer.run();

            } else {

                Console.WriteLine("No valid arguments were passed. Exiting");
                Console.WriteLine("Usage dotnet run {producer|consumer}");

                Environment.Exit(0);
            }

        }
    }
}
