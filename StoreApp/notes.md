This is a course going over all the essential beginner data structures in C#:

(This will not go over: complexity analysis, big O notation, linked lists, trees, heaps, graphs, and 
advanced algorithms)... learn this later


Data structures have developed alongside computing itself. As early computers began processing larger and more
varied collections of information, programmers needed relaiable ways to organize values in memory and perform operations on them.

Some structures model information in a simple sequence. Others represent priorities, relationships, hierarchies, or
fast lookups. Many of the structures used in modern software were studied and refined during the early decades of computer sceincem
then incorportated into programming languages and their standard libraries.

From machine memory to Abstraction: 

	Early programmers worked close to hte machine and had to manage memory locations directly. Arrays provided a way to treat 
	adjacent memory locations as one indexed collection, making repeated data easier to manage. 

	As programming languages became more expensive, structures such as linked lists, stacks, queues, trees, graphs, 
	and hash tables gave programmers reusable models for common problems. These abstractions let developers focus on the
	behavior they need without handling every memory detail themselves.



WHy do these ideas still matter? 

	Modern languages such as C# provide ready-made collection types, but the underlying decisions have not disappeared.
	A List<T>, Queue<T>, Stack<T>, or Dictionary<TKey, TValue> behaves differently because each is built around a 
	different structure and purpose.






