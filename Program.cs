using System;
using System.Collections.Generic;

class Program
{
    static List<List<object>> kioskInventory = new List<List<object>>
    {
        new List<object> { 1, "Bread", 50, 1.20 },
        new List<object> { 2, "Milk", 100, 0.80 },
        new List<object> { 3, "Yogurt", 75, 1.50 },
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
        // Implement item adding logic here
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
