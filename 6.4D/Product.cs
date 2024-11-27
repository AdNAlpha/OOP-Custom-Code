namespace SupplyChainHub
{
    public abstract class Product
    {
        private string _productID;
        private string _productName;
        private decimal _price;

        public Product(string productID, string productName, decimal price)
        {
            _productID = productID;
            _productName = productName;
            _price = price;
        }

        // Getters
        public string ProductID => _productID;
        public string ProductName => _productName;
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }
                _price = value;
            }
        }

        // Abstract Product Details 
        public abstract string ProductDetails();
    }
}
