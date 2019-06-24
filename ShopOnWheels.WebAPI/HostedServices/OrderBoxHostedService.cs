using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShopOnWheels.Domain;
using ShopOnWheels.Domain.Models.Box;
using ShopOnWheels.Domain.Models.Order;
using ShopOnWheels.Hubs;
using ShopOnWheels.Services.Stores.BoxStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopOnWheels.WebAPI.HostedServices
{
    public class OrderBoxHostedService : IHostedService
    {
        public IServiceProvider Services { get; }
        private IHubContext<BoxHub> _boxHub;

        public OrderBoxHostedService(IServiceProvider services, IHubContext<BoxHub> boxHub)
        {
            Services = services;
            _boxHub = boxHub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start");
            Tick();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("End");
            return Task.CompletedTask;
        }

        public void Tick()
        {
            TimerCallback tm = new TimerCallback(CheckOrders);

            Timer timer = new Timer(tm, 0, 1000 * 10, 1000 * 30);
        }

        private void CheckOrders(object obj)
        {
            using (var scope = Services.CreateScope())
            {
                var boxStore = scope.ServiceProvider
                        .GetRequiredService<IBoxStore>();

                var context =
                    scope.ServiceProvider
                        .GetRequiredService<ShopOnWheelsDbContext>();

                List<Order> orders = context.Orders.Include(o => o.ProductList).ThenInclude(pl => pl.Product).Where(o => o.OrderDeliver.Day == DateTime.Today.Day && o.Frequency != null).ToList();

                var isUpdatedBoxes = false;
                var isAddedBoxes = false;

                foreach (var o in orders)
                {
                    foreach (var pl in o.ProductList)
                    {
                        var box = boxStore.GetBoxByProductListId(pl.Id);

                        if (box == null)
                        {
                            isAddedBoxes = true;
                            Box model = new Box() { ProductList = pl, Weight = pl.Product.Weight };

                            context.Boxes.Add(model);
                        } 
                        else if (box.Weight == 0)
                        {
                            isUpdatedBoxes = true;
                            box.Weight = pl.Product.Weight.Value;
                            context.Boxes.Update(box);
                        }
                    } 
                    o.OrderDeliver = o.OrderDeliver.AddDays((double)o.Frequency);
                }

                context.Orders.UpdateRange(orders);

                context.SaveChangesAsync();

                var boxes = boxStore.GetBoxes();

                if (isUpdatedBoxes)
                {
                    _boxHub.Clients.All.SendAsync("BoxUpdate", JsonConvert.SerializeObject(boxes));
                    _boxHub.Clients.All.SendAsync("OrdersUpdate", JsonConvert.SerializeObject(orders));
                }
                else if (isAddedBoxes)
                {
                    _boxHub.Clients.All.SendAsync("AddedBoxes", JsonConvert.SerializeObject(boxes));
                }
            }
        }
    }
}
