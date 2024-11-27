using System;
using SupplyChainHub.enums;
using System.Collections.Generic;

namespace SupplyChainHub
{
    public class Supplier
    {
        private string _supplierID;
        private string _supplierName;
        private int _contactNumber;
        private List<Product> _products;
        private List<Warehouse> _warehouses;
        private List<Order> _orders; // HD Additon 
        private List<Shipment> _shipments; // HD Addition


        public Supplier(string supplierID, string supplierName, int contactNumber)
        {
            _supplierID = supplierID;
            _supplierName = supplierName;
            _contactNumber = contactNumber;
            _products = new List<Product>();
            _warehouses = new List<Warehouse>();
            _orders = new List<Order>();
            _shipments = new List<Shipment>();
        }

        // Getters

        public string SupplierID => _supplierID;
        public string SupplierName => _supplierName;
        public int ContactNumber => _contactNumber;
        public List<Warehouse> Warehouses => _warehouses;
        public List<Order> Orders => _orders; // HD
        public List<Shipment> Shipments => _shipments; // HD

        // Add a warehouse to the supplier
        public void AddWarehouse(Warehouse warehouse)
        {
            _warehouses.Add(warehouse);
            Console.WriteLine($"Warehouse {warehouse.WarehouseDetails} added to supplier {SupplierID}.");
        }

        // Remove warehouse from supplier
        public void RemoveWarehouse(Warehouse warehouse)
        {
            if (Warehouses.Contains(warehouse))
            {
                _warehouses.Remove(warehouse);
            }
        }

        // Add product to catalog
        public void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Add a New Product:");

            Console.Write("Enter Product Type (Furniture/Electronic): ");
            string productType = Console.ReadLine()?.Trim().ToLower();

            Product newProduct = null;

