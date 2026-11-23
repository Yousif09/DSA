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

	The store management system needs a set of departments: Electronics, Groceries, Clothing, Home, and Sports. These departments will remain
	fixed in the first version of the application. We need one variable that can hold several related values in a defined order. We also know how many values there will
	be before the application starts. This is a natrual problem for our first data structure: an array.
