using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Masha.Foundation;
using static Masha.Foundation.Core;

namespace Masha.FoundExt.Kafka.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "52.172.54.209:9092",
                GroupId = "test-consumer-group",
                //SessionTimeoutMs = 6000,
                //SecurityProtocol = SecurityProtocol.SaslSsl,
                //SaslMechanism = SaslMechanism.ScramSha256,
                //SaslUsername = "etvpw972",
                //SaslPassword = "QQI_FV0VYz_D6zRNlRT3z_7ZtTuEah85"
            };

            KafkaConsumer
                .Init(config)
                .Ready()
                .Consume("test")
                .Subscribe(new ConsoleObserver<string>());
            Console.WriteLine("Press any ke to close");
            Console.ReadKey();
        }
    }
}
