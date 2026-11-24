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


    /* List that represents the products in the store.
     * NOTE: it is readonly which prevents assigning a different list to Products. The list itself
     remaing mutable, so the application can still call Add and update its Product objects. */
    private static readonly List<Product> Products =
    [
        new Product
        {
            Name = "Wireless Mouse",
            Sku = "ELE-001",
            Price = 24.99m,
            Department = "Electronics",
            StockQuantity = 12
        },

        new Product
        {
            Name = "Coffee Beans",
            Sku = "GRO-004",
            Price = 14.50m,
            Department = "Groceries",
            StockQuantity = 20
        }
    ];

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
                .AddChoices("View Departments", "Manage Products", "Exit"));


            switch(choice)
            {
                case "View Departments":
                    ShowDepartments();
                    break;
                case "Manage Products":
                    ShowProductMenu();
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


    // shows all the options you can do to manipulate the products, simply viewing all of them, adding, or updating, or returning to the main menu. 
    private static void ShowProductMenu()
    {
        bool returnToMainMenu = false; 

        while(!returnToMainMenu)
        {
            AnsiConsole.Clear();

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Product Management[/]")
                .AddChoices(
                    "View All Products", "Add Product", "Update Product", "Return to Main Menu"));

            switch(choice)
            {
                case "View All Products":
                    ShowProducts();
                    break;

                case "Add Product":
                    AddProduct();
                    break;
                case "Update Product": 
                    UpdateProduct();
                    break;
                case "Return to Main Menu":
                    returnToMainMenu = true;
                    break; 
            }
        }

    }


    // shows all of our products in store:
    private static void ShowProducts()
    {
        AnsiConsole.Clear();

        var table = new Table()
            .Title("[green]Products[/]")
            .AddColumn("SKU")
            .AddColumn("Name")
            .AddColumn("Department")
            .AddColumn("Price")
            .AddColumn("Stock");

        foreach(Product product in Products)
        {
            table.AddRow(
                Markup.Escape(product.Sku),
                Markup.Escape(product.Name),
                Markup.Escape(product.Department),
                product.Price.ToString(),
                product.StockQuantity.ToString());
        }

        AnsiConsole.Write(table);
        Pause(); 
    }




    // adds a product to the list:
    private static void AddProduct()
    {
        AnsiConsole.Clear();

        string sku = AnsiConsole.Prompt(
            new TextPrompt<string>("SKU:")
            .Validate(value => !string.IsNullOrWhiteSpace(value)
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]SKU is required.[/]")))
            .Trim()
            .ToUpperInvariant(); 

        if(Products.Exists(product => product.Sku == sku))
        {
            AnsiConsole.MarkupLine($"[red]A product with SKU {Markup.Escape(sku)} already exists.[/]");
            Pause();
            return;
        }

        string name = AnsiConsole.Prompt(
           new TextPrompt<string>("Name:")
           .Validate(value => !string.IsNullOrWhiteSpace(value)
           ? ValidationResult.Success()
           : ValidationResult.Error("[red]Name is required.[/]")))
           .Trim();

        string department = AnsiConsole.Prompt(
           new SelectionPrompt<string>()
           .Title("Department:")
           .AddChoices(Departments));


        decimal price = AnsiConsole.Prompt(
          new TextPrompt<decimal>("Price:")
          .Validate(value => value >= 0
          ? ValidationResult.Success()
          : ValidationResult.Error("[red]Price cannot be negative.[/]")));

        int stockQuantity = AnsiConsole.Prompt(
          new TextPrompt<int>("Stock Quantity:")
          .Validate(value => value >= 0
          ? ValidationResult.Success()
          : ValidationResult.Error("[red]Stock quantity cannot be negative.[/]"))); 


        Products.Add(new Product
        {
            Name = name,
            Sku = sku,
            Price = price,
            Department = department,
            StockQuantity = stockQuantity
        });

        AnsiConsole.MarkupLine("[green]Product added successfully.[/]");
        Pause();


    }



    


    // updates a current product:
    private static void UpdateProduct()
    {
        AnsiConsole.Clear(); 

        if(Products.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]There are no products to update.[/]");
            Pause();
            return;
        }

        string selectedSku = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose a product:")
            .AddChoices(Products.Select(product => product.Sku)));

        Product product = Products.Find(product => product.Sku == selectedSku);

        product.Name = AnsiConsole.Prompt(
        new TextPrompt<string>("Name:")
        .DefaultValue(product.Name)
        .Validate(value => !string.IsNullOrWhiteSpace(value)
        ? ValidationResult.Success()
        : ValidationResult.Error("[red]Name is required.[/]")))
        .Trim();

        product.Department = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Department:")
        .AddChoices(Departments));

        product.Price = AnsiConsole.Prompt(
         new TextPrompt<decimal>("Price:")
         .DefaultValue(product.Price)
          .Validate(value => value >= 0
          ? ValidationResult.Success()
          : ValidationResult.Error("[red]Price cannot be negative.[/]")));


        product.StockQuantity = AnsiConsole.Prompt(
           new TextPrompt<int>("Stock Quantity:")
           .DefaultValue(product.StockQuantity)
           .Validate(value => value >= 0
           ? ValidationResult.Success()
           : ValidationResult.Error("[red]Stock quantity cannot be negative.[/]")));

        AnsiConsole.MarkupLine("[green]Product updated successfully.[/]");
        Pause(); 

    }


    // the product methods all pause before returning:
    private static void Pause()
    {
        AnsiConsole.MarkupLine("[grey]Press any key to return...[/]");
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