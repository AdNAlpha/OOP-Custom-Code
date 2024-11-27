using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChainHub
{
    public class FactoryCustomer
    {
        public static Customer CreateCustomer(string name, string address, string phone)
        {
            return new Customer(
                IDGenerator.GenerateCustomerID(),
                name,
                address,
                phone);
        }
    }
}
