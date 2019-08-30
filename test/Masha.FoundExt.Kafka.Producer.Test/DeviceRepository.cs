using Masha.Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Masha.FoundExt.Kafka.Producer.Test
{
    public class DeviceRepository : IDeviceRepository
    {
        public async Task<Result<Device>> Save(Device entity)
        {
            return await Task.FromResult(entity);
        }
    }
}