            // Validate the product type and gather appropriate input
            switch (productType)
            {
                case "furniture":
                    Console.WriteLine("Enter Furniture Details...");

                    Console.Write("Product Name: ");
                    string furnitureName = Console.ReadLine();

                    Console.Write("Enter Product Price: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal furniturePrice) || furniturePrice < 0)
                    {
                        Console.WriteLine("Invalid price. Please try again.");
                        return;
                    }

                    Console.Write("Enter Material: ");
                    string material = Console.ReadLine();

                    Console.Write("Enter Dimensions (Width x Height x Depth): ");
                    string dimensions = Console.ReadLine();

                    Console.Write("Enter Weight (kg): ");
                    if (!double.TryParse(Console.ReadLine(), out double weight) || weight < 0)
                    {
                        Console.WriteLine("Invalid weight. Please try again.");
                        return;
                    }

                    // Create the furniture product
                    newProduct = FactoryProduct.CreateProduct(
                        productType: "furniture",
                        productName: furnitureName,
                        price: furniturePrice,
                        material: material,
                        dimensions: dimensions,
                        weight: weight);
                    break;

                case "electronic":
                    Console.WriteLine("Enter Electronic Product Details...");

                    Console.Write("Product Name: ");
                    string electronicName = Console.ReadLine();

                    Console.Write("Enter Product Price: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal electronicPrice) || electronicPrice < 0)
                    {
                        Console.WriteLine("Invalid price. Please try again.");
                        return;
                    }

                    Console.Write("Enter Category: ");
                    string category = Console.ReadLine();

                    Console.Write("Enter Warranty Period (months): ");
                    if (!int.TryParse(Console.ReadLine(), out int warranty) || warranty < 0)
                    {
                        Console.WriteLine("Invalid warranty period. Please try again.");
                        return;
                    }

                    Console.Write("Enter Power Rating: ");
                    string powerRating = Console.ReadLine();

                    // Create the electronic product
                    newProduct = FactoryProduct.CreateProduct(
                        productType: "electronic",
                        productName: electronicName,
                        price: electronicPrice,
                        category: category,
                        warranty: warranty,
                        powerRating: powerRating);
                    break;

                default:
                    Console.WriteLine("Invalid product type. Please try again.");
                    return;
            }

            // Add the product to the supplier's catalog
            if (newProduct != null)
            {
                _products.Add(newProduct);
                Console.WriteLine($"\nProduct Added Successfully!\nProduct ID: {newProduct.ProductID}\n");
            }
        }

        // Get products in the supplier's catalog
        public List<Product> GetProducts()
        {
            return _products;
        }

        // Add Product to Warehouse
        public void AddProductToWarehouse()
        {
            Console.Clear();
            Console.WriteLine("Add Product to Warehouse:");

            // Display all products in the catalog
            if (_products.Count == 0)
            {
                Console.WriteLine("No products in catalog. Returning to the menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a product to add to the warehouse:");
            for (int i = 0; i < _products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_products[i].ProductName} (ID: {_products[i].ProductID})");
            }

            int productChoice = -1;
            while (productChoice == -1)
            {
                Console.Write("\nEnter the product number to add: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _products.Count)
                {
                    productChoice = choice - 1;  // Adjust to zero-based index
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid product number.");
                }
            }

            var selectedProduct = _products[productChoice];

            //Display all warehouses assigned to the supplier
            if (_warehouses.Count == 0)
            {
                Console.WriteLine("No warehouses assigned. Returning to the menu...");
                Console.ReadKey();
                return;  // Exit if no warehouses exist
            }

            Console.WriteLine("\nSelect a warehouse to add the product to:");
            for (int i = 0; i < _warehouses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_warehouses[i].WarehouseDetails} (ID: {_warehouses[i].WarehouseID})");
            }

            int warehouseChoice = -1;
            while (warehouseChoice == -1)
            {
                Console.Write("\nEnter the warehouse number to add to: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _warehouses.Count)
                {
                    warehouseChoice = choice - 1;  // Adjust to zero-based index
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid warehouse number.");
                }
            }

            var selectedWarehouse = _warehouses[warehouseChoice];

            //Ask for the quantity to add
            Console.Write("Enter the quantity to add: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Please try again.");
                return;  // Exit if quantity is invalid
            }

            //Add the stock to the warehouse
            selectedWarehouse.AddStock(selectedProduct, quantity); // This is where AddStock method is called

            Console.WriteLine($"Successfully added {quantity} of {selectedProduct.ProductName} to warehouse {selectedWarehouse.WarehouseID}.");
        }


        // View Product Details
        public void ViewProductDetails()
        {
            Console.WriteLine("View Product Details:");

            // Change to string to accept alphanumeric Product IDs like "ELCxxx"
            Console.Write("Enter Product ID: ");
            string productId = Console.ReadLine();
            Product product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.WriteLine($"\n{product.ProductDetails()}");
        }

        // Remove Product from Catalog
        public void RemoveProduct()
        {
            Console.WriteLine("Remove Product from Catalog:");

            // Change to string to accept alphanumeric Product IDs like "ELCxxx"
            Console.Write("Enter Product ID: ");
            string productId = Console.ReadLine();
            Product product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                Console.WriteLine("Product not found in the catalog.");
                return;
            }

            _products.Remove(product);
            Console.WriteLine($"Product {product.ProductName} removed successfully.");
        }

        // Update Product Price
        public void UpdateProductPrice()
        {
            Console.WriteLine("Update Product Price:");

            // Change to string to accept alphanumeric Product IDs like "ELCxxx"
            Console.Write("Enter Product ID: ");
            string productId = Console.ReadLine();
            Product product = _products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                Console.ReadKey(); // Allow user to read the error before returning
                return;
            }

            // Show current price and ask for new price
            Console.WriteLine($"Current Price: {product.Price:C}");
            Console.Write("Enter New Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice) || newPrice < 0)
            {
                Console.WriteLine("Invalid price. Please try again.");
                Console.ReadKey(); // Allow user to read the error before returning
                return;
            }

            // Update the product's price
            product.Price = newPrice;
            Console.WriteLine($"Price of {product.ProductName} updated to {newPrice:C}.");
            Console.WriteLine("Press any key to return to the catalog menu...");
        }

