using System.Net.Http.Headers;
using Spectre.Console;
namespace DSA.StoreApp;

public sealed class StoreApp
{
    public void Run()
    {
        bool isRunning = true;
        while(isRunning)
        {
            AnsiConsole.Clear();
            DisplayTitle();

            String choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(5)
                .AddChoices("View Departments", "Exit"));


            switch(choice)
            {
                case "View Departments":
                    ShowDepartmentsPlaceholder();
                    break;
                case "Exit":
                    isRunning = false;
                    break;
            }
        }

    }

    private static void DisplayTitle()
    {
        AnsiConsole.Write(
            new FigletText("Store Management")
            .Centered()
            .Color(Color.Green)); 
    }

    private static void ShowDepartmentsPlaceholder()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow]Departments haven't been implemented yet.[/]");
        AnsiConsole.MarkupLine("[grey]Press any key to return to menu...[/]");
        Console.ReadKey(intercept: true);
    }
}