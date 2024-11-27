using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChainHub
{
    public class DataStore
    {  
            public List<Supplier> Suppliers { get; set; } = new List<Supplier>();
            public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
            public List<Customer> Customers { get; set; } = new List<Customer>();
            public List<Order> Orders { get; set; } = new List<Order>();
            public List<Shipment> Shipments { get; set; } = new List<Shipment>();
        
    }
}
