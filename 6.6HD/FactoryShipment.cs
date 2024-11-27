using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChainHub
{
    public class FactoryShipment
    {
        public static Shipment CreateShipment(Order order, DateTime estimatedDelivery) 
        {
            return new Shipment(
                IDGenerator.GenerateShipmentID(),
                order,
                estimatedDelivery);
        }
    }
}