Arrays: 

	* The store management system needs a set of departments: Electronics, Groceries, Clothing, Home, and Sports. These departments will remain
	fixed in the first version of the application. We need one variable that can hold several related values in a defined order. We also know how many values there will
	be before the application starts. This is a natrual problem for our first data structure: an array. 

	* What is an array? An array is a collection that stores a fixed number of values of the same type. Each value occupies a numbered position called an
	index. We use that index to read or replace a particular value. Arrays are among the most fundamental data structures in programming. Many more flexible collections are 
	built on top of array-like storage internally. Which makes arrays a useful starting point for understanding how collections organize values. 

	* An array has one element type, such as string, int, or Product.
	* it's length is decided when the array is created.
	* it's elements are stored in a defined order.
	* Each element can be accessed using its numeric index.
	* The values can change, BUT the number of positions CANNOT change. 

	* You can also create an array by specifying its length before assigning its values. When C# creates an array like:
	string[] departments = new string[5] --> it immediately creates five positions. Until we assign strings to them, these positions contain 
	the default value for string, which is null. Numeric arrays begin with zeroes, and bool ararys begin with false
	C# arrays are (zero-indexed). First element is at index 0, the second is at index 1, and final element is at index (Length - 1).
	An array with three elements has valid indexes from 0 to 2. Trying to access index 3 causes an IndexOutOfRangeException at runtime. You can also use
	the index-from-end operator. departments[^1] means the last element, while departments[^2] means the second to last element.
	Unlike a normal index, ^1 refers to the final element, 0^ refers to the position after the end and cannot be used to read an element.

	* Length and Iteration: The Length property tells us how many elements an array contains:
	string[] departments = ["Electronics", "Groceries", "Clothing"];
	Console.WriteLine(departments.Length); // 3

	* use foreach loop when you want to process every element and do not need its index:
	foreach(string department in departments)  {
	   Console.WriteLine(department); 
	}

	* use a for loop when the index is part of the work. The condition uses <, not <= because
	Length propery is one greater than the final index. 
	for(int i=0; i < departments.Length; i++) {
	   
	   Console.WriteLine($"{i + 1}. {departments[i]}"); 

	   //1. Electronics
	   //2. Groceries... and so on

	}



	* Arrays are fixed-size, Not read-only: A fixed size does NOT mean that an array's elements are permanent. We can 
	replace a value at an existing index:

	string[] departments = ["Electronics", "Groceries", "Clothing"];
	departments[1] = "Food"; 
	Console.WriteLine(departments[1]); // Now says Food instead of groceries.

	* What we can NOT do is add a sixth position to something like a five-element array or remove one of its positions.
	To represent a different number of elements, we must create another array with a different length and copy the values we want to 
	keep. That limitation is useful when the number of values is genuinely fixed, for example something like departments in a store that are already defined. 
	It becomes inconvenient when values need to be added and removed regularly. Later, List<T> will solve that problem.

	* Arrays are Reference Types: an array is a reference type, even when it contains value types such as int. Assigning an array
	variable to another variable does NOT duplicate its elements. But variables refer to the same array. 
	int[] original = { 10, 20, 30 };
	int[] sameArray = original;

	sameArray[0] = 99;
	Console.WriteLine(original[0]); 

	* Changing the array through sameArray is visible through original because there is only one array. When you need an independent copy, create a
	new array and copy the elements. 

	int[] original = { 10, 20, 30 };
	int[] copy = new int[original.Length];

	Array.Copy(original, copy, original.Length);
	copy[0] = 99;

	Console.WriteLine(original[0]); // 10
	Console.WriteLine(copy[0]); // 99 


	* Useful Array Operations: The Array class provides static methods for common operations: 
		string[] departments = {"Electronics", "Sports", "Home"}; 
		int homeIndex = Array.IndexOf(departments, "Home");
		Console.WriteLine(homeIndex); //2

		Array.Sort(departments); // Electronics, Home, Sports
		Array.Reverse(departments); // Sports, Home, Electronics (reversed order of the sort function) 

		* Array.IndexOf returns the matching elements index or -1 when no match exists. Array.Sort and Array.Reverese modify 
		the original array rather than producing a seperate result. Always ask yourself if the original order is acceptable before sorting or
		reversing an array. If the order has meaning, make a copy first. 


		* Multidimensional Arrays and Jagged Arrays: Most arrays in everyday application code are one-dimensional, but C# also supports
			rectangular multidimensional arrays. A two-dimensional array uses two indexes, often visualized as rows and columns: 

			regular two dimensional array: 
			string[,] shelfLocations =
			{
				{"A1", "A2", "A3" }, // row 0
				{"B1", "B2", "B3" } // row 1

			};

			Console.WriteLine(shelfLocations[0, 1]); // A2
			Console.WriteLine(shelfLocations[1, 2]); // B3


			Jagged Array: a jagged array is an array whose elements are themselves arrays. Each inner array can have a 
			different length. 
			
			string[][] productByAisle = [

				["Cofee", "Tea"],
				["Bread", "Rice", "Pasta"],
				["Soap"]

			 ];

			Console.WriteLine(productByAisle[1][2]); //pasta

			* You should regonize both forms, but our department feature needs only a simple one dimensional array. Choose
				additional dimensions only when the data genuinely has a grid or nested shape.

			* So When should you use an array?:
				An array is a good choice when: 
				 
				 * You know the number of elements when the array is created.
				 * That number will NOT change while the array is being used. 
				 * The values all share one element type.
				 * Their order matters or you need to access them by position
				 * A simple, direct collection is preferable to a growable, dynamic one.

				 An array is usually not the best choice when the collection needs frequent additions or removals, when each
				 value needs a meaningful lookup key, or when duplicate values must be prevented automatically. 








