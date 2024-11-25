using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static string inventoryFilePath = "inventory.txt";
    static string salesReportFilePath = "salesReport.txt";
    static List<List<object>> kioskInventory = new List<List<object>>();
    static double totalRevenue = 0;
    static double totalCOGS = 0;

    static void Main(string[] args)
    {
        LoadInventory();
        LoadSalesData();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Mr. Benson's Kiosk Management System");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Record New Sales");
            Console.WriteLine("2. Add or Update Inventory");
            Console.WriteLine("3. View Inventory");
            Console.WriteLine("4. View Monthly Report (Profit/Loss)");
            Console.WriteLine("5. Reset Sales and Inventory Data");
            Console.WriteLine("6. Exit");

            Console.Write("-> ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RecordNewSales();
                    break;
                case "2":
                    AddOrUpdateInventory();
                    break;
                case "3":
                    ViewInventory();
                    break;
                case "4":
                    ViewMonthlyReport();
                    break;
                case "5":
                    ResetData();
                    break;
                case "6":
                    Console.WriteLine("Goodbye!");
                    SaveInventory();
                    SaveSalesData();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    WaitForInput();
                    break;
            }
        }
    }

    static void RecordNewSales()
    {
        Console.Clear();
        Console.WriteLine("Enter sales for multiple items. Type 'done' when finished.\n");

        while (true)
        {
            Console.Write("Enter the name of the item sold (or 'done' to finish): ");
            string itemName = Console.ReadLine() ?? "";

            if (itemName.ToLower() == "done")
            {
                Console.WriteLine("Sales recorded successfully.");
                WaitForInput();
                return;
            }

            var item = kioskInventory.Find(i => i[1].ToString().ToLower() == itemName.ToLower());
            if (item != null)
            {
                Console.WriteLine($"Item found: {item[1]} (Quantity: {item[2]}, Price: Ksh {item[3]:0.00})");
                Console.Write("Enter quantity sold: ");
                if (int.TryParse(Console.ReadLine(), out int quantitySold) && quantitySold > 0)
                {
                    if ((int)item[2] >= quantitySold)
                    {
                        item[2] = (int)item[2] - quantitySold;
                        double saleAmount = quantitySold * (double)item[3];
                        double cost = quantitySold * (double)item[4];
                        totalRevenue += saleAmount;
                        totalCOGS += cost;

                        LogSale(itemName, quantitySold, saleAmount, cost);
                        Console.WriteLine($"Sale recorded: Ksh {saleAmount:0.00} earned.");

                        if ((int)item[2] == 0)
                        {
                            Console.WriteLine($"Warning: {item[1]} is now out of stock!");
                            Console.Write("Would you like to restock this item immediately? (yes/no): ");
                            if (Console.ReadLine()?.ToLower() == "yes")
                            {
                                RestockItem(item);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient stock to complete the sale.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Try again.");
                }
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }
        }
    }

    static void AddOrUpdateInventory()
    {
        Console.Clear();
        Console.Write("Enter item name: ");
        string name = Console.ReadLine() ?? "";
        var existingItem = kioskInventory.Find(i => i[1].ToString().ToLower() == name.ToLower());

        Console.Write("Enter quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity. Try again.");
            WaitForInput();
            return;
        }

        Console.Write("Enter price per unit (Ksh): ");
        if (!double.TryParse(Console.ReadLine(), out double price) || price <= 0)
        {
            Console.WriteLine("Invalid price. Try again.");
            WaitForInput();
            return;
        }

        Console.Write("Enter cost price per unit (Ksh): ");
        if (!double.TryParse(Console.ReadLine(), out double costPrice) || costPrice <= 0)
        {
            Console.WriteLine("Invalid cost price. Try again.");
            WaitForInput();
            return;
        }

        if (existingItem != null)
        {
            existingItem[2] = (int)existingItem[2] + quantity;
            existingItem[3] = price;
            existingItem[4] = costPrice;
            Console.WriteLine($"Item '{name}' updated successfully.");
        }
        else
        {
            kioskInventory.Add(new List<object> { kioskInventory.Count + 1, name, quantity, price, costPrice });
            Console.WriteLine($"Item '{name}' added successfully.");
        }
        WaitForInput();
    }

    static void RestockItem(List<object> item)
    {
        Console.Write($"Enter the quantity to restock for {item[1]}: ");
        if (int.TryParse(Console.ReadLine(), out int restockQuantity) && restockQuantity > 0)
        {
            item[2] = (int)item[2] + restockQuantity;
            Console.WriteLine($"{item[1]} restocked successfully.");
        }
        else
        {
            Console.WriteLine("Invalid quantity. Restock cancelled.");
        }
    }

    static void ViewInventory()
    {
        Console.Clear();
        Console.WriteLine("Inventory:");
        Console.WriteLine("ID | Name               | Quantity | Price (Ksh) | Cost Price (Ksh)");
        Console.WriteLine("--------------------------------------------------------------");
        foreach (var item in kioskInventory)
        {
            Console.WriteLine($"{item[0],2} | {item[1],-18} | {item[2],8} | {item[3],10:0.00} | {item[4],15:0.00}");
            if ((int)item[2] == 0)
            {
                Console.WriteLine($"Warning: {item[1]} is out of stock! Please restock.");
            }
        }
        WaitForInput();
    }

static void ViewMonthlyReport()
    {
        Console.Clear();
        double profit = totalRevenue - totalCOGS;
        Console.WriteLine("Monthly Report:");
        Console.WriteLine($"Total Revenue: Ksh {totalRevenue:0.00}");
        Console.WriteLine($"Total COGS:    Ksh {totalCOGS:0.00}");
        Console.WriteLine($"Profit/Loss:   Ksh {profit:0.00}");

        if (profit > 0)
        {
            Console.WriteLine("You made a PROFIT this month. Great job!");
        }
        else if (profit < 0)
        {
            Console.WriteLine("You incurred a LOSS this month. Consider adjusting your costs or prices.");
        }
        else
        {
            Console.WriteLine("You broke even this month. Aim for a profit next time!");
        }
        WaitForInput();
    }

    static void ResetData()
    {
        Console.Clear();
        Console.Write("Are you sure you want to reset all data? This cannot be undone. (yes/no): ");
        if (Console.ReadLine()?.ToLower() == "yes")
        {
            kioskInventory.Clear();
            totalRevenue = 0;
            totalCOGS = 0;

            if (File.Exists(inventoryFilePath)) File.Delete(inventoryFilePath);
            if (File.Exists(salesReportFilePath)) File.Delete(salesReportFilePath);

            Console.WriteLine("All data has been reset.");
        }
        else
        {
            Console.WriteLine("Reset cancelled.");
        }
        WaitForInput();
    }

    static void LoadInventory()
    {
        if (File.Exists(inventoryFilePath))
        {
            string[] inventoryData = File.ReadAllLines(inventoryFilePath);
            foreach (string line in inventoryData)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 5)
                {
                    kioskInventory.Add(new List<object>
                    {
                        int.TryParse(parts[0], out int id) ? id : 0,
                        parts[1],
                        int.TryParse(parts[2], out int quantity) ? quantity : 0,
                        double.TryParse(parts[3], out double price) ? price : 0,
                        double.TryParse(parts[4], out double costPrice) ? costPrice : 0
                    });
                }
            }
        }
    }

    static void SaveInventory()
    {
        using StreamWriter writer = new StreamWriter(inventoryFilePath);
        foreach (var item in kioskInventory)
        {
            writer.WriteLine($"{item[0]},{item[1]},{item[2]},{item[3]},{item[4]}");
        }
    }

    static void LoadSalesData()
    {
        if (File.Exists(salesReportFilePath))
        {
            string[] lines = File.ReadAllLines(salesReportFilePath);
            if (lines.Length > 0)
            {
                string[] parts = lines[0].Split(',');
                if (parts.Length >= 2)
                {
                    totalRevenue = double.TryParse(parts[0], out double revenue) ? revenue : 0;
                    totalCOGS = double.TryParse(parts[1], out double cogs) ? cogs : 0;
                }
            }
        }
    }

    static void SaveSalesData()
    {
        using StreamWriter writer = new StreamWriter(salesReportFilePath);
        writer.WriteLine($"{totalRevenue},{totalCOGS}");
    }

    static void LogSale(string itemName, int quantitySold, double saleAmount, double cost)
    {
        using StreamWriter writer = new StreamWriter(salesReportFilePath, true);
        writer.WriteLine($"{DateTime.Now},Sold {quantitySold} x {itemName},Ksh {saleAmount:0.00},Ksh {cost:0.00}");
    }

    static void WaitForInput()
    {
        Console.WriteLine("\nPress Enter to return to the main menu...");
        Console.ReadLine();
    }
}
