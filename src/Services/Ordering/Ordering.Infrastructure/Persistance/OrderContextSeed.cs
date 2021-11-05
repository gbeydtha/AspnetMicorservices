using Microsoft.Extensions.Logging;
using Ordering.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order()
                {
                    UserName = "abul.hasan.de",
                    FirstName = "Abul",
                    LastName = "Hasan",
                    EmailAddress = "abul.hasan.de@gmail.com",
                    AddressLine = "Grossweidenmuhlstr. 10",
                    Country = "Germany",
                    TotalPrice = 350
                }
            };
        }
    }
}
