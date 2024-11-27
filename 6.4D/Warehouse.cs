namespace SupplyChainHub
{
    public class Warehouse
    {
        private string _warehouseID;
        private string _country;
        public string _city;
        private Dictionary<Product, int> _inventoryList;
        
        //Constructor
        public Warehouse(string warehouseID, string country, string city)
        {
            _warehouseID = warehouseID;
            _country = country;
            _city = city;
            _inventoryList = new Dictionary<Product, int>();
        }

        // Getters
        public string WarehouseID => _warehouseID;
        public string Country => _country;
        public string City => _city;
        public string WarehouseDetails => $"Warehouse ID: {WarehouseID}, Location: {City}, {Country}";


        // Add stock of a product to the warehouse
        public void AddStock(Product product, int quantity)
        {
            if (_inventoryList.ContainsKey(product))
            {
                _inventoryList[product] += quantity;
            }
            else
            {
                _inventoryList[product] = quantity;
            }
            Console.WriteLine($"Added {quantity} of {product.ProductName} to warehouse {WarehouseID}.");
        }        

        // View Stock 
        public void ViewWarehouseStock()
        {
            Console.WriteLine($"Stock in Warehouse {WarehouseID}:");

            if (_inventoryList.Count == 0)
            {
                Console.WriteLine("The warehouse is currently empty.");
                return;
            }

            Console.WriteLine($"{"Product Name",-20} {"Product ID",-10} {"Quantity",-10}");
            Console.WriteLine(new string('-', 40));

            foreach (var item in _inventoryList)
            {
                Product product = item.Key;
                int quantity = item.Value;

                Console.WriteLine($"{product.ProductName,-20} {product.ProductID,-10} {quantity,-10}");
            }

            Console.WriteLine(new string('-', 40));
        }

        // Remove stock Entirely 
        public void RemoveWarehouseStock()
        {
            Console.WriteLine("Enter the Product ID to remove:");
            string productId = Console.ReadLine();

            // Find the product in the inventory by its ID
            var product = _inventoryList.Keys.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine("Product not found in the warehouse inventory.");
                return;
            }

            // Remove the product entirely from the inventory
            _inventoryList.Remove(product);
            Console.WriteLine($"Product {product.ProductName} has been completely removed from warehouse {WarehouseID}.");
        }

        // Transfer Stock 
        public void TransferWarehouseStock(Supplier supplier)
        {
            // Check if the supplier has at least 2 warehouses
            if (supplier.Warehouses.Count < 2)
            {
                Console.WriteLine("Transfer not possible. Supplier must be associated with at least 2 warehouses.");
                return;
            }

            Console.WriteLine("Enter the Product ID to transfer:");
            string productId = Console.ReadLine();

            // Find the product in the current warehouse
            var product = _inventoryList.Keys.FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Console.WriteLine("Product not found in warehouse inventory.");
                return;
            }

            Console.WriteLine("Enter the Destination Warehouse ID:");
            string destinationWarehouseID = Console.ReadLine();

            // Validate destination warehouse
            var destinationWarehouse = supplier.Warehouses.FirstOrDefault(w => w.WarehouseID == destinationWarehouseID);

            if (destinationWarehouse == null)
            {
                Console.WriteLine("Destination warehouse not found or not associated with the supplier.");
                return;
            }

            if (destinationWarehouse.WarehouseID == this.WarehouseID)
            {
                Console.WriteLine("Cannot transfer stock to the same warehouse.");
                return;
            }

            Console.WriteLine($"Enter the quantity of {product.ProductName} to transfer:");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Please try again.");
                return;
            }

            if (!_inventoryList.TryGetValue(product, out int currentStock) || currentStock < quantity)
            {
                Console.WriteLine("Insufficient stock in the current warehouse.");
                return;
            }

            // Perform the transfer
            _inventoryList[product] -= quantity;

            if (_inventoryList[product] == 0)
            {
                _inventoryList.Remove(product); // Remove product if quantity becomes zero
            }

            destinationWarehouse.AddStock(product, quantity);

            Console.WriteLine($"Successfully transferred {quantity} of {product.ProductName} to warehouse {destinationWarehouse.WarehouseID}.");
        }
    }
}
