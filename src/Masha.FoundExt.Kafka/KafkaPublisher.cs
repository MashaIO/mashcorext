namespace Masha.FoundExt.Kafka
{
    using System;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class KafkaPublisher : IEventPublisher
    {
        private ProducerBuilder<Null, string> pb;
        private List<string> topics;

        public KafkaPublisher(ProducerConfig pc, IEnumerable<string> topics)
        {
            this.pb = new ProducerBuilder<Null, string>(pc);
            this.topics = new List<string>(topics);
        }

        public KafkaPublisher(ProducerConfig pc, string topic)
        {
            this.pb = new ProducerBuilder<Null, string>(pc);
            this.topics = new List<string>();
            this.topics.Add(topic);
        }

        public async Task<Result<T>> Raise<T>(T @event) where T : Message
        {
            string message = string.Empty;
            try
            {
                message = JsonConvert.SerializeObject(@event);
            }catch(Exception e)
            {
                return Result<T>(Foundation.Error.Of(e));
            }

            using(var producer = this.pb.Build())
            {
                try
                {
                    if (topics.Count > 1)
                    {
                        Parallel.ForEach(this.topics, async t =>
                        {
                            await producer.ProduceAsync(t, new Message<Null, string> { Value = message });
                        });
                    }else
                    {
                        await producer.ProduceAsync(topics[0], new Message<Null, string> { Value = message });
                    }
                }
                catch (ProduceException<Null, string> e)
                {
                    return Result<T>(Foundation.Error.Of((int)e.Error.Code));
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }

            return @event;
        }
    }
}
