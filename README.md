# Order Management System

> A web-based Order Management System developed as part of a software development assignment.

## 📌 Overview

The **Order Management System** is a full-stack web application that allows users to create and manage orders, apply discounts, calculate order totals, and search existing orders using an Order Code.

The application is developed using **Angular**, **ASP.NET Core Web API**, **ADO.NET**, and **SQL Server** with **Stored Procedures**.

---

## 🏗️ Architecture

The application follows a **3-Layer Architecture** with clear separation of responsibilities.

```text
Angular
   │
   ▼
ASP.NET Core Web API
   │
   ▼
Business Logic Layer (BLL)
   │
   ▼
Data Access Layer (DAL)
   │
   ▼
ADO.NET
   │
   ▼
SQL Server
   │
   ▼
Stored Procedures

Architecture Layers
Layer	Responsibility
Angular	User Interface and API integration
Web API	Handles HTTP requests and responses
BLL	Handles business logic and calculations
DAL	Handles database operations using ADO.NET
SQL Server	Stores application data
Stored Procedures	Handles database operations and validations

🛠️ Technologies Used
Frontend
Angular
TypeScript
HTML5
CSS3
Bootstrap
Angular Routing
Angular Services
HTTP Client
Backend
ASP.NET Core Web API
.NET 8
C#
ADO.NET
Dependency Injection
3-Layer Architecture
RESTful APIs
Database
Microsoft SQL Server
Stored Procedures
Table-Valued Parameters (TVP)
SQL Transactions
Tools
Visual Studio
Visual Studio Code
SQL Server Management Studio
Swagger
Postman
Git
GitHub

📦 Modules
1. Home

The Home page provides navigation to the main modules:

Create Order
View Order

2. Create Order

The Create Order module allows users to create a new order.

Features
Select Product
Enter Quantity
Select Discount Type
Calculate Amount
Calculate Discount Amount
Calculate Net Amount
Add Product to Order
Remove Product from Order
Calculate Sub Total
Calculate Total Discount
Calculate Grand Total
Enter Order Code
Select Order Date
Enter Billing Address
Enter Shipping Address
Enter Remark
Create Order

3. View Order

The View Order module allows users to search an existing order using the Order Code.

Features
Search Order by Order Code
View Order Date
View Billing Address
View Shipping Address
View Product Details
View Quantity
View Rate
View Amount
View Discount Type
View Discount Amount
View Net Amount
View Sub Total
View Total Discount
View Grand Total

🗄️ Database Design
Master Tables
MstProductMaster

Stores product master information.

Id	Product Name	Rate
1	Notebook	₹50
2	Pen	₹200
3	Keyboard	₹400
4	Mouse	₹500
MstDiscountMaster

Stores discount master information.

Id	Discount Type	Value
1	Percentage	10%
2	Fixed	₹20
Transaction Tables
TrnOrder

Stores order header information.

Column
Id
OrderCode
OrderDate
SubTotal
TotalDiscount
GrandTotal
Remark
BillingAddress
ShippingAddress
TrnOrderDetails

Stores individual products added to an order.

Column
Id
OrderId
ProductId
Quantity
Rate
Amount
DiscountId
DiscountAmount
NetAmount

💰 Discount Calculation
The application supports two types of discounts:

Percentage Discount
Fixed Discount
Percentage Discount Example
Product   : Pen
Quantity  : 2
Rate      : ₹200
Amount    : ₹400
Discount  : 10%
Calculation
Discount Amount = ₹400 × 10 / 100
                = ₹40

Net Amount = ₹400 - ₹40
           = ₹360
Fixed Discount Example
Product   : Notebook
Quantity  : 4
Rate      : ₹50
Amount    : ₹200
Discount  : ₹20
Calculation
Discount Amount = 4 × ₹20
                = ₹80

Net Amount = ₹200 - ₹80
           = ₹120

🧮 Order Total Calculation
Example Order
Product	Qty	Rate	Amount	Discount	Discount Amount	Net Amount
Notebook	4	₹50	₹200	Fixed ₹20	₹80	₹120
Pen	2	₹200	₹400	10%	₹40	₹360
Order Summary
Total	Amount
Sub Total	₹600
Total Discount	₹120
Grand Total	₹480

🔐 Validation
The application implements validations at both the frontend and database levels.

Frontend Validation
Product is required.
Quantity must be greater than zero.
Discount is required.
Order Code is required.
Billing Address is required.
Shipping Address is required.
At least one product must be added before creating an order.
Database Validation

The database Stored Procedure validates:

Product existence
Discount existence
Quantity
Amount
Discount Amount
Net Amount
Order totals
Duplicate Order Code
🔄 Transaction Management

Order creation is handled using a SQL Server Transaction.

The Order Header and Order Details are inserted within the same transaction.

If any validation or database operation fails, the transaction is rolled back.

Begin Transaction
       │
       ▼
Validate Order
       │
       ▼
Insert Order Header
       │
       ▼
Insert Order Details
       │
       ▼
Commit Transaction

If an error occurs:

Error
  │
  ▼
Rollback Transaction

This prevents partial or incomplete order data from being saved.

📋 Table-Valued Parameter

A Table-Valued Parameter (TVP) is used to pass multiple order detail records from the API to the SQL Stored Procedure.

This allows multiple order details to be processed efficiently in a single database operation.

OrderDetailType
CREATE TYPE dbo.OrderDetailType AS TABLE
(
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    Rate DECIMAL(18,2) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    DiscountId INT NOT NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL,
    NetAmount DECIMAL(18,2) NOT NULL
);
⚙️ Stored Procedures

The application uses Stored Procedures for database operations.

Main database operations include:

Get Products
Get Product By ID
Get Discounts
Get Discount By ID
Calculate Order Detail
Save Order
Get Order By Order Code
🌐 API Endpoints
Product APIs
Get Products
GET /api/Order/products

Returns all available products.

Get Product By ID
GET /api/Order/products/{productId}

Returns product details by Product ID.

Discount APIs
Get Discounts
GET /api/Order/discounts

Returns all available discount types.

Get Discount By ID
GET /api/Order/discounts/{discountId}

Returns discount details by Discount ID.

Calculate Order Detail
POST /api/Order/calculate-detail

Calculates:

Rate
Amount
Discount Amount
Net Amount

based on Product, Quantity, and Discount.

Create Order
POST /api/Order

Creates a new order along with its order details.

Get Order By Code
GET /api/Order/{orderCode}

Retrieves an existing order using the Order Code.

Example:

GET /api/Order/SH000001
📁 Project Structure
OrderManagement
│
├── Backend
│   └── OrderManagement
│       ├── Controllers
│       │
│       ├── BLL
│       │   ├── Interfaces
│       │   └── Services
│       │
│       ├── DAL
│       │   ├── Interfaces
│       │   └── Repositories
│       │
│       ├── Models
│       │
│       └── SQL
│
├── Frontend
│   └── order-management
│       ├── src
│       │   └── app
│       │       ├── Components
│       │       │   ├── home
│       │       │   ├── order
│       │       │   └── view-order
│       │       │
│       │       ├── Models
│       │       ├── Services
│       │       ├── app-routing.module.ts
│       │       ├── app.module.ts
│       │       └── app.component.html
│       │
│       ├── angular.json
│       └── package.json
│
└── README.md
🔗 Frontend & Backend Integration

Angular communicates with the ASP.NET Core Web API using HTTP requests.

Angular Component
       │
       ▼
OrderService
       │
       ▼
ASP.NET Core Web API
       │
       ▼
BLL
       │
       ▼
DAL
       │
       ▼
ADO.NET
       │
       ▼
Stored Procedure
       │
       ▼
SQL Server

CORS is configured in the ASP.NET Core Web API to allow communication between the Angular application and the backend API.

🧪 API Testing

The APIs were tested using:

Swagger UI
Postman
Tested Operations
Get Products
Get Product By ID
Get Discounts
Get Discount By ID
Calculate Order Detail
Create Order
Search Order By Order Code
Product Validation
Discount Validation
Order Validation
Discount Calculation
Order Total Calculation
Duplicate Order Code Validation
🚀 How to Run the Project
Prerequisites

Install the following:

.NET 8 SDK
Node.js
Angular CLI
SQL Server
SQL Server Management Studio
Visual Studio
Visual Studio Code
🗄️ Database Setup
Open SQL Server Management Studio.
Create the required database.
Execute the SQL scripts available in the SQL folder.
Create the required tables.
Insert the master data.
Create the Table-Valued Parameter.
Create the Stored Procedures.
Update the SQL Server connection string in the backend configuration.
🔧 Backend Setup
Open the backend solution in Visual Studio.
Update the SQL Server connection string.
Build the solution.
Run the ASP.NET Core Web API.
Open Swagger UI or use Postman to test the APIs.
🖥️ Frontend Setup

Open the Angular project in Visual Studio Code.

Install dependencies:

npm install

Run the Angular application:

ng serve

The application will be available at:

http://localhost:4200

Make sure the ASP.NET Core Web API is running before using the Angular application.

🔧 Configuration

The Angular application uses an API base URL similar to:

apiUrl: 'https://localhost:7186/api'

Update the API URL according to the port on which the ASP.NET Core Web API is running.

Note: Do not commit real database passwords, API secrets, or other sensitive credentials to the repository.

✨ Key Implementation Highlights
Angular frontend
ASP.NET Core Web API
.NET 8
C#
3-Layer Architecture
Separation of Concerns
Business Logic Layer
Data Access Layer
ADO.NET
SQL Server
Stored Procedures
Table-Valued Parameters
SQL Transactions
Product Management
Discount Management
Percentage Discount Calculation
Fixed Discount Calculation
Order Detail Calculation
Order Total Calculation
Frontend Validation
Database Validation
Duplicate Order Code Validation
RESTful APIs
Angular Routing
Angular Services
TypeScript Interfaces
Bootstrap UI
Error Handling
Success Message Handling
Clean and Maintainable Code Structure
👨‍💻 Developer
Pratik Nimbalkar

Software Engineer

.NET • ASP.NET Core • Angular • C# • SQL Server • ADO.NET
