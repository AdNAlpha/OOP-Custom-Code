using System;
using SupplyChainHub.enums;
using System.Collections.Generic;

namespace SupplyChainHub
{
    public class Order
    {
        private string _orderID;
        private Customer _customer;
        private List<(Product Product, int Quantity)> _productQuantities;
        private OrderStatus _orderStatus;
        private Supplier _supplier;

        // Updated Constructor to take 6 parameters
        public Order(string orderID, Customer customer, Product product, int quantity, OrderStatus orderStatus, Supplier supplier)
        {
            _orderID = orderID;
            _customer = customer;
            _productQuantities = new List<(Product, int)> { (product, quantity) }; // Initialize with product and quantity
            _orderStatus = orderStatus;  // Set order status (pending)
            _supplier = supplier;        // Set the supplier
        }

        // Getters
        public string OrderID => _orderID;
        public Customer Customer => _customer;
        public Supplier Supplier => _supplier;
        public List<(Product Product, int Quantity)> ProductQuantities => _productQuantities;
        public Shipment Shipment { get; set; }

        public OrderStatus OrderStatus  // Property to get and set the order status
        {
            get => _orderStatus;
            set => _orderStatus = value;
        }

        //Update Status
        public void UpdateStatus(OrderStatus newOrderStatus)
        {
            _orderStatus = newOrderStatus;
            Console.WriteLine($"Order status updated to: {newOrderStatus}");
        }

        // Display Order Total
        public void OrderTotal()
        {
            if (_productQuantities.Count == 0)
            {
                Console.WriteLine("No products in the order.");
                return;
            }

            Console.WriteLine($"Order ID: {OrderID}");
            Console.WriteLine($"Customer: {Customer.CustomerName}, Address: {Customer.CustomerAddress}");
            Console.WriteLine($"Supplier: {Supplier.SupplierName}");

            Console.WriteLine("\nProducts in Order:");
            Console.WriteLine($"{"Product Name",-20} {"Category",-15} {"Quantity",-10} {"Price",-10} {"Total",-10}");
            Console.WriteLine(new string('-', 60));

            decimal totalCost = 0;
            foreach (var (product, quantity) in _productQuantities)
            {
                string category = product is Electronic ? "Electronic" : "Furniture";
                decimal productTotal = product.Price * quantity;
                totalCost += productTotal;

                Console.WriteLine($"{product.ProductName,-20} {category,-15} {quantity,-10} {product.Price,-10:C} {productTotal,-10:C}");
            }

            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Total Order Cost: {totalCost:C}\n");
        }

        // Cancel Order
        public void CancelOrder()
        {
            if (OrderStatus == OrderStatus.Canceled)
            {
                Console.WriteLine("Order is already canceled.");
                return;
            }

            // Release reserved stock for each product
            foreach (var (product, quantity) in ProductQuantities)
            {
                var warehouse = Supplier.Warehouses.FirstOrDefault(w => w.HasReservedStock(product, quantity));
                if (warehouse != null)
                {
                    warehouse.ReleaseReservedStock(product, quantity);
                }
            }

            // Update the order status to Canceled
            UpdateStatus(OrderStatus.Canceled);
            Console.WriteLine($"Order {OrderID} has been canceled.");
        }


    }
}
