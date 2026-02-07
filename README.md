# Team Project Community Helpers - CSE 325 Project

Community Helpers is a responsive web application designed to bridge the gap between local residents needing assistance and community volunteers.

## 👥 Team Members
* Author : Diogo Rangel Dos Santos

## 🛠️ Project Scope & Features
* **User Authentication:** Secure login and registration using ASP.NET Core Identity.
* **Help Dashboard:** A centralized hub to view, create, and claim "Help Requests."
* **Status Tracking:** Real-time updates for tasks (Pending, In-Progress, Completed).
* **Category Filtering:** Sort tasks by types like Yard Work, Groceries, or Tech Support.
* **Database Integration:** Real-time data storage using SQLite.

---

## 🚀 Setup & Execution Instructions

Follow these steps to get the project running on your local machine.

### 1. Prerequisites
* [Download .NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* VS Code with the **C# Dev Kit** extension.

### 2. Installation
Clone the repository and enter the project directory:
```bash
git clone [https://github.com/diogorangel/CommunityHelpersCSE325Team12.git](https://github.com/diogorangel/CommunityHelpersCSE325Team12.git)
cd CommunityHelpersCSE325Team12/CommunityHelpers.Web

1. Open the terminal in the CommunityHelpersCSE325Team12 folder.
2. Execute the command below:

	dotnet watch --project CommunityHelpers.Web/CommunityHelpers.Web.csproj

This will compile and run the main web project.

If you prefer to run without hot reload, use:

	dotnet run --project CommunityHelpers.Web/CommunityHelpers.Web.csproj

The site will be available at http://localhost:5000 or http://localhost:5001 (SSL).

3. Database Initialization

Before running the app, you must apply the migrations to create your local app.db file:

Bash
dotnet ef database update

4. Running the Project

To start the application with Hot Reload (auto-updates when you save code):

Bash
dotnet watch run


The site will be available at https://localhost:5001 or http://localhost:5000.

💡 Useful Commands for New Users
Development & Maintenance

Install SQLite Support: (If not already present) dotnet add package Microsoft.EntityFrameworkCore.Sqlite

Create a New Migration: (If you change the Models) dotnet ef migrations add NameOfYourChange

Restore Dependencies: dotnet restore

Project Structure

/Controllers: Contains the logic for handling requests (e.g., HelpRequestsController.cs).

/Models: Defines the data structure (e.g., HelpRequest.cs).

/Views: Contains the HTML/Razor files for the user interface.

/Data: Contains the Database Context and Migrations.

📅 Project Timeline

Phase 1: Foundation - Repository setup, Architecture, and Initial Design.

Phase 2: Core Development - Database setup and CRUD for Help Requests.

Phase 3: Security - Identity implementation and Form Validation.

Phase 4: Quality - Responsive testing and Final Documentation.