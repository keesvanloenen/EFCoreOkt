namespace School.ConsoleApp.Menu;

using System;
using System.Collections.Generic;

public record MenuItem(string Text, Action Action);

public static class MenuSystem
{
    public static void Show(string title, Dictionary<string, MenuItem> menu, bool isMainMenu = false)
    {
        string choice;

        do
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            foreach (var menuItem in menu)
            {
                Console.WriteLine($" {menuItem.Key} - {menuItem.Value.Text}");
            }

            Console.WriteLine($" 0 - {(isMainMenu ? "Exit" : "Back")}");
            Console.Write("Choose: ");

            choice = Console.ReadLine() ?? string.Empty;

            if (menu.TryGetValue(choice, out var item))
            {
                Console.WriteLine();
                item.Action();

                // Only show "Press any key" for submenu actions (not main menu actions)
                if (!isMainMenu)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else if (choice != "0")
            {
                Console.WriteLine("Unknown option.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
  
        } while (choice != "0");
    }
}