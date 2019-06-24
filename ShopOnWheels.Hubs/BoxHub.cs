using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ShopOnWheels.Domain.Models.Box;
using ShopOnWheels.Entities.Models.Box;
using ShopOnWheels.Services.Stores.BoxStore;

namespace ShopOnWheels.Hubs
{
    public class BoxHub : Hub
    {
        public IBoxStore _boxStore;

        public BoxHub(IBoxStore boxStore)
        {
            _boxStore = boxStore;
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Send", message);
        }

        public async Task GetBoxes()
        {
            var boxes = _boxStore.GetBoxes();
            await Clients.Caller.SendAsync("GetBoxes", JsonConvert.SerializeObject(boxes));
        }

        public async Task EatFromBox(string res)
        {
            var box = JsonConvert.DeserializeObject<BoxDTO>(res);

            await _boxStore.UpdateBox(box.Id, box);
            await Clients.All.SendAsync("DataSent", JsonConvert.SerializeObject(box));
        }
    }
}
