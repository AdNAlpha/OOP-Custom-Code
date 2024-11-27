using System;
using SupplyChainHub.enums;
using System.Collections.Generic;

namespace SupplyChainHub
{
    public class Shipment
    {
        private string _shipmentId;
        private Order _order;
        private DateTime _estimatedDelivery;
        private ShipmentStatus _shipmentStatus; // To store shipment status

        // Constructor
        public Shipment(string shipmentID, Order order, DateTime estimatedDelivery)
        {
            _shipmentId = shipmentID;
            _order = order;
            _estimatedDelivery = estimatedDelivery;
            _shipmentStatus = enums.ShipmentStatus.NotShipped; // Initial status when the shipment is created
        }

        // Getters
        public string ShipmentID => _shipmentId;
        public Order Order => _order;
        public DateTime EstimatedDelivery => _estimatedDelivery;

        // get and Set shipment status
        public ShipmentStatus ShipmentStatus 
        {
            get => _shipmentStatus;
            set => _shipmentStatus = value;
        }

        // Method to simulate tracking a shipment based on its status
        public void TrackShipment()
        {
            Console.Clear();
            Console.WriteLine($"Tracking Shipment: {_shipmentId}");
            Console.WriteLine($"Order ID: {_order.OrderID}");
            Console.WriteLine($"Estimated Delivery: {EstimatedDelivery.ToShortDateString()}");
            Console.WriteLine($"Shipment Status: {_shipmentStatus}");
        }

        // Method to update the shipment status
        public void UpdateShipmentStatus(ShipmentStatus newStatus)
        {
            _shipmentStatus = newStatus;
            Console.WriteLine($"Shipment status updated to: {_shipmentStatus}");
        }

       //to change shipment status, could be called when status changes in the system
        public void MarkAsShipped()
        {
            UpdateShipmentStatus(ShipmentStatus.Shipped);
        }
    }
}
