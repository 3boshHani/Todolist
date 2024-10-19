using System;
using System.Diagnostics;
using Spectre.Console;
using System.Data.SQLite;


 namespace Functions;
public class Function
{
    // to open links
    public static void OpenLink(string link)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = link,
                UseShellExecute = true
            });
        }
        catch
        {
            Console.WriteLine("Couldn't Open Link");
        }

    }
    public static void DisplayTasks()
{
    using (var connection = new SQLiteConnection("Data Source=tasks.db;Version=3;"))
    {
        connection.Open();
        var command = new SQLiteCommand("SELECT Id, Description FROM Tasks", connection);
        using (var reader = command.ExecuteReader())
        {
            Table tasks = new Table();
            tasks.AddColumn("ID");
            tasks.AddColumn(new TableColumn("Tasks!").Width(40).Centered());
            tasks.Centered();

                while (reader.Read())
            {
                tasks.AddRow(reader["Id"].ToString(), reader["Description"].ToString());
                    tasks.AddEmptyRow();
            }

            AnsiConsole.Write(tasks);
        }
    }
}


    public static void AddTask()
    {
        Console.Clear();
        AnsiConsole.Write(new Markup("[bold darkviolet] Write Your Task ! [/]").Centered());
        AnsiConsole.Write(new Markup(" ").Centered());
        Console.ForegroundColor = ConsoleColor.Magenta;
        string taskDescription = Console.ReadLine()!;

        using (var connection = new SQLiteConnection("Data Source=tasks.db;Version=3;"))
        {
            connection.Open();
            var command = new SQLiteCommand("INSERT INTO Tasks (Description) VALUES (@description)", connection);
            command.Parameters.AddWithValue("@description", taskDescription);
            command.ExecuteNonQuery();
        }
;

    }
    public static void DeleteTask()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Clear();
        Function.DisplayTasks();
        AnsiConsole.Markup("Enter Task ID to delete:");
        string taskId = Console.ReadLine()!;

        // Delete the task with the given ID
        using (var connection = new SQLiteConnection("Data Source=tasks.db;Version=3;"))
        {
            connection.Open();
            var command = new SQLiteCommand("DELETE FROM Tasks WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", taskId);
            int rowsAffected = command.ExecuteNonQuery();
            var checkCommand = new SQLiteCommand("SELECT COUNT(*) FROM Tasks", connection);
            long taskCount = (long)checkCommand.ExecuteScalar();

            if (taskCount == 0)
            {
                var resetCommand = new SQLiteCommand("DELETE FROM sqlite_sequence WHERE name='Tasks'", connection);
                resetCommand.ExecuteNonQuery();
            }
            }
    }
}
