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


4. **Apply Database Migrations:**
   If you're using Entity Framework Core, run the following command:
   ```bash
   dotnet ef database update

   (for visual studio)
   update-database 

**API Endpoints**
**User Registration API**

      Endpoint: POST /api/auth/signUp
    Validation:
        Email must be in a valid format.
        Password must be at least 6 characters long, containing at least one uppercase letter, one lowercase letter, and one number.
    Response:
    On successful registration, a confirmation email will be sent, and user details will be saved in the database.

**User Login API**

      Endpoint: POST /api/auth/signIn


**Product CRUD Operations**

    GET /api/products: Get a list of all products, with pagination and optional filters.
    GET /api/products/{productId}: Get details of a specific product by its ID.
    POST /api/products: Create a new product. The request body should contain details like name, description, price, etc.
    PUT /api/products/{productId}: Update an existing product.
    DELETE /api/products/{productId}: Delete a product by its ID.

**Pagination and Filtering**

    GET /api/products/filter?SearchQuery=del&PageNumber=1&PageSize=1: Filter products based on price and name.
