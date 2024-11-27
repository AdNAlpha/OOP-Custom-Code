using SupplyChainHub;
using System.Diagnostics;

public class Electronic : Product
{
    private string _category;
    private int _warrantyPeriod;
    private string _powerRating;

    public Electronic(string productID, string productName, decimal price, string category, int warrantyPeriod, string powerRating)
        : base(productID, productName, price)
    {
        _category = category;
        _warrantyPeriod = warrantyPeriod;
        _powerRating = powerRating;
    }

    public override string ProductDetails()
    {
        return $"{ProductName} (ID: {ProductID})\nCategory: {_category}, Warranty: {_warrantyPeriod} months, Power Rating: {_powerRating}, Price: {Price:C}";
    }
}
