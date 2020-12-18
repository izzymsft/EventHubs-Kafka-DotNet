using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace SweetStreams.Services
{

    public class Consumer2
    {
        public int requests {get; set; }

        private IConsumer<long, string> consumer;

        private string topic;

        public Consumer2(string brokerList, string connStr, string topic, string cacertlocation, String consumerGroup)
        {
            this.requests = 16;

            var config = new ConsumerConfig
            {
                BootstrapServers = brokerList,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SocketTimeoutMs = 60000,                //this corresponds to the Consumer config `request.timeout.ms`
                SessionTimeoutMs = 30000,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = connStr,
                SslCaLocation = cacertlocation,
                GroupId = consumerGroup,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                BrokerVersionFallback = "1.0.0",        //Event Hubs for Kafka Ecosystems supports Kafka v1.0+, a fallback to an older API will fail
                //Debug = "security,broker,protocol"    //Uncomment for librdkafka debugging information
            };

            this.topic = topic;

            this.consumer = new ConsumerBuilder<long, string>(config).SetKeyDeserializer(Deserializers.Int64).SetValueDeserializer(Deserializers.Utf8).Build();

            Console.WriteLine("Consuming messages from topic: " + topic + ", broker(s): " + brokerList);
        }

        public void run() {

            try
            {
                this.runAux();

            } catch (Exception e) {

                Console.WriteLine(string.Format("Exception Occurred - {0}", e.Message));
            }
        }


        /*
        * @TODO remove cancellation tokens
        */
        private void runAux() {

            int i = 1;

            consumer.Subscribe(topic);

            while (i <= this.requests)
            {
                Console.WriteLine($"Attempting Request Number " + i);

                try
                {
                    var msg = consumer.Consume();

                    var value = msg.Message.Value;

                    //Console.WriteLine($"Received: '{msg.Value}'");
                    Console.WriteLine($"Received:" + value);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Consume error: {e.Error.Reason}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }

                i++;
            }
        }
    }
}