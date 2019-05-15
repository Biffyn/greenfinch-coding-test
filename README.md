# Greenfinch Coding Test

This application was created to demonstrate a fully fledged application built with Angular that interacts with a backend server built using .Net Core.

## Technologies and Frameworks Used

### API Backend

- .NET Core 2.2
- Entity Framework Core Code First
- Serilog
- xUnit & Moq
- API Documentation using Swagger

### Cleint

- Angular 7
- Angular CLI
- Bootstrap 4

# Getting started

**Make sure you have at least Node 8.x or higher (w/ npm 5+) installed!**  
**This repository uses .Net Core 2.2, which has a hard requirement on .NET Core Runtime 2.2 and .NET Core SDK 2.2. Please install these items from [here](https://dotnet.microsoft.com/download)**

## Installation

- Clone the [Git Repository](https://github.com/Biffyn/greenfinch-coding-test.git)
- Install all dependencies using "dotnet restore" for .NET Core projecta & "npm install" for the angular project.
  When using VisualStudio this is automatic, check the output window or status bar to know that the package/dependencies restore process is complete before launching the application for the first time.
- If you get any errors, consider running the steps manually.
  Open command prompt and do the below steps:
  - run 'dotnet restore' to restore nuget packages
  - run 'npm install' from the ClientApp folder in the Greenfinch.Web.Client project to restore npm packages
  - Try running the application again - Test to make sure it all works

### Visual Studio

Make sure that you have .NET Core 2.2 installed and VS2017 or VS2019.

#### Set Startup Projects

Open the properties for GreenfinchCodingTest.sln and select multiple startup projects. Select Greenfinch.Web.Api and Greenfinch.Web.Client as start up projects.

#### Update DB Connection String

- Open appsettings.json in Greenfinch.Web.Api project and update ConnectionStrings > SQLConnection to match your Server, User and Password:

```json
 "ConnectionStrings": {
   "SQLConnection": "Server=YOUR-SERVER;Initial Catalog=greenfinch;User ID=YOUR-USER;Password=YOUR-PASSWORD ;Trusted_Connection=True;"
 }
```

**Note:** You may also want to change the db name, if it conflicts with a pre-exsiting db.

## Running The Application

### Visual Studio

Simply push F5 to start debugging!
