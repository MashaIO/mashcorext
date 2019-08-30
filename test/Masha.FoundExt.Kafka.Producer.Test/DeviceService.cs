namespace Masha.FoundExt.Kafka.Producer.Test
{
    using System;
    using Masha.Foundation;
    using static Masha.Foundation.Core;
    using System.Threading.Tasks;

    public class DeviceService
    {
       
        public Result<Device> CreateDevice(string name)
        {
            var newDevice = new Device
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Generation = 1
            };
            return newDevice;
        }

        public async Task<Result<DeviceRegistered>> Register(IDeviceRepository repository, IEventPublisher pub, string name)
        {
            return await CreateDevice(name)
                .Map(repository.Save)
                .Map(s => new DeviceRegistered
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .Map(pub.Raise);
        }
    }
}
