using System;
using Spectre.Console;
using Functions;
using System.Data.SQLite;

public class Database
{
    private const string connection = "Data Source=tasks.db;Version=3;";
    public static void installDatabase()
    {
        using (var connect = new SQLiteConnection(connection)) 
        {
            connect.Open();
            string createTable = @"CREATE TABLE IF NOT EXISTS Tasks (
                                   Id INTEGER PRIMARY KEY AUTOINCREMENT ,
                                   Description TEXT NOT NULL)";
            var command = new SQLiteCommand(createTable, connect);
            command.ExecuteNonQuery();
        }
    }
}
class Program
{

    public static void Main()
    {
        Database.installDatabase();
        while (true)
        {

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            
            // the tasks menu
            Table menu = new();

            
            menu.AddColumn(new TableColumn("Menu").Width(20).Centered());
            menu.AddRow("1. Add Task");
            menu.AddRow("2. Delete Task");
            menu.AddRow("3. Instagram");
            menu.AddRow("4. Github");
            menu.AddRow("5. Exit");


           

            menu.Border = TableBorder.Rounded;
            // Center align the table
            menu.RightAligned();
            AnsiConsole.Write(menu);
            AnsiConsole.Write(new Markup("[bold darkviolet]Copyright to 3Bosh_Hani[/] ").RightJustified());

            Console.ForegroundColor = ConsoleColor.Magenta;
            Function.DisplayTasks();

            ConsoleKeyInfo pressed = Console.ReadKey() ;
            switch (pressed.Key)
            {
                // add task 
                case ConsoleKey.D1:
                    Function.AddTask(); 
                    break;
                    // delete task
                case ConsoleKey.D2:
                     Function.DeleteTask();
                    break;
                    // instagram
                case ConsoleKey.D3:
                    Function.OpenLink("https://www.instagram.com/3bosh_hani/");
                    break;
                // github
                case ConsoleKey.D4:
                    Function.OpenLink("https://github.com/3boshHani");
                    break;
                // close
                case ConsoleKey.D5:
                    return;
                default:
                    Console.WriteLine("Chosse Now");
                    break;
            }

        }

    }
}
