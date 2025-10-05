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