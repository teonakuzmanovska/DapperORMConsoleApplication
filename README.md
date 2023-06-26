# DapperORMConsoleApplication

DapperORMConsoleApplication is a console application that demonstrates the usage of Dapper ORM for database operations. It allows you to interact with a database, create tables, perform CRUD operations and showcase various features of Dapper.

## Features

- Table creation using Dapper
- Inserting data into tables using Dapper
- Retrieving data from tables using Dapper
- Sorting data with queries using Dapper

## Prerequisites

- .NET Framework 7.4.2 or later
- SQL Server installed locally or a connection string to a remote SQL Server


## Getting Started

1. Clone the repository:

   ```shell
   git clone https://github.com/your-username/DapperORMConsoleApp.git

2. Open the solution in Visual Studio.
3. Configure the connection string in the App.config file under `<configurations>`:
   ```shell
   connectionString = "your-connection-string";
4. Create database in your local SQL Server with the name you included in your connection string
5. Build the solution to restore NuGet packages.
6. Run the application.

## Usage

For the sake of convenience and to minimize the possibility of human error, the data is being automatically inserted. The entire process is logged in the console for visibility and transparency.