List<T> Fundamentals: 

	Our store departments work well in an array because their number if fixed. But products are different. While the
	application runs, users need to add products, update them, and eventually remove them. We could create a larger array and manually
	copy elements whenever the collection changes, but that would add repetitive bookkeeping to a common task. .NET
	provides a collection designed for this situation: List<T>

	* What is List<T>: List<T> is an ordered, generic collection whose size can change. The T is a type parameter. When we create a
	   List<string>, every element is a string. A List<Product> stores Product objects. 

	   * A list keeps its elements in a defined order.
	   * it uses zero-based indexes just like an array. 
	   * Elements can be added, inserted, replaced, and removed.
	   * Its Count changes as elements are added or removed. 
	   * its generic element type gives compile-time type safety. 

	   Internally, List<T> uses an array-like storage area. When that storage becomes full , the list creates a 
	   larger area and moves its elements. The collection handles this process for us, which gives us convenient growth while preserving
	   indexed access.

	   * creating an empty list when the values will arrive later: 
	   
	   var products = new List<string>(); 

	   When values are already known, initalize the list with a collection expression:
	   List<string> products = {

	       "Wireless mouse",
		   "Coffee Beans",
		   "Running Shoes"
	   };

	   The explicit type on the left tells the compiler to create a list from the collection expression. Using 
	   var products { } does not provide enough information for the compiler to determine the collection type.
	   
	   * Count and Capacity: A list has two related measurements. Count is the number of elements currently in the list. Capacity is the
			number of elements its internal storage can currently hold before it needs to grow. 

			
        // LISTS: 
        var products = new List<string>();
        products.Add("Wireless mouse");
        products.Add("Coffee Beans");

        Console.WriteLine(products.Count); // 2
        Console.WriteLine(products.Capacity);  // at least 2
	
		Most application code should make decisions using Count. Capacity is an implementation detail that 
		can occasionally help you when you already know a collection will become large, but you should not treat it as 
		the number of meaningful elements. 

		Adding and Inserting Elements: Add places one element at the end. AddRange appends several elements. Insert places
		an element at a particular index and shifts later elements to make space. 

		products.AddRange([ "Socks", "Deodorant"]);
        products.Insert(1, "Desk Lamp"); // Wireless Mouse, Desk Lamp, Coffee Beans, Socks, Deodorant. 
        foreach(string product in products)
        {
            Console.WriteLine(product);
        }

		* Use Insert only when a specific position matters. If order is not important, adding at the end communicates the intention more clearly.
		An invalid insertion index throws ArgumentOutOfRangeException.

		* Reading and Updating Elements: Lists use the same index syntax as arryays. Valid indexes run from Zero to Count -1. 
		Console.WriteLine(products[0]);  // Wireless Mouse.
        Console.WriteLine(products[^1]);  // Deodorant
        products[0] = "Keyboard"; 
        Console.WriteLine(products[0]);

		* The index changes an existing position, it does not add a new one. Writing products[3] = "Lamp" when the Count is 
		three throws an Exception. Use Add to create a new element. 

		* Finding Elements: Lists provide methods for common searches. Contains checks for a value, IndexOf returns its position, 
		   Exists checks whether any element matches a condition, and Find returns the first match: 

			bool hasCoffee = products.Contains("Coffee"); // False
			int socksIndex = products.IndexOf("Socks"); // [3]

			/* inside of it is a lambda expression (mini, unnamed function), that defines the search condition. Where
			 name is a temporary variable representing each individual item as the loop checks the list, 
			=>: lambda operator, which is read as "such that". name.Length > 5: condition being tested*/
			bool hasLongName = products.Exists(name => name.Length > 5); // True
			string? firstWithS = products.Find(name => name.StartsWith('S')); // Socks

			Find returns the elements types dfefault value when there is no match, so reference type result 
			SHOULD be checked for null. 



		 * Removing Elements: Lists supports several form of removal:
        
			bool removed = products.Remove("Desk Lamp"); // True
			products.RemoveAt(0); // removes element at index 0 
			int removedCount = products.RemoveAll(name => name == "Deodorant"); // 1
			products.Clear(); // removes everything

			Remove returns false when it cannot find the value. RemoveAt throws when the index is invalid. RemoveAll
			returns the number of matching elements to be removed.

			Not yet in this project will a product removal be added to the store. This is because we will combine removal with Stack<T> so the user
			can undo the most recent removal. 

			

		* Iteration and Sorting: use foreach when you need each element and for loop when the index matters. Do not add or
		   remove elements inside a foreach loop; changing the collection while it is being enumerated causes an execption. 
		   
		   
			foreach(string product in products)
			{
				Console.WriteLine(product);
			}

			products.Sort();
			products.Reverse();

			* Sort and Reverse mutate the original list. Custom objects need a comparison rule. For example:
			   products.Sort((left, right) => left.Name.CompareTo(right.Name)) : this sorts product objects by name.




		* Array or List?
		   
		   * Choose an array when the number of elements is fixed and indexed storage is the clearest representation.
		   * Choose List<T> when elements need to be added or removed while the collection is in use.
		   * Both collections are ordered, zero-indexed, generic, iterable, and allow duplicate values.
		   * Neither structure automatically provides meaningful key-based lookup or uniqueness rule. Later structures will introduce collections for that. 