using Store;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Store Management System!");

        while (true)
        {
            Console.WriteLine("\n1. Add Store");
            Console.WriteLine("2. Search Stores");
            Console.WriteLine("3. Delete Store");
            Console.WriteLine("4. Update Store");
            Console.WriteLine("5. Exit");
            Console.Write("\nEnter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter store name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter store location: ");
                    string location = Console.ReadLine();

                    Console.Write("Enter store type: ");
                    string type = Console.ReadLine();

                    Console.Write("Enter store manager: ");
                    string manager = Console.ReadLine();

                    Console.Write("Enter store opening date (yyyy-mm-dd): ");
                    DateTime openingDate;
                    while (!DateTime.TryParse(Console.ReadLine(), out openingDate))
                    {
                        Console.Write("Invalid date format. Please enter in yyyy-mm-dd format: ");
                    }

                    Console.Write("Enter store categories (separated by commas): ");
                    List<string> categories = Console.ReadLine().Split(',').Select(c => c.Trim()).ToList();

                    StoreManager.AddStore(name, location, type, manager, openingDate, categories);
                    break;

                case "2":
                    Console.WriteLine("Choose search criteria:");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Location");
                    Console.WriteLine("3. Type");
                    Console.WriteLine("4. Manager");
                    Console.WriteLine("5. Categories");
                    Console.Write("Enter the number corresponding to the search criteria: ");
                    string searchCriteriaOption = Console.ReadLine();

                    switch (searchCriteriaOption)
                    {
                        case "1":
                            Console.Write("Enter store name: ");
                            break;
                        case "2":
                            Console.Write("Enter store location: ");
                            break;
                        case "3":
                            Console.Write("Enter store type: ");
                            break;
                        case "4":
                            Console.Write("Enter manager's name: ");
                            break;
                        case "5":
                            Console.Write("Enter category: ");
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }

                    string searchCriteria = Console.ReadLine();
                    StoreManager.SearchStores(searchCriteria);
                    break;

                case "3":
                    Console.Write("Enter store name to delete: ");
                    string storeToDelete = Console.ReadLine();
                    StoreManager.DeleteStore(storeToDelete);
                    break;

                case "4":
                    Console.Write("Enter store name to update: ");
                    string storeToUpdate = Console.ReadLine();

                    Console.Write("Enter new name (leave empty to keep current): ");
                    string newName = Console.ReadLine();

                    Console.Write("Enter new location (leave empty to keep current): ");
                    string newLocation = Console.ReadLine();

                    Console.Write("Enter new type (leave empty to keep current): ");
                    string newType = Console.ReadLine();

                    Console.Write("Enter new manager (leave empty to keep current): ");
                    string newManager = Console.ReadLine();

                    Console.Write("Enter new opening date (yyyy-mm-dd, leave empty to keep current): ");
                    DateTime newOpeningDate;
                    if (!DateTime.TryParse(Console.ReadLine(), out newOpeningDate))
                    {
                        newOpeningDate = DateTime.MinValue;
                    }

                    Console.Write("Enter new categories (separated by commas, leave empty to keep current): ");
                    List<string> newCategories = Console.ReadLine().Split(',').Select(c => c.Trim()).ToList();

                    StoreManager.UpdateStore(storeToUpdate, newName, newLocation, newType, newManager, newOpeningDate, newCategories);
                    break;

                case "5":
                    Console.WriteLine("Exiting the program...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
