using System;
using System.IO;
using System.Text.Json;

namespace SupplyChainHub
{
    public static class DataHandler
    {
        private static readonly string FilePath = "data.json";

        // Save DataStore to file
        public static void SaveData(DataStore dataStore)
        {
            try
            {
                string json = JsonSerializer.Serialize(dataStore, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
                Console.WriteLine("Data saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        // Load DataStore from file
        public static DataStore LoadData()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    return JsonSerializer.Deserialize<DataStore>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }

            return null; // Return null if no data is found
        }
    }
}
