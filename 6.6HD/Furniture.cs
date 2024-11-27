namespace SupplyChainHub
{
    public class Furniture : Product
    {
        private string _material;
        private string _dimensions;
        private double _weight;

        public Furniture(string productID, string productName, decimal price, string material, string dimensions, double weight)
            : base(productID, productName, price)
        {
            _material = material;
            _dimensions = dimensions;
            _weight = weight;
        }

        public override string ProductDetails()
        {
            return $"Furniture: {ProductName} (ID: {ProductID})\nMaterial: {_material}, Dimensions: {_dimensions}, Weight: {_weight}kg, Price: {Price:C}";
        }
    }
}
