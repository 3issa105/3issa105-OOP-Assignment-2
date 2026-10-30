namespace Part1_ProceduralToOOP;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Object-Oriented Order System (C#)");
        Console.WriteLine("Seed sample data, show a demo, then open the menu.");

        var system = new OrderSystem();
        system.SeedSampleData();
        system.RunDemoScenario();

        system.PrintCustomers();
        system.PrintProducts();
        system.PrintAllOrders();

        Console.WriteLine($"\nPaid sales total after demo: {system.TotalSalesPaidOnly():F2}");

        RunInteractiveMenu(system);
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");
    }

    private static void RunInteractiveMenu(OrderSystem system)
    {
        int choice = -1;
        while (choice != 0)
        {
            PrintMenu();
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Unknown choice.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    system.PrintCustomers();
                    break;
                case 2:
                    system.PrintProducts();
                    break;
                case 3:
                    system.PrintAllOrders();
                    break;
                case 4:
                    Console.Write("Order id: ");
                    if (int.TryParse(Console.ReadLine(), out int orderIdToPrint))
                        system.PrintOrder(orderIdToPrint);
                    break;
                case 5:
                    Console.Write("Order id: ");
                    int.TryParse(Console.ReadLine(), out int newOrderId);
                    Console.Write("Customer id: ");
                    int.TryParse(Console.ReadLine(), out int custId);
                    Console.Write("Date (YYYY-MM-DD): ");
                    string date = Console.ReadLine() ?? string.Empty;
                    system.CreateOrder(newOrderId, custId, date);
                    break;
                case 6:
                    Console.Write("Order id: ");
                    int.TryParse(Console.ReadLine(), out int targetOrderId);
                    Console.Write("Product id: ");
                    int.TryParse(Console.ReadLine(), out int targetProdId);
                    Console.Write("Quantity: ");
                    int.TryParse(Console.ReadLine(), out int qty);
                    system.AddLineToOrder(targetOrderId, targetProdId, qty);
                    break;
                case 7:
                    Console.Write("Order id: ");
                    if (int.TryParse(Console.ReadLine(), out int orderToPay))
                        system.MarkOrderPaid(orderToPay);
                    break;
                case 8:
                    Console.WriteLine($"Paid sales total: {system.TotalSalesPaidOnly():F2}");
                    break;
                case 0:
                    Console.WriteLine("Bye.");
                    break;
                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }
}
