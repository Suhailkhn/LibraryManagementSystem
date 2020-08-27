# Library Management System

A desktop application made using C# and WPF, to be used in a library to manage their inventory of books. 

Watch application walkthrough on YouTube:-
https://www.youtube.com/watch?v=XRYlyq4vCug

Features:-
- Provide a way for the user to create, update, and delete books
- Provide a way for the user to create, update, and delete library customers.
- Provide a system that allows customers to check books out and back in.
- The system should keep a history of who checked in/out a book and when.

Technical Details:-
- Made using C#, WPF and XAML.
- Demonstrates MVVM pattern.
- Demonstrates 3-tier architecture where data access layer and business logic layer are separated from the entire application. 
  Only Data layer can access the database and the Data layer can only be accessed by Business layer. 
  The presentation layer (viewmodels) only accesses the Business layer.
- Uses MySQL as database and Dapper as ORM.
- Uses Unity Container for Dependency Injection.
