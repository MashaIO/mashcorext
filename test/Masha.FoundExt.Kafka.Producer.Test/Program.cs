using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Masha.Foundation;
using static Masha.Foundation.Core;

namespace Masha.FoundExt.Kafka.Producer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Publish().Wait();
            Console.WriteLine("Done.  Press any key to close");
            Console.ReadKey();
        }

        private static async Task Publish()
        {
            var config = new ProducerConfig { BootstrapServers = "52.172.54.209:9092" };
            IEventPublisher publisher = new KafkaPublisher(config, "test");
            IDeviceRepository repo = new DeviceRepository();

            var result = await new DeviceService()
                            .Register(repo, publisher, "Galaxy S10");
            if (result.HasError) Console.WriteLine("Failed");
        }
    }
}
