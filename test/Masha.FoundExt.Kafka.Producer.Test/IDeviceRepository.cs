using Masha.Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Masha.FoundExt.Kafka.Producer.Test
{
    public interface IDeviceRepository
    {
        Task<Result<Device>> Save(Device entity);
    }
}