        // view Supplier details 
        public void ViewSupplierDetails()
        {
            Console.Clear();

            // Display Supplier Information
            Console.WriteLine($"Supplier Details for {SupplierName}:");
            Console.WriteLine($"ID: {SupplierID}");
            Console.WriteLine($"Contact Number: {ContactNumber}");

            // Check and display assigned warehouses
            if (Warehouses.Count == 0)
            {
                Console.WriteLine("\nNo warehouses assigned.");
            }
            else
            {
                Console.WriteLine("Assigned Warehouses:");
                foreach (var warehouse in Warehouses)
                {
                    Console.WriteLine(warehouse.WarehouseDetails); // Assuming WarehouseDetails property in Warehouse class
                }
            }
        }

        // HD addition 
        // Create Shipment with access to Customer pending orders
        public void CreateShipmentForOrder(List<Customer> allCustomers)  // Passing list of customers directly
        {
            Console.Clear();
            Console.WriteLine("Create Shipment\n");

            var pendingOrders = new List<Order>();

            // Loop through all customers and collect their pending orders
            foreach (var customer in allCustomers)  // Now we directly access customers
            {
                var customerPendingOrders = customer.Orders.Where(o => o.OrderStatus == OrderStatus.Pending).ToList();
                pendingOrders.AddRange(customerPendingOrders);
            }

            if (!pendingOrders.Any())
            {
                Console.WriteLine("No pending orders available for shipment.");
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pending Orders:");
            for (int i = 0; i < pendingOrders.Count; i++)
            {
                var order = pendingOrders[i];
                Console.WriteLine($"{i + 1}. Order ID: {order.OrderID}, Customer: {order.Customer.CustomerName}, Total Quantity: {order.ProductQuantities.Sum(pq => pq.Quantity)}");

                // Loop through each product in the order and display its name and ID
                foreach (var (product, quantity) in order.ProductQuantities)
                {
                    Console.WriteLine($"   Product Name: {product.ProductName}, Product ID: {product.ProductID}, Quantity: {quantity}");
                }
            }

            // Prompt the supplier to select an order to create a shipment for
            Console.Write("\nSelect an order to create a shipment (Enter number): ");
            if (!int.TryParse(Console.ReadLine(), out int orderIndex) || orderIndex < 1 || orderIndex > pendingOrders.Count)
            {
                Console.WriteLine("Invalid selection. Returning to menu...");
                Console.ReadKey();
                return;
            }

            var selectedOrder = pendingOrders[orderIndex - 1];

            // Ask for the estimated delivery date
            Console.Write("Enter the estimated delivery date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime estimatedDelivery))
            {
                Console.WriteLine("Invalid date format. Returning to menu...");
                Console.ReadKey();
                return;
            }

            // Use the FactoryShipment to create the shipment for the selected order
            Shipment newShipment = FactoryShipment.CreateShipment(selectedOrder, estimatedDelivery);

            // Assign the shipment to the order
            selectedOrder.Shipment = newShipment;

            // Update the order status to "Shipped"
            selectedOrder.UpdateStatus(OrderStatus.Shipped);  // Update order status to Shipped

            // Call MarkAsShipped to update shipment status
            newShipment.MarkAsShipped();  // Update shipment status to Shipped

            // Add the shipment to the supplier's shipment list
            _shipments.Add(newShipment);

            // Display confirmation details
            Console.WriteLine($"Shipment created for Order ID: {selectedOrder.OrderID}. Shipment ID: {newShipment.ShipmentID}");
            Console.WriteLine($"Estimated Delivery: {newShipment.EstimatedDelivery.ToShortDateString()}");
            Console.WriteLine($"Shipment Status: {newShipment.ShipmentStatus}");

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
