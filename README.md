# ASP.NET Core Product Management API
This project is a robust web application built using ASP.NET Core API (C#) that allows users to register, log in, and perform CRUD (Create, Read, Update, Delete) operations on products. The application supports pagination and filtering for efficient product management.

## Features
- User Registration
- User Login
- CRUD operations for products
- Pagination support for product listings
- Filtering capabilities based on product attributes

## Architecture
This application follows an Earth Layer Architecture and employs the CQRS (Command Query Responsibility Segregation) pattern using the Mediator pattern for handling requests.

## Prerequisites
To run this project, ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0 or later)
- [MySQL](https://dev.mysql.com/downloads/mysql/) (version 8.0 or later)
- [Postman](https://www.postman.com/downloads/) (for testing API endpoints)

## Libraries Used
- **Entity Framework Core** - For database operations and migrations.
- **FluentValidation** - For input validation.
- **MediatR** - For implementing the Mediator pattern.
- **Swashbuckle.AspNetCore** - For API documentation (Swagger).
- **JWT Bearer Authentication** - For securing the API with tokens.

## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone <repository-url>
   cd <project-directory>
   
2. **Install Dependencies:**
   ```bash
    dotnet restore
   
3. **Update the Connection String:**:
In appsettings.json, replace the default connection string with your MySQL database connection string:
```bash
   {
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProductDB;User=root;Password=my-secret-pw;"
  }
}


