public static class CollisionChecker
{
    public static string CollisionCheck(IEnumerable<string> items, string targetName)
    {
        var matchingItems = items.Where(item => Path.GetFileName(item) == targetName).ToArray();

        if (matchingItems.Length == 0)
        {
            throw new FileNotFoundException($"No items found with the name '{targetName}'.");
        }

        if (matchingItems.Length == 1)
        {
            return matchingItems[0];
        }

        Console.WriteLine($"Multiple items found with the name '{targetName}':");

        for (int i = 0; i < matchingItems.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {matchingItems[i]}");
        }

        Console.Write("Choose the item by number: ");
        
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= matchingItems.Length)
        {
            return matchingItems[choice - 1];
        }

        throw new InvalidOperationException("Invalid choice.");
    }
}
