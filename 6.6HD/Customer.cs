using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChainHub
{
    public class Customer
    {
        private string _customerID;
        private string _customerName;
        private string _customerAddress;
        private string _customerPhone;
        private List<Order> _orders;

        // Constructor
        public Customer(string customerID, string customerName, string customerAddress, string customerPhone) 
        {
            _customerAddress = customerAddress;
            _customerID = customerID;
            _customerName = customerName;
            _customerAddress = customerAddress;
            _customerPhone = customerPhone;
            _orders = new List<Order>();
        }

        // Getters 
        public string CustomerID => _customerID;
        public string CustomerName => _customerName;
        public string CustomerAddress => _customerAddress;
        public string CustomerPhone => _customerPhone;
        public List<Order> Orders => _orders;

        // Add Order 
        public void AddOrder(Order order)
        {
            _orders.Add(order); 
        }
       

    }
}
