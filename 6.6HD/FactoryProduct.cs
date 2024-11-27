using System;

namespace SupplyChainHub
{
    public static class FactoryProduct
    {
        public static Product CreateProduct(
            string productType,
            string productName,
            decimal price,
            string material = null,
            string category = null,
            double weight = 0,
            string powerRating = null,
            int warranty = 0,
            string dimensions = null)
        {
            switch (productType.ToLower())
            {
                case "furniture":
                    if (string.IsNullOrEmpty(material) || string.IsNullOrEmpty(dimensions) || weight <= 0)
                    {
                        throw new ArgumentException("Invalid parameters for furniture.");
                    }
                    return new Furniture(
                        IDGenerator.GenerateProductID("Furniture"),
                        productName,
                        price,
                        material,
                        dimensions,
                        weight);

                case "electronic":
                    if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(powerRating) || warranty <= 0)
                    {
                        throw new ArgumentException("Invalid parameters for electronic.");
                    }
                    return new Electronic(
                        IDGenerator.GenerateProductID("Electronic"),
                        productName,
                        price,
                        category,
                        warranty,
                        powerRating);

                default:
                    throw new ArgumentException("Invalid product type.");
            }
        }
    }
}
