💻 Employee Management System Backend (ASP.NET Core)
This repository contains the backend API for the Employee Management System, built using ASP.NET Core and designed to be consumed by a frontend application (such as an Angular UI). It provides RESTful endpoints for managing employee and department data.

🚀 Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

Prerequisites
You will need the following software installed on your machine:

.NET SDK (version 6.0 or higher recommended).

SQL Server (or SQL Server LocalDB/Express) to host the database.

Visual Studio (or VS Code with the C# extension).

Installation and Setup

Clone the repository:
Bash
git clone https://github.com/chandanagamage1984/imbs_BIT31303_backend_asp_dot_net_core.git
cd imbs_BIT31303_backend_asp_dot_net_core

Restore dependencies:
Bash
dotnet restore

Configure the Database Connection:

Open the appsettings.Development.json file.

Update the DefaultConnection string to point to your local SQL Server instance.

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  // Change (localdb)\\mssqllocaldb if needed
}
Apply Database Migrations:
Navigate to the directory containing the .csproj file and run the following commands to create the database schema:

Bash

dotnet tool install --global dotnet-ef # If not already installed
dotnet ef database update
▶️ Running the Application
1. Run via CLI (Command Line Interface)
Use the following command to build and run the application:

Bash

dotnet run
2. Run via Visual Studio
Open the solution file (.sln) in Visual Studio.

Set the start-up project if necessary.

Press F5 or click the Start button.

The application will typically start running on:

HTTPS: https://localhost:7155/api/

HTTP: http://localhost:5256/api/

⚙️ Configuration
The primary configuration files are:

File	Description
appsettings.json	General application settings.
appsettings.Development.json	Overrides for development environment (local connection strings, logging).
launchSettings.json	Specifies HTTP/HTTPS ports used by Visual Studio/dotnet run. Default port is likely 7000 (HTTPS).
Startup.cs	(Or Program.cs in newer versions) Configures services, routing, and middleware (CORS, Authentication, etc.).

CORS Policy
The CORS policy is configured in Startup.cs (or Program.cs) to allow the frontend application to make requests. Ensure that the allowed origins include the port where your Angular UI is running (e.g., http://localhost:4200).

🧱 API Endpoints
The API follows a standard RESTful convention.

Departments API
Method	Endpoint	Description
GET	/api/departments	Retrieves all departments.
GET	/api/departments/{id}	Retrieves a single department by ID.
POST	/api/departments	Creates a new department.
PUT	/api/departments/{id}	Updates an existing department.
DELETE	/api/departments/{id}	Deletes a department.

Employees API
Method	Endpoint	Description
GET	/api/employees	Retrieves all employees.
GET	/api/employees/{id}	Retrieves a single employee by ID.
POST	/api/employees	Creates a new employee.
PUT	/api/employees/{id}	Updates an existing employee.
DELETE	/api/employees/{id}	Deletes an employee.

Export to Sheets
🤝 Contribution
Feel free to fork the repository and contribute by submitting pull requests.

Note: For the best experience, open the project in Visual Studio or VS Code to manage dependencies and migrations easily.
