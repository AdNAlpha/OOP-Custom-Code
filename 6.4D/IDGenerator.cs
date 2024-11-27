﻿using System;

namespace SupplyChainHub
{
    public static class IDGenerator
    {
        private static Random _random = new Random();

        // Generate a unique Supplier ID
        public static string GenerateSupplierID()
        {
            return $"SUP{_random.Next(100, 999):D3}";
        }

        // Generate a unique Product ID
        public static string GenerateProductID(string type) // type String to determine the type of product
        {
            return type switch
            {
                "Furniture" => $"FUR{_random.Next(1000, 9999):D4}",  // Product IDs for Furniture start with FUR
                "Electronic" => $"ELC{_random.Next(1000, 9999):D4}", // Product IDs for Electronics start with ELC
                _ => throw new ArgumentException("Invalid product type")
            };
        }

        // Generate a unique Warehouse ID
        public static string GenerateWarehouseID()
        {
            return $"WRH{_random.Next(100, 999):D3}";
        }
    }
}