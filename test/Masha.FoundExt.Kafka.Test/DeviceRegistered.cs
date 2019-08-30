using Masha.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masha.FoundExt.Kafka.Producer.Test
{
    public class DeviceRegistered : Message
    {
        public DeviceRegistered()
        {
            this.At = DateTime.Now;
            this.By = "Mono";
            this.Id = "M1";
            this.Type = "DeviceRegistered";
        }
    }
}
