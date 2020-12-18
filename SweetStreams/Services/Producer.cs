using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace SweetStreams.Services
{

    public class Producer
    {
        public int requests {get; set; }

        private IProducer<long, string> producer;

        private string topic;

        public Producer(string brokerList, string connStr, string topic, string cacertlocation)
        {
            this.requests = 16;

            var config = new ProducerConfig
            {
                BootstrapServers = brokerList,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = connStr,
                SslCaLocation = cacertlocation,
                //Debug = "security,broker,protocol"        //Uncomment for librdkafka debugging information
            };

            this.topic = topic;

            this.producer = new ProducerBuilder<long, string>(config).SetKeySerializer(Serializers.Int64).SetValueSerializer(Serializers.Utf8).Build();
        }

        public void run() {

            try
            {
                this.runAux().Wait();

            } catch (Exception e) {

                Console.WriteLine(string.Format("Exception Occurred - {0}", e.Message));
            }
        }

        private async Task runAux() {

            int messageCount = this.requests;

            for (int x = 0; x <= messageCount; x++)
            {
                long messageKey = x;
                string messageValue = "The square of " + messageKey + " is " + (messageKey * messageKey) + ". Generated at " + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss.ffff");

                var messageContents = new Message<long, string> { Key = messageKey, Value = messageValue };

                var deliveryReport = await producer.ProduceAsync(topic, messageContents);

                Console.WriteLine(string.Format("Message sent Key={0} (value: '{1}')", x, messageValue));
            }
        }
    }
}