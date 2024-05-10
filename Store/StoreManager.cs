using System.Text.Json;

namespace Store
{
    internal static class StoreManager
    {
        private const string StoreDataFile = "stores.json";

        internal static void AddStore(string name, string location, string type, string manager, DateTime openingDate, List<string> categories)
        {
            List<Store> stores = GetStores();

            // Check if the store already exists
            if (stores.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Store with the same name already exists.");
                return;
            }

            Store newStore = new Store
            {
                Name = name,
                Location = location,
                Type = type,
                Manager = manager,
                OpeningDate = openingDate,
                Categories = categories
            };

            stores.Add(newStore);

            SaveStores(stores);

            Console.WriteLine("Store added successfully.");
        }

        internal static void SearchStores(string searchCriteria)
        {
            List<Store> stores = GetStores();

            var matchingStores = stores.Where(s =>
                s.Name.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                s.Location.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                s.Type.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                s.Manager.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                s.Categories.Any(c => c.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase)));

            if (matchingStores.Any())
            {
                Console.WriteLine("\nSearch Results:");
                foreach (var store in matchingStores)
                {
                    Console.WriteLine($"Name: {store.Name}");
                    Console.WriteLine($"Location: {store.Location}");
                    Console.WriteLine($"Type: {store.Type}");
                    Console.WriteLine($"Manager: {store.Manager}");
                    Console.WriteLine($"Opening Date: {store.OpeningDate:d}");
                    Console.WriteLine("Categories: " + string.Join(", ", store.Categories) + "\n");
                }
            }
            else
            {
                Console.WriteLine("No matching stores found.");
            }
        }

        internal static void DeleteStore(string name)
        {
            List<Store> stores = GetStores();

            Store storeToDelete = stores.FirstOrDefault(s =>
                s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (storeToDelete != null)
            {
                stores.Remove(storeToDelete);
                SaveStores(stores);
                Console.WriteLine("Store deleted successfully.");
            }
            else
            {
                Console.WriteLine("Store not found.");
            }
        }

        internal static void UpdateStore(string name, string newName, string newLocation, string newType, string newManager, DateTime newOpeningDate, List<string> newCategories)
        {
            List<Store> stores = GetStores();

            Store storeToUpdate = stores.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (storeToUpdate != null)
            {
                // Update only the attributes that are provided and leave the rest unchanged
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    storeToUpdate.Name = newName;
                }
                if (!string.IsNullOrWhiteSpace(newLocation))
                {
                    storeToUpdate.Location = newLocation;
                }
                if (!string.IsNullOrWhiteSpace(newType))
                {
                    storeToUpdate.Type = newType;
                }
                if (!string.IsNullOrWhiteSpace(newManager))
                {
                    storeToUpdate.Manager = newManager;
                }
                if (newOpeningDate != DateTime.MinValue)
                {
                    storeToUpdate.OpeningDate = newOpeningDate;
                }
                if (newCategories != null && newCategories.Any())
                {
                    // Concatenate the new categories with the existing ones
                    storeToUpdate.Categories.AddRange(newCategories);
                    // Remove duplicates by converting to HashSet and back to List
                    storeToUpdate.Categories = storeToUpdate.Categories.Distinct().ToList();
                }

                SaveStores(stores);
                Console.WriteLine("Store information updated successfully.");
            }
            else
            {
                Console.WriteLine("Store not found.");
            }
        }



        private static List<Store> GetStores()
        {
            // Check if the data file exists
            if (!File.Exists(StoreDataFile))
            {
                // If file doesn't exist, return an empty list of stores
                return new List<Store>();
            }

            // Read store data from the file
            string jsonData = File.ReadAllText(StoreDataFile);

            // Deserialize the JSON data into a list of stores
            List<Store> stores = JsonSerializer.Deserialize<List<Store>>(jsonData);

            return stores ?? new List<Store>();
        }

        private static void SaveStores(List<Store> stores)
        {
            // Serialize the list of stores to JSON format
            string jsonData = JsonSerializer.Serialize(stores);

            // Write the JSON data to the file
            File.WriteAllText(StoreDataFile, jsonData);
        }
    }
}
