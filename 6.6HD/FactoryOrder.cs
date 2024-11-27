using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChainHub.enums;

namespace SupplyChainHub
{
    internal class FactoryOrder
    {
        public static Order CreateOrder(Customer customer, Product product, int quantity, Supplier supplier)
        {
            // Create the order and set initial status to "pending"
            return new Order(
                IDGenerator.GenerateOrderID(),  // Generate unique Order ID
                customer,                       // Customer placing the order
                product,                        // Product being ordered
                quantity,                       // Quantity of the product
                OrderStatus.Pending,                      // Initial order status
                supplier                        // Supplier fulfilling the order
            );
        }
    }
}
