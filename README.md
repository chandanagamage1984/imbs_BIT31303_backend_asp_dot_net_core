# Employee Management System - ASP.NET Core Backend
A robust ASP.NET Core backend for the Employee Management System, providing RESTful APIs for managing employees, departments, and their relationships. Built with ASP.NET Core 7, Entity Framework Core, and SQL Server/MySQL.

https://img.shields.io/badge/ASP.NET%2520Core-7-blue

https://img.shields.io/badge/C%2523-10.0-green

https://img.shields.io/badge/Entity%2520Framework%2520Core-7-purple

https://img.shields.io/badge/SQL%2520Server-2019-red

https://img.shields.io/badge/MySQL-8.0-orange

## 🚀 Features
### Core Functionality
* ✅ Employee Management - Complete CRUD operations for employees

* ✅ Department Management - Full department lifecycle management

* ✅ Employee-Project Relationships - Many-to-many relationships management

* ✅ RESTful APIs - Clean, consistent API design following REST principles

* ✅ Data Validation - Comprehensive input validation and error handling

* ✅ DTO Pattern - Separation of concerns with Data Transfer Objects

### Technical Features
* 🗄️ Entity Framework Core - Modern ORM with code-first approach

* 🔒 Data Validation - Model validation with proper error responses

* 📊 LINQ Queries - Efficient data querying and manipulation

* 🎯 Custom DTOs - Optimized data transfer between layers

* 🛡️ Global Exception Handling - Consistent error response format

* 📝 Swagger Documentation - Interactive API documentation

## 🛠️ Technology Stack
### Backend
* ASP.NET Core 7 - Modern web framework

* Entity Framework Core 7 - Object-relational mapper

* LINQ - Language Integrated Queries

* AutoMapper - Object-object mapping

* Fluent Validation - Advanced validation (optional)

* Swashbuckle - API documentation

### Database
* SQL Server - Primary database (default)

* MySQL - Alternative database option

* Entity Framework Core - Database migrations and ORM

### Development Tools
* Visual Studio 2022 - Primary IDE

* Visual Studio Code - Lightweight alternative

* .NET CLI - Command-line interface

* SQL Server Management Studio - Database management

## 📋 Prerequisites
Before running this application, ensure you have the following installed:

* .NET 7 SDK or higher - Download here

* Visual Studio 2022 or Visual Studio Code - Download here

* SQL Server 2019 or higher - Download here

  * Alternatively: MySQL 8.0 or higher

* Git - For version control

## ⚙️ Installation & Setup
### 1. Clone the Repository
bash
```
git clone <repository-url>
cd EmployeesManagementSystem.API
```

### 2. Database Setup
#### Option A: MSSQL Server
sql
```
-- Create database
CREATE DATABASE bit31303_emp_mgt_sys;
-- Or use SQL Server Management Studio (SSMS) to create the database

USE bit31303_emp_mgt_sys;

CREATE TABLE departments(
departmentid int identity(1,1) NOT NULL PRIMARY KEY,
name nvarchar(128) NOT NULL,
location nvarchar(64) NOT NULL
);

CREATE TABLE employees(
employeeid int identity(1,1) NOT NULL PRIMARY KEY,
name nvarchar(128) NOT NULL,
email nvarchar(64) NOT NULL,
phone nvarchar(10) NOT NULL,
salary float NOT NULL,
departmentid int FOREIGN KEY REFERENCES departments(departmentid) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE projects(
projectid int identity(1,1) NOT NULL PRIMARY KEY,
projectname nvarchar(128) NOT NULL,
startdate date NOT NULL,
enddate date NOT NULL
);

CREATE TABLE employee_projects(
employeeid int NOT NULL FOREIGN KEY REFERENCES employees(employeeid) ON UPDATE CASCADE ON DELETE CASCADE,
projectid int NOT NULL FOREIGN KEY REFERENCES projects(projectid) ON UPDATE CASCADE ON DELETE CASCADE
);
```

### 3. Configuration
Update appsettings.json with your database connection string:

#### For MSSQL Server:
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=bit31303_emp_mgt_sys;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}

### 4. Verify Installation
The application will start on:

* HTTPS: https://localhost:7155

* HTTP: http://localhost:5256

Test the API endpoints:

bash
```
# Test API health
curl https://localhost:7155/api/employees

# Or open Swagger documentation
https://localhost:7155/swagger
```

## 🎯 API Endpoints
### Employee Endpoints
| Method   | Endpoint             | Description                               |
| -------- | -------------------- | ----------------------------------------- |
| GET	     | /api/employees	      | Get all employees with department details |
| GET	     | /api/employees/{id}	| Get employee by ID                        |
| POST	   | /api/employees	      | Create new employee                       |
| PUT	     | /api/employees/{id}	| Update employee                           |
| DELETE	 | /api/employees/{id}	| Delete employee                           |

### Department Endpoints
| Method   | Endpoint                | Description            |
| -------- | ----------------------- | ---------------------- |
| GET	     | /api/departments	       | Get all departments    |
| GET	     | /api/departments/{id}	 | Get department by ID   |
| POST	   | /api/departments	       | Create new department  |
| PUT	     | /api/departments/{id}	 | Update department      |
| DELETE	 | /api/departments/{id}	 | Delete department      |

### Project Endpoints
| Method   | Endpoint                | Description            |
| -------- | ----------------------- | ---------------------- |
| GET	     | /api/projects	         | Get all projects       |
| GET	     | /api/projects/{id}	     | Get projects by ID     |
| POST	   | /api/projects	         | Create new project     |
| PUT	     | /api/projects/{id}	     | Update project         |
| DELETE	 | /api/projects/{id}	     | Delete project         |

### Employee-Project Assignment Endpoints
| Method   | Endpoint                                    | Description                          |
| -------- | ------------------------------------------- | ------------------------------------ |
| GET	     | /api/employeeprojects	                     | Get all employee-project assignments |
| GET	     | /api/employeeprojects/employee/{employeeId} | Get projects by employee             |
| GET	     | /api/employeeprojects/project/{projectId}	 | Get employees by project             |
| POST	   | /api/employeeprojects	                     | Assign employee to project           |
| PUT	     | /api/employeeprojects	                     | Update assignment                    |
| DELETE	 | /api/employeeprojects	                     | Remove assignment                    |
