# Backup Management - Backend

## Overview

This project is the backend for an automatic backup system created as my school project. It provides RESTful APIs for managing user authentication with support for admin/user, backup operations, and backup assignation. 
The backend is built using ASP.NET Core and leverages JWT tokens for secure authentication on the frontend part of this project.

This project <ins>**isn't complete production ready software**</ins> and should be only used as a reference for future projects. Originally this project was stored on a private GitLab repo.
Take into consideration that this iy my completely first ASP.NET backend project, so any feedback is more than welcome. ^^

## Technologies Used

- .NET 8.0
- ASP.NET Core
- Entity Framework Core
- MySQL
- JWT (JSON Web Tokens)
- Swagger (Default part of ASP.NET Core)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MySQL](https://www.mysql.com/downloads/)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/backupsystem-backend.git
    cd backupsystem-backend/WebServer
    ```

2. Configure the database connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "MySQL": "server=your_server;database=your_database;user=your_user;password=your_password;charset=utf8;"
    }
    ```

3. Install dotnet-ef if not yet installed.
    ```sh
    Verify installation: dotnet ef
    Install:             dotnet tool install --global dotnet-ef
    ```

4. Run the migrations to set up the database:
    ```sh
    dotnet ef database update
    ```

5. Build and run the project:
    ```sh
    dotnet run
    ```
