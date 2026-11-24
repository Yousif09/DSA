using System.Net.Http.Headers;
using System.Reflection;
using Spectre.Console;
namespace DSA.StoreApp;





public sealed class StoreApp
{


    // one dimensional array representing all the departments in our store: 
    /* NOTE: The field is static because the same departments belong to the application rather than to an 
     * individual object. It is readonly so teh field cannot later be assigned to a different array. Remember that readonly
     protects the field reference, it does NOT make the array's individual elements immutable.*/ 
    private static readonly string[] Departments =
    {
        "Electronics",
        "Groceries",
        "Clothing",
        "Home",
        "Sports"
    }; 

    // when function is called this goes in a loop until the user exits. 
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
                    ShowDepartments();
                    break;
                case "Exit":
                    isRunning = false;
                    break;
            }
        }

    }

    
    // function to display a nice title to the user at the beginning of our program
    private static void DisplayTitle()
    {
        AnsiConsole.Write(
            new FigletText("Store Management")
            .Centered()
            .Color(Color.Green)); 
    }

    // when a user chooses the case of View Departments, this function is called and they see this:
    /* The loop in this function connects the theory to the feature. Departments.Length controls how many rows
     * we create, Departments[i] reterieves each department, and i+1 gives the user a friendly number beginning at one.
     * Markup.Escape belong to Spectre.Console. It prevents special markup characters inside a deparmtent name from being treated
     as formatting.*/ 
    private static void ShowDepartments()
    {
        AnsiConsole.Clear();

        var table = new Table()
            .Title("[green]Store Departments[/]")
            .AddColumn("Number")
            .AddColumn("Department"); 

            for(int i=0; i < Departments.Length; i++)
        {
            table.AddRow(
                (i + 1).ToString(),
                Markup.Escape(Departments[i]));
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
        Console.ReadKey(intercept: true); 
    }














    // just for practice purposes: 
    public static void PracticeExercises()
    {

        // ARRAYS:
        Console.WriteLine(Departments[0]); // Electronics.
        Console.WriteLine(Departments[^1]); // Sports.

        Departments[0] = "Candy";
        int length = Departments.Length;
        Console.WriteLine(length); // 5

        int homeIndex = Departments.IndexOf("Home");
        Console.WriteLine(homeIndex); // 3

        string[] departmentCopy = new string[Departments.Length];
        Array.Copy(Departments, departmentCopy, Departments.Length);
        Array.Sort(departmentCopy);
        Console.WriteLine("--------------");


        // LISTS: 
        var products = new List<string>();
        products.Add("Wireless mouse");
        products.Add("Coffee Beans");

        Console.WriteLine(products.Count); // 2
        Console.WriteLine(products.Capacity);  // at least 2

        products.AddRange([ "Socks", "Deodorant"]);
        products.Insert(1, "Desk Lamp"); // Wireless Mouse, Desk Lamp, Coffee Beans, Socks, Deodorant. 
        foreach(string product in products)
        {
            Console.WriteLine(product);
        }


        Console.WriteLine(products[0]);  // Wireless Mouse.
        Console.WriteLine(products[^1]);  // Deodorant
        products[0] = "Keyboard"; 
        Console.WriteLine(products[0]);


        bool hasCoffee = products.Contains("Coffee"); // False
        int socksIndex = products.IndexOf("Socks"); // [3]

        /* inside of it is a lambda expression (mini, unnamed function), that defines the search condition. Where
         name is a temporary variable representing each individual item as the loop checks the list, 
        =>: lambda operator, which is read as "such that". name.Length > 5: condition being tested*/
        bool hasLongName = products.Exists(name => name.Length > 5); // True
        string? firstWithS = products.Find(name => name.StartsWith('S')); // Socks


        // removing elements:
        bool removed = products.Remove("Desk Lamp"); // True
        products.RemoveAt(0); // removes element at index 0 
        int removedCount = products.RemoveAll(name => name == "Deodorant"); // 1
       // products.Clear(); // removes everything
        Console.WriteLine("------------");


        foreach(string product in products)
        {
            Console.WriteLine(product);
        }

        products.Sort();
        products.Reverse();

    }
}