using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChainHub
{
    public class FactorySupplier
    {
        // Update the method to accept a CustomerManager instance
        public static Supplier CreateSupplier(string name, int contactNumber)
        {
            // Pass the customerManager to the Supplier constructor
            return new Supplier(
                IDGenerator.GenerateSupplierID(),
                name,
                contactNumber);
        }
    }
}

