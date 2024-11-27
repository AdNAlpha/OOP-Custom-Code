using SupplyChainHub;
using System;

namespace SupplyChainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SupplyChainManager manager = new SupplyChainManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Supply Chain Hub\n");
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Register a Supplier");
                Console.WriteLine("2. Add a Warehouse");
                Console.WriteLine("3. Assign Warehouse to Supplier");
                Console.WriteLine("4. Deallocate supplier warehouse");
                Console.WriteLine("5. Log in");
                Console.WriteLine("6. Exit");
                Console.Write("\nChoose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        manager.RegisterSupplier();
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(); // Pause after the action
                        break;

                    case "2":
                        Console.Clear();
                        manager.AddWarehouse();
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(); // Pause after the action
                        break;

                    case "3":
                        Console.Clear();
                        manager.AssignWarehouseToSupplier();
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(); // Pause after the action
                        break;

                    case "4":
                        Console.Clear();
                        manager.DeallocateWarehouseFromSupplier();
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(); // Pause after the action
                        break;

                    case "5":
                        Console.Clear();
                        Console.Write("Enter Supplier Name: ");
                        string loginSupplierName = Console.ReadLine();

                        Console.Write("Enter Supplier ID: ");
                        string loginSupplierID = Console.ReadLine();

                        Supplier loggedInSupplier = manager.FindSupplierByNameAndId(loginSupplierName, loginSupplierID);

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
                                Console.WriteLine("5. View Supplier Details");
                                Console.WriteLine("6. Log Out");
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


                                    case "2":
                                        loggedInSupplier.AddProduct();
                                        Console.WriteLine("\nPress any key to return to the Supplier Menu...");
                                        Console.ReadKey(); // Pause after adding product
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


                                    case "4":
                                        loggedInSupplier.AddProductToWarehouse();
                                        Console.WriteLine("\nPress any key to return to the Supplier Menu...");
                                        Console.ReadKey();
                                        break;

                                    case "5":
                                        Console.Clear();
                                        loggedInSupplier.ViewSupplierDetails();
                                        Console.WriteLine("\nPress any key to return to the menu...");
                                        Console.ReadKey();
                                        break;

                                    case "6":
                                        Console.WriteLine("Logging out...");
                                        Console.ReadKey();
                                        continueManaging = false;
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
                            Console.WriteLine("\nPress any key to return to the main menu...");
                            Console.ReadKey();
                        }
                        break;

                    case "6":
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
