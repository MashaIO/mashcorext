namespace Masha.FoundExt.Kafka
{
    using System;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Masha.Foundation;
    using static Masha.Foundation.Core;

    public class KafkaConsumer
    {
        private ConsumerBuilder<Ignore, string> cb;
        private IConsumer<Ignore, string> consumer;

        private KafkaConsumer(ConsumerConfig conf)
        {
            cb = new ConsumerBuilder<Ignore, string>(conf);
        }

        public static KafkaConsumer Init(ConsumerConfig config)
        {
            return new KafkaConsumer(config);
        }
        public KafkaConsumer Ready()
        {            
            this.consumer = this.cb.Build();
            return this;
        }
        public IObservable<string> Consume(string topic)
        {
            return Observable.Create<string>((o, ct) =>
            {
                return Task.Run(() =>
                {
                    using (this.consumer)
                    {
                        consumer.Subscribe(topic);

                        CancellationTokenSource cts = new CancellationTokenSource();

                        try
                        {
                            while (true)
                            {
                                try
                                {
                                    var cr = consumer.Consume(cts.Token);
                                    o.OnNext(cr.Value);
                                }
                                catch (ConsumeException e)
                                {
                                    o.OnError(e);
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            o.OnCompleted();
                            consumer.Close();
                        }
                    }
                });
            });
        }
    }
}
