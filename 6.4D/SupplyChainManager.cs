using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyChainHub
{
    public class SupplyChainManager
    {
        private List<Supplier> _suppliers;
        private List<Warehouse> _warehouses;

        public SupplyChainManager()
        {
            _suppliers = new List<Supplier>();
            _warehouses = new List<Warehouse>();
        }

        // Register a new Supplier
        public void RegisterSupplier()
        {
            Console.Clear();
            Console.WriteLine("Register a New Supplier:");

            Console.Write("Enter Supplier Name: ");
            string name = Console.ReadLine();

            string input;
            int contact = 0;
            bool validContact = false;

            // Loop until a valid contact number is entered
            while (!validContact)
            {
                Console.Write("Enter Contact Number (10 Digits): ");
                input = Console.ReadLine();

                // Check if the contact number is valid (exactly 10 digits)
                if (input.Length != 10 || !input.All(char.IsDigit))
                {
                    Console.WriteLine("Invalid contact number. Please enter exactly 10 digits.");
                }
                else if (int.TryParse(input, out contact))
                {
                    // Successfully parsed
                    validContact = true;
                }
                else
                {
                    Console.WriteLine("Invalid contact number. Please enter a valid 10-digit number.");
                }
            }

            // Generate unique Supplier ID
            string supplierID = IDGenerator.GenerateSupplierID();
            Supplier newSupplier = new Supplier(supplierID, name, contact);

            _suppliers.Add(newSupplier);
            Console.WriteLine($"\nSupplier Registered Successfully!\nSupplier ID: {supplierID}\n");
        }

        // Add a warehouse with user input inside the method
        public void AddWarehouse()
        {
            Console.Clear();
            Console.WriteLine("Add a New Warehouse:");

            Console.Write("Enter Country: ");
            string country = Console.ReadLine();

            Console.Write("Enter City: ");
            string city = Console.ReadLine();

            // Generate unique Warehouse ID
            string warehouseID = IDGenerator.GenerateWarehouseID();
            Warehouse newWarehouse = new Warehouse(warehouseID, country, city);

            _warehouses.Add(newWarehouse);
            Console.WriteLine($"\nWarehouse Added Successfully!\nWarehouse ID: {warehouseID}\n");
        }

        // Assign warehouse to supplier
        public void AssignWarehouseToSupplier()
        {
            Console.Clear();
            Console.WriteLine("Assign Warehouse to Supplier:");

            // Ask for Supplier ID and Supplier Name
            Console.Write("Enter Supplier ID: ");
            string supplierID = Console.ReadLine();

            Console.Write("Enter Supplier Name: ");
            string supplierName = Console.ReadLine();

            // Find the supplier based on ID and name
            Supplier supplier = _suppliers.FirstOrDefault(s => s.SupplierID == supplierID && s.SupplierName.Equals(supplierName, StringComparison.OrdinalIgnoreCase));

            if (supplier == null)
            {
                Console.WriteLine("Supplier not found. Please try again.");
                return;
            }

            // Ask for Warehouse ID
            Console.Write("Enter Warehouse ID: ");
            string warehouseID = Console.ReadLine();

            // Find the warehouse based on Warehouse ID
            Warehouse warehouse = _warehouses.FirstOrDefault(w => w.WarehouseID == warehouseID);
            if (warehouse == null)
            {
                Console.WriteLine("Warehouse not found. Please try again.");
                return;
            }

            // Check if the warehouse is already assigned to the supplier
            if (supplier.Warehouses.Contains(warehouse))
            {
                Console.WriteLine($"Warehouse {warehouse.WarehouseID} is already assigned to supplier {supplier.SupplierID}.");
                return;
            }

            // Assign the warehouse to the supplier
            supplier.AddWarehouse(warehouse);
            Console.WriteLine($"Warehouse {warehouse.WarehouseID} successfully assigned to supplier {supplier.SupplierID}.");
        }

        // Find the Supplier And Warehouse by ID 
        public Supplier FindSupplierByNameAndId(string supplierName, string supplierID)
        {
            return _suppliers.FirstOrDefault(s =>
                s.SupplierID == supplierID &&
                s.SupplierName.Equals(supplierName, StringComparison.OrdinalIgnoreCase));
        }

        public void DeallocateWarehouseFromSupplier()
        {
            Console.Clear();
            Console.WriteLine("Deallocate Warehouse from Supplier:");

            // Ask for Supplier ID and Supplier Name
            Console.Write("Enter Supplier ID: ");
            string supplierID = Console.ReadLine();

            Console.Write("Enter Supplier Name: ");
            string supplierName = Console.ReadLine();

            // Find the supplier based on ID and name
            Supplier supplier = _suppliers.FirstOrDefault(s => s.SupplierID == supplierID && s.SupplierName.Equals(supplierName, StringComparison.OrdinalIgnoreCase));

            if (supplier == null)
            {
                Console.WriteLine("Supplier not found. Please try again.");
                return;
            }

            // Ask for Warehouse ID
            Console.Write("Enter Warehouse ID: ");
            string warehouseID = Console.ReadLine();

            // Find the warehouse based on Warehouse ID
            Warehouse warehouse = _warehouses.FirstOrDefault(w => w.WarehouseID == warehouseID);

            if (warehouse == null)
            {
                Console.WriteLine("Warehouse not found. Please try again.");
                return;
            }

            // Check if the warehouse is assigned to the supplier
            if (!supplier.Warehouses.Contains(warehouse))
            {
                Console.WriteLine($"Warehouse {warehouse.WarehouseID} is not assigned to supplier {supplier.SupplierID}.");
                return;
            }

            // Deallocate the warehouse from the supplier
            supplier.RemoveWarehouse(warehouse);
            Console.WriteLine($"Warehouse {warehouse.WarehouseID} successfully deallocated from supplier {supplier.SupplierID}.");
        }


        // View catalog for a specific supplier
        public void ViewCatalog(Supplier supplier)
        {
            Console.Clear();
            Console.WriteLine($"Viewing catalog for supplier {supplier.SupplierID}:");

            if (supplier.GetProducts().Count == 0)
            {
                Console.WriteLine("No products in catalog.");
                return;
            }
        }
        
    }
}
