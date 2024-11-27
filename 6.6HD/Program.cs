using System;
using SupplyChainHub.enums;
using SupplyChainHub;
using System.Collections.Generic;

namespace SupplyChainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SupplyChainManager supplyChainManager = new SupplyChainManager();
            CustomerManager customerManager = new CustomerManager();

            // All the customers from Customer manager 
            List<Customer> _customers = customerManager.GetAllCustomers();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Supply Chain Hub\n");
                Console.WriteLine("Are you a:\n1. Supplier\n2. Customer\n3. Exit");
                Console.Write("\nChoose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": // Supplier Logic
                        bool inSupplierMenu = true;
                        while (inSupplierMenu)
                        {
                            Console.Clear();
                            Console.WriteLine("Supplier Menu:");
                            Console.WriteLine("1. Register a Supplier");
                            Console.WriteLine("2. Add a Warehouse");
                            Console.WriteLine("3. Assign Warehouse to Supplier");
                            Console.WriteLine("4. Deallocate Supplier Warehouse");
                            Console.WriteLine("5. Log in");
                            Console.WriteLine("6. Back to Main Menu");
                            Console.Write("\nChoose an option: ");

                            switch (Console.ReadLine())
                            {
                                case "1":
                                    supplyChainManager.RegisterSupplier();
                                    Console.WriteLine("\nPress any key to return to the Supplier Menu...");
                                    Console.ReadKey();
                                    break;

                                case "2":
                                    supplyChainManager.AddWarehouse();
                                    Console.WriteLine("\nPress any key to return to the Supplier Menu...");
                                    Console.ReadKey();
                                    break;

                                case "3":
                                    supplyChainManager.AssignWarehouseToSupplier();
                                    Console.WriteLine("\nPress any key to return to the Supplier Menu...");
                                    Console.ReadKey();
                                    break;

                                case "4":
                                    supplyChainManager.DeallocateWarehouseFromSupplier();
                                    Console.WriteLine("\nPress any key to return to the Supplier Menu...");
                                    Console.ReadKey();
                                    break;

                                case "5":
                                    Console.Write("Enter Supplier Name: ");
                                    string supplierName = Console.ReadLine();

                                    Console.Write("Enter Supplier ID: ");
                                    string supplierId = Console.ReadLine();

                                    Supplier loggedInSupplier = supplyChainManager.FindSupplierByNameAndId(supplierName, supplierId);

                                    if (loggedInSupplier != null)
                                    {
                                        bool continueManaging = true;
                                        while (continueManaging)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Welcome {loggedInSupplier.SupplierName}\n");
                                            Console.WriteLine("1. View Catalog");
                                            Console.WriteLine("2. Add Product to Catalog");
                                            Console.WriteLine("3. View Warehouse");
                                            Console.WriteLine("4. Add Product to Warehouse");
                                            Console.WriteLine("5. Create Shipment");
                                            Console.WriteLine("6. View Supplier Details");
                                            Console.WriteLine("7. Log Out");
                                            Console.Write("\nChoose an option: ");

                                            switch (Console.ReadLine())
                                            {
                                                case "1": // View Catalog 
                                                    bool continueCatalog = true;
                                                    while (continueCatalog)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine($"Viewing catalog for supplier {loggedInSupplier.SupplierName} {loggedInSupplier.SupplierID}:");
                                                        if (loggedInSupplier.GetProducts().Count == 0)
                                                        {
                                                            Console.WriteLine("No products in catalog.");
                                                        }
                                                        else
                                                        {
                                                            int counter = 1;
                                                            foreach (var product in loggedInSupplier.GetProducts())
                                                            {
                                                                Console.WriteLine($"{counter++}. {product.ProductName} (ID: {product.ProductID})");
                                                            }
                                                        }

                                                        Console.WriteLine("\nOptions:");
                                                        Console.WriteLine("1. Remove Product");
                                                        Console.WriteLine("2. Update Product Price");
                                                        Console.WriteLine("3. View Product Details");
                                                        Console.WriteLine("4. Return to Supplier Menu");
                                                        Console.Write("\nChoose an option: ");

                                                        switch (Console.ReadLine())
                                                        {
                                                            case "1":
                                                                loggedInSupplier.RemoveProduct();
                                                                break;

                                                            case "2":
                                                                loggedInSupplier.UpdateProductPrice();
                                                                break;

                                                            case "3":
                                                                loggedInSupplier.ViewProductDetails();
                                                                break;

                                                            case "4":
                                                                continueCatalog = false; // Exit catalog view loop
                                                                break;

                                                            default:
                                                                Console.WriteLine("Invalid option. Please try again.");
                                                                break;
                                                        }

                                                        if (continueCatalog)
                                                        {
                                                            Console.WriteLine("\nPress any key to continue...");
                                                            Console.ReadKey();
                                                        }
                                                    }
                                                    break;

                                                case "2": // Add product to Supplier catalog
                                                    loggedInSupplier.AddProduct();
                                                    Console.WriteLine("\nPress any key to continue...");
                                                    Console.ReadKey();
                                                    break;

                                                case "3": // View Warehouse
                                                    if (loggedInSupplier.Warehouses.Count == 0)
                                                    {
                                                        Console.WriteLine("No assigned warehouse. Returning to the menu...");
                                                        Console.ReadKey();
                                                    }
                                                    else
                                                    {
                                                        bool continueWarehouse = true;
                                                        while (continueWarehouse)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Select which warehouse to manage:");
                                                            for (int i = 0; i < loggedInSupplier.Warehouses.Count; i++)
                                                            {
                                                                Console.WriteLine($"{i + 1}. {loggedInSupplier.Warehouses[i].WarehouseDetails}");
                                                            }
                                                            Console.WriteLine($"{loggedInSupplier.Warehouses.Count + 1}. Return to Supplier Menu");

                                                            Console.Write("\nChoose an option: ");
                                                            string input = Console.ReadLine();

                                                            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= loggedInSupplier.Warehouses.Count)
                                                            {
                                                                var selectedWarehouse = loggedInSupplier.Warehouses[choice - 1];
                                                                bool manageWarehouse = true;

                                                                while (manageWarehouse)
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine($"Managing Warehouse: {selectedWarehouse.WarehouseDetails}");
                                                                    Console.WriteLine("\nOptions:");
                                                                    Console.WriteLine("1. View Stock");
                                                                    Console.WriteLine("2. Remove Product from Warehouse");
                                                                    Console.WriteLine("3. Transfer Stock to Another Warehouse");
                                                                    Console.WriteLine("4. Return to Wareouse selection");
                                                                    Console.Write("\nChoose an option: ");

                                                                    switch (Console.ReadLine())
                                                                    {
                                                                        case "1":
                                                                            selectedWarehouse.ViewWarehouseStock();
                                                                            break;

                                                                        case "2":
                                                                            selectedWarehouse.RemoveWarehouseStock();
                                                                            break;

                                                                        case "3":
                                                                            selectedWarehouse.TransferWarehouseStock(loggedInSupplier);
                                                                            break;

                                                                        case "4":
                                                                            manageWarehouse = false; // Exit warehouse management loop
                                                                            break;

                                                                        default:
                                                                            Console.WriteLine("Invalid option. Please try again.");
                                                                            break;
                                                                    }

                                                                    if (manageWarehouse)
                                                                    {
                                                                        Console.WriteLine("Press any key to continue...");
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                            }
                                                            else if (choice == loggedInSupplier.Warehouses.Count + 1)
                                                            {
                                                                continueWarehouse = false; // Exit warehouse loop
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine($"Invalid input. Please enter a number between 1 and {loggedInSupplier.Warehouses.Count + 1}.");
                                                                Console.ReadKey();
                                                            }
                                                        }
                                                    }
                                                    break;


                                                case "4": // Add Product to warehouse 
                                                    loggedInSupplier.AddProductToWarehouse();
                                                    Console.WriteLine("\nPress any key to continue...");
                                                    Console.ReadKey();
                                                    break;

                                                case "5": // Create Shipment
                                                    loggedInSupplier.CreateShipmentForOrder(_customers);
                                                    Console.WriteLine("\nPress any key to continue...");
                                                    Console.ReadKey();
                                                    break;

                                                case "6":
                                                    loggedInSupplier.ViewSupplierDetails();
                                                    Console.WriteLine("\nPress any key to continue...");
                                                    Console.ReadKey();
                                                    break;

                                                case "7":
                                                    continueManaging = false; // Exit supplier submenu
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid option. Please try again.");
                                                    Console.ReadKey();
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Supplier credentials.");
                                        Console.ReadKey();
                                    }
                                    break;

                                case "6":
                                    inSupplierMenu = false; // Exit Supplier menu
                                    break;

                                default:
                                    Console.WriteLine("Invalid option. Please try again.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;

                    case "2": // Customer Logic
                        bool inCustomerMenu = true;
                        while (inCustomerMenu)
                        {
                            Console.Clear();
                            Console.WriteLine("Customer Menu:");
                            Console.WriteLine("1. Register as a Customer");
                            Console.WriteLine("2. Log in as a Customer");
                            Console.WriteLine("3. Back to Main Menu");
                            Console.Write("\nChoose an option: ");

                            switch (Console.ReadLine())
                            {
                                case "1": // Register a customer
                                    customerManager.RegisterCustomer();
                                    Console.WriteLine("\nPress any key to return to the Customer Menu...");
                                    Console.ReadKey();
                                    break;

                                case "2": // Log in as a customer
                                    Console.Write("Enter Customer Name: ");
                                    string customerName = Console.ReadLine();

                                    Console.Write("Enter Customer ID: ");
                                    string customerId = Console.ReadLine();

                                    Customer loggedInCustomer = customerManager.FindCustomerById(customerId);

                                    if (loggedInCustomer != null && loggedInCustomer.CustomerName == customerName)
                                    {
                                        bool continueShopping = true;
                                        while (continueShopping)
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"Welcome {loggedInCustomer.CustomerName}\n");
                                            Console.WriteLine("1. Browse Products");
                                            Console.WriteLine("2. View Orders");
                                            Console.WriteLine("3. Cancel Order");
                                            Console.WriteLine("4. Log Out");
                                            Console.Write("\nChoose an option: ");

                                            switch (Console.ReadLine())
                                            {
                                                case "1": // Browse and place orders
                                                    customerManager.PlaceOrder(supplyChainManager, loggedInCustomer);
                                                    Console.WriteLine("\nPress any key to return to the Customer Menu...");
                                                    Console.ReadKey();
                                                    break;

                                                case "2": // View orders
                                                    customerManager.ViewCustomerOrders(loggedInCustomer);
                                                    Console.WriteLine("\nPress any key to return to the Customer Menu...");
                                                    Console.ReadKey();
                                                    break;

                                                case "3": // View orders
                                                    customerManager.CancelOrder(loggedInCustomer);
                                                    Console.WriteLine("\nPress any key to return to the Customer Menu...");
                                                    Console.ReadKey();
                                                    break;

                                                case "4": // Log out
                                                    continueShopping = false; // Exit customer submenu
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid option. Please try again.");
                                                    Console.ReadKey();
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Customer credentials.");
                                        Console.ReadKey();
                                    }
                                    break;

                                case "3":
                                    inCustomerMenu = false; // Exit Customer menu
                                    break;

                                default:
                                    Console.WriteLine("Invalid option. Please try again.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;

                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
