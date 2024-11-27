using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChainHub
{
    public class FactoryWarehouse
    {
        public static Warehouse CreateWarehouse(string country, string city)
        {
            return new Warehouse(
                IDGenerator.GenerateWarehouseID(),
                country,
                city);
        }
    }
}
