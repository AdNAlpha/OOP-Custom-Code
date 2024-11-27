using System;
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

        public Supplier(string supplierID, string supplierName, int contactNumber)
        {
            _supplierID = supplierID;
            _supplierName = supplierName;
            _contactNumber = contactNumber;
            _products = new List<Product>();
            _warehouses = new List<Warehouse>();
        }

        // Getters
        public string SupplierID => _supplierID;
        public string SupplierName => _supplierName;
        public int ContactNumber => _contactNumber;
        public List<Warehouse> Warehouses => _warehouses;

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

        // Product to catalog
        public void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Add a New Product:");

            Console.Write("Enter Product Type (Furniture/Electronic): ");
            string productType = Console.ReadLine()?.Trim();

            Product product = null;
            string productID;

            // Validate the product type
            if (string.Equals(productType, "Furniture", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter Furniture Details...");

                Console.Write("Product Name: ");
                string productName = Console.ReadLine();

                Console.Write("Enter Product Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
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

                // Generate the product ID using IDGenerator
                productID = IDGenerator.GenerateProductID("Furniture");

                // Create the Furniture product with the generated ID
                product = new Furniture(productID, productName, price, material, dimensions, weight);
            }
            else if (string.Equals(productType, "Electronic", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter Electronic Product Details...");

                Console.Write("Product Name: ");
                string productName = Console.ReadLine();

                Console.Write("Enter Product Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
                {
                    Console.WriteLine("Invalid price. Please try again.");
                    return;
                }

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                Console.Write("Enter Warranty Period (months): ");
                if (!int.TryParse(Console.ReadLine(), out int warrantyPeriod) || warrantyPeriod < 0)
                {
                    Console.WriteLine("Invalid warranty period. Please try again.");
                    return;
                }

                Console.Write("Enter Power Rating: ");
                string powerRating = Console.ReadLine();

                // Generate the product ID using IDGenerator
                productID = IDGenerator.GenerateProductID("Electronic");

                // Create the Electronic product with the generated ID
                product = new Electronic(productID, productName, price, category, warrantyPeriod, powerRating);
            }
            else
            {
                Console.WriteLine("Invalid product type. Please try again.");
                return;
            }

            // Add the product to the product list
            _products.Add(product);
            Console.WriteLine($"\nProduct Added Successfully!\nProduct ID: {productID}\n");
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
    }
}
