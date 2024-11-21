using System;
using System.Collections.Generic;

class Program
{
    static List<List<object>> kioskInventory = new List<List<object>>
    {
        new List<object> { 1, "Bread", , 50, 80 },
        new List<object> { 2, "Milk", 100, 60 },
        new List<object> { 3, "Yogurt", 75, 150 },
        new List<object> { 4, "Soap", 20, 0.50 },
        new List<object> { 5, "Washing Powder", 30, 2.00 }
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Mr.Benson!");

        while (true)
        {
            Console.WriteLine("What operation are you performing?\n" +
                "1. Record a New Sale\n" +
                "2. Add new item\n" +
                "3. View Monthly Report\n" +
                "4. Search for an item\n" +
                "5. Exit");

            Console.Write("->");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RecordNewSale();
                    break;
                case 2:
                    AddNewItem();
                    break;
                case 3:
                    ViewMonthlyReport();
                    break;
                case 4:
                    SearchForItem();
                    break;
                case 5:
                    Console.WriteLine("Goodbye, Mr.Benson!");
                    return;
                default:
                    Console.WriteLine("Invalid Choice selected");
                    break;
            }
        }
    }

    static void RecordNewSale()
    {
        Console.Write("Enter the name of the item being sold: ");
        string itemName = Console.ReadLine();

        foreach (var item in kioskInventory)
        {
            if (item[1].ToString().ToLower() == itemName.ToLower())
            {
                Console.WriteLine($"Item found: {item[1]} (Quantity: {item[2]}, Price: ${item[3]:0.00})");
                Console.Write("Enter quantity sold: ");
                int quantitySold = int.Parse(Console.ReadLine());

                if ((int)item[2] >= quantitySold)
                {
                    item[2] = (int)item[2] - quantitySold;
                    Console.WriteLine("Sale recorded successfully!");
                }
                else
                {
                    Console.WriteLine("Insufficient stock to complete the sale.");
                }
                return;
            }
        }
        Console.WriteLine("Item not found.");
    }

    static void AddNewItem()
    {
        List<object> newItem = new List<object>();

        // Get the next item ID
        int nextItemId = kioskInventory.Count + 1;

        Console.WriteLine("Adding new item to the inventory...");

        // Add Item ID
        newItem.Add(nextItemId);

        // Get Item Name
        Console.Write("Enter the name of the item: ");
        string itemName = Console.ReadLine();
        newItem.Add(itemName);

        Console.Write("Enter the size/mass of the item");
        int itemSize = int.Parse(Console.ReadLine());
        newItem.Add(itemSize);

        // Get Item Quantity
        Console.Write("Enter the stock of the item: ");
        int itemQuantity;
        while (!int.TryParse(Console.ReadLine(), out itemQuantity))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the quantity: ");
            }
        newItem.Add(itemQuantity);

            // Get Item Price
            Console.Write("Enter the price of the item: ");
            double itemPrice;
            while (!double.TryParse(Console.ReadLine(), out itemPrice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the price: ");
            }
            newItem.Add(itemPrice);

            // Add the new item to the inventory
            kioskInventory.Add(newItem);
    }

static void ViewMonthlyReport()
    {
        // Implement monthly report viewing logic here
    }

    static void SearchForItem()
    {
        Console.Write("Enter the name of the item to search: ");
        string itemName = Console.ReadLine();

        foreach (var item in kioskInventory)
        {
            if (item[1].ToString().ToLower() == itemName.ToLower())
            {
                Console.WriteLine($"Item ID: {item[0]}, Name: {item[1]}, Quantity: {item[2]}, Price: ${item[3]:0.00}");
                return;
            }
        }
        Console.WriteLine("Item not found");
    }
}
