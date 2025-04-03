Gym Management System API

## 📌 Project Description

This repository contains the source code for a Gym Management System API, built using .NET, following Modular Monolith and Vertical Slice architectural patterns. The API provides a set of endpoints to manage various gym operations, including:

- Workout categories and exercise routines

- Membership management

- Staff and role management

- Discount and promotion handling

This project adheres to best practices in software architecture and testing, ensuring high maintainability and scalability.

## 🏗️ Architecture

The project is structured as a Modular Monolith, with each module containing:

- MainModule → Business logic and features

- MainModule.Contracts → Defines public API contracts

- MainModule.Tests → Unit tests for the module

It also follows the **Vertical Slice Architecture**, keeping features independent and encapsulated.

## 🧪 Testing

To ensure reliability, the project incorporates:

- Unit Testing with **xUnit**

- Assertions using **FluentAssertions**

- Fake data generation using **Bogus**

- Integration Testing with **WebApplicationFactory**

## 🚀 Technologies Used

- .NET (ASP.NET Core)

- C#

- Entity Framework Core

- Duende IdentityServer (for authentication and authorization)

- Carter (for lightweight API endpoint routing)

- MediatR (for CQRS pattern)

- .Net Aspire (for service orchestration)

- xUnit, FluentAssertions, Bogus (for testing)

## 📌 Features

- User authentication and authorization (JWT-based)

- Role-based access control

- Membership and subscription management

- Workout catalog with categories and exercises

- Discount and promotions system

- Staff and trainer management

- Logging and error handling

## 🔧 Setup & Installation

Please, note that this is a .Net Aspire project and it requires the Docker Desktop to be up and running in order to work.

_Clone the repository:_

```
git clone https://github.com/your-repo/gym-management-api.git
cd gym-management-api
 ```

_Install dependencies:_

```dotnet restore ```

_Update the database connection:_

Open appsettings.json

_Locate the ConnectionStrings section_

_Replace the default database connection with your own, for example:_

```"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=GymManagementDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}
 ```

_Run database migrations:_

```dotnet ef database update  ```

_Start the API:_

```dotnet run ```


