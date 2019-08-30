namespace Masha.FoundExt.Kafka.Producer.Test
{
    using Masha.Foundation;
    using System;

    public class Device : AggregateRoot
    {
        public string Name { get; set; }
        public int Generation { get; set; }
    }
}
