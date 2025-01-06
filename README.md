**#AuthProject API**

This project is an API built using .NET 8. It provides authentication and authorization functionalities, including JWT (JSON Web Token) generation and database integration using Entity Framework Core.
**Features**
.NET 8: Built with the latest version of .NET for modern and efficient API development.
Authentication: Secure login and registration endpoints.
JWT Token: Generate JWT tokens for secure client-server communication.
Database Integration: Database operations are handled using Entity Framework Core with DbContext.
Migration Support: Easily manage database schema with EF Core migrations

**Database Migration**
**create a new migration**
dotnet ef migrations add <MigrationName>
**Apply migration**
dotnet ef database update
