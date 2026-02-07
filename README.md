# Team Project Community Helpers - CSE 325 Project
Community Helpers is a responsive web application designed to bridge the gap between local residents needing assistance and community volunteers. This version is built using Blazor Server with Interactive Components for a seamless user experience.

# 👥 Team Members
Author: Diogo Rangel Dos Santos

🛠️ Project Scope & Features
User Authentication: Secure login and registration using ASP.NET Core Identity.

Interactive Dashboard: A centralized hub to view all community requests with real-time updates.

Claim & Unclaim System: Volunteers can claim a task or release it if they are unable to complete it.

Attachments: Users can upload images (e.g., photos of the problem) when creating a request.

Status Tracking: Real-time visibility for tasks: Pending, In-Progress, and Completed.

Category Filtering: Quickly sort tasks by types like Yard Work, Groceries, Tech Support, or Moving.

Database Integration: Reliable data storage using Entity Framework Core and SQLite.

# 🚀 Setup & Execution Instructions
Follow these steps to get the project running on your local machine.

1. Prerequisites
.NET 10.0 SDK (or current version used in project)

EF Core Tools: Install via dotnet tool install --global dotnet-ef

VS Code with the C# Dev Kit extension.

2. Installation & Database Setup
Clone the repository and prepare the database:

Bash
# Clone the repository
git clone https://github.com/diogorangel/CommunityHelpersCSE325Team12.git

# Navigate to the web project folder
cd CommunityHelpersCSE325Team12/CommunityHelpers.Web

# Restore dependencies
dotnet restore

# Apply migrations to create the local SQLite database (app.db)
dotnet ef database update
3. Running the Project
To start the application with Hot Reload (the app will automatically refresh when you save code changes):

Bash
dotnet watch
The site will be available at:

HTTPS: https://localhost:7xxx (Check terminal for specific port)

HTTP: http://localhost:5xxx

# 💡 Useful Commands
Development & Maintenance
Create a Migration: (Run after changing models)

dotnet ef migrations add NameOfYourChange

Update Database: dotnet ef database update

Clean Build: (Use if the file is locked by a ghost process)

dotnet clean

Kill Locked Process (Windows): taskkill /f /im CommunityHelpers.Blazor.exe

Project Structure (Blazor)
/Components/Pages: Contains the .razor files (Dashboard, MyRequests, CreateRequest).

/Models: Defines the data structure (e.g., HelpRequest.cs with AttachmentPath).

/Data: Contains the ApplicationDbContext.cs and migrations.

/wwwroot/uploads: Where user-uploaded images are stored.

# 📅 Project Timeline
Phase 1: Foundation - Repository setup, Blazor Server architecture, and initial design.

Phase 2: Core Development - Database setup and CRUD for Help Requests.

Phase 3: Security & Interaction - Identity implementation, interactive "Claim" logic, and form validation.

Phase 4: Advanced Features - Image upload system, "Unclaim" functionality, and English translation.

Phase 5: Quality Assurance - Final documentation and responsive testing across devices.