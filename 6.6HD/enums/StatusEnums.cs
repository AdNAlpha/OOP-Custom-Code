namespace SupplyChainHub.enums
{
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Canceled
    }

    public enum ShipmentStatus
    {
        NotShipped,
        Shipped,
        InTransit,
        Delivered,
        Returned
    }
}