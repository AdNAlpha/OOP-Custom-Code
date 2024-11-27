using SupplyChainHub.enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyChainHub
{
    public class CustomerManager
    {
        private List<Customer> _customers; // List of registered customers
        private List<Order> _orders; // List of all orders

        public CustomerManager()
        {
            _customers = new List<Customer>();
            _orders = new List<Order>();
        }

        // get the peding orders for the customers
        public List<Order> GetPendingOrdersForCustomer(Customer customer)
        {
            // Get all orders for the customer where the status is Pending
            return _orders.Where(o => o.Customer == customer && o.OrderStatus == OrderStatus.Pending).ToList();
        }
        // get all the customers
        public List<Customer> GetAllCustomers()
        {
            return _customers;  // Return the list of customers
        }


        // Register a new customer
        public void RegisterCustomer()
        {
            Console.Clear();
            Console.WriteLine("Register a New Customer:");

            string name, address, phone;

            // Validate Name
            do
            {
                Console.Write("Enter Customer Name: ");
                name = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Customer name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            // Validate Address
            do
            {
                Console.Write("Enter Customer Address: ");
                address = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Customer address cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(address));

            // Validate Phone
            do
            {
                Console.Write("Enter Customer Phone: ");
                phone = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(phone))
                {
                    Console.WriteLine("Customer phone cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(phone));

            // Use FactoryCustomer to create the customer
            Customer customer = FactoryCustomer.CreateCustomer(name, address, phone);

            _customers.Add(customer);
            Console.WriteLine($"\nCustomer Registered Successfully! Customer ID: {customer.CustomerID}");
        }


        public void PlaceOrder(SupplyChainManager supplyChainManager, Customer customer)
        {
            Console.Clear();
            Console.WriteLine("Available Products:\n");

            int productIndex = 1;
            Dictionary<int, (Product Product, Supplier Supplier)> productMap = new Dictionary<int, (Product, Supplier)>();

            // Loop through all suppliers and display their products
            foreach (var currentSupplier in supplyChainManager.GetSuppliers())
            {
                foreach (var product in currentSupplier.GetProducts())
                {
                    Console.WriteLine($"{productIndex}. {product.ProductDetails()} (ID: {product.ProductID}) - Supplier: {currentSupplier.SupplierName}\n");
                    productMap[productIndex] = (product, currentSupplier);
                    productIndex++;
                }
            }

            if (productMap.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            // Ask customer to select a product
            Console.WriteLine("\nEnter the number of the product to add to your order (or 0 to cancel):");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0 || !productMap.ContainsKey(choice))
            {
                Console.WriteLine("Order canceled.");
                return;
            }

            // Get the selected product and supplier
            var (selectedProduct, supplier) = productMap[choice];

            // Ask customer for quantity
            Console.Write($"Enter quantity for {selectedProduct.ProductName}: ");
            int quantity = int.Parse(Console.ReadLine());

            // Check stock availability in the supplier's warehouses
            Warehouse availableWarehouse = supplier.Warehouses
                .FirstOrDefault(w => w.CheckStock(selectedProduct, quantity));

            if (availableWarehouse == null)
            {
                Console.WriteLine($"Insufficient stock of {selectedProduct.ProductName}. Please reduce the quantity or choose another product.");
                return;
            }

            // Reserve stock in the warehouse (not deducting yet)
            availableWarehouse.ReserveStock(selectedProduct, quantity);

            // Use FactoryOrder to create an order instance with 'Pending' status
            var order = FactoryOrder.CreateOrder(customer, selectedProduct, quantity, supplier);

            // Debugging: Print order status after creation
            Console.WriteLine($"Order created with status: {order.OrderStatus}");

            // Add the order to the list of orders
            customer.AddOrder(order);

            Console.WriteLine($"Order placed successfully! Order ID: {order.OrderID}");

            // Display the total cost of the order
            order.OrderTotal();
        }


        // View customer Orders
        public void ViewCustomerOrders(Customer customer)
        {
            Console.Clear();
            Console.WriteLine($"Orders for Customer {customer.CustomerName} ({customer.CustomerID}):");

            var customerOrders = customer.Orders;  // Access the orders directly from the customer object

            if (!customerOrders.Any())
            {
                Console.WriteLine("No orders found.");
                return;
            }

            // Loop through the orders for the customer
            foreach (var order in customerOrders)
            {
                // Get the associated shipment for the order
                Shipment shipment = order.Shipment; // Accessing the shipment related to the order

                // Display Order ID, Order Status, and Shipping Status
                Console.WriteLine($"Order ID: {order.OrderID}, Order Status: {order.OrderStatus}");

                // If shipment exists, call TrackShipment to display shipment details
                if (shipment != null)
                {
                    shipment.TrackShipment(); // Call the TrackShipment method to show shipment details
                }
                else
                {
                    Console.WriteLine("   No shipment details available.");
                }

                // Display the order total
                order.OrderTotal();

                // Line separator for better readability between orders
                Console.WriteLine(new string('-', 60));
            }
        }

        // Cancel Order
        public void CancelOrder(Customer customer)
        {
            Console.Clear();
            Console.WriteLine($"Cancel Order for Customer {customer.CustomerName} ({customer.CustomerID}):\n");

            // Filter and display pending orders for the customer
            var pendingOrders = customer.Orders.Where(o => o.OrderStatus == OrderStatus.Pending).ToList();

            if (!pendingOrders.Any())
            {
                Console.WriteLine("No pending orders found.");
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"{"Order ID",-15} {"Product Name",-20} {"Quantity",-10} {"Status",-15}");
            Console.WriteLine(new string('-', 60));

            // Display each pending order with its details
            foreach (var order in pendingOrders)
            {
                foreach (var (product, quantity) in order.ProductQuantities)
                {
                    Console.WriteLine($"{order.OrderID,-15} {product.ProductName,-20} {quantity,-10} {order.OrderStatus,-15}");
                }
            }

            Console.WriteLine(new string('-', 60));

            // Prompt the customer to enter the Order ID to cancel
            Console.Write("\nEnter the Order ID to cancel: ");
            string orderId = Console.ReadLine();

            // Find the selected order
            var orderToCancel = pendingOrders.FirstOrDefault(o => o.OrderID == orderId);

            if (orderToCancel == null)
            {
                Console.WriteLine("Order not found or does not belong to the customer.");
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            // Call the CancelOrder method from the Order class
            orderToCancel.CancelOrder();

            Console.WriteLine($"Order {orderId} has been successfully canceled.");
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }


        // Find a customer by ID
        public Customer FindCustomerById(string customerId)
        {
            return _customers.FirstOrDefault(c => c.CustomerID == customerId);
        }

    }
}
