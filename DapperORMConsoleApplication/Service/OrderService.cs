using DapperORMConsoleApplication.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperORMConsoleApplication.Service
{
    public class OrderService
    {
        OrderRepository orderRepository;

        public OrderService(string connectionString)
        {
            orderRepository = new OrderRepository(connectionString);
        }

        public void DropOrdersTableIfExists()
        {
            orderRepository.DropOrdersTableIfExists();
        }

        public void CreateOrdersTable()
        {
            orderRepository.CreateOrdersTable();
            Console.WriteLine("Table Orders created successfully!");
        }

        public void InsertOrders()
        {
            Random random = new Random();
            DateTime orderDate;

            for (var i = 0; i <= 5; i++)
            {
                orderDate = DateTime.Now.AddDays(random.Next(0, 10));
                var newOrder = new
                {
                    CustomerId = 1,
                    EmployeeId = 1,
                    OrderDate = orderDate,
                    RequiredDate = orderDate.AddDays(10),
                    ShippedDate = orderDate.AddDays(3),
                    ShipVia = "shipvia",
                    Freight = 3,
                    ShipName = "ship 1",
                    ShipAddress = "address 1",
                    ShipCity = "shipcity 1",
                    ShipRegion = "shipregion 1",
                    ShipPostalCode = "1000",
                    ShipCountry = "country 1"
                };

                orderRepository.InsertOrder(newOrder);
                Console.WriteLine("New order inserted successfully!");
            }
        }

        public void GetAllOrders()
        {
            var orders = orderRepository.GetAllOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Order date: {order.OrderDate}");
            }
        }

        public void SortOrdersByDate()
        {
            Console.WriteLine("Sorted orders by date: ");
            var sortedOrders = orderRepository.SortOrdersByDate();
            foreach (var sortedOrder in sortedOrders)
            {
                Console.WriteLine($"Order ID: {sortedOrder.Id}, Order date: {sortedOrder.OrderDate}");
            }
        }
    }
}
