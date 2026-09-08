# Order Management System

## 📌 Project Overview

Order Management System is a web-based application developed as part of an assignment to manage product orders, calculate discounts, and view order details.

The application follows a 3-layer architecture with Angular as the frontend, ASP.NET Core Web API as the backend, ADO.NET for database access, and SQL Server with Stored Procedures for data management.

---

## 🏗️ Architecture

The application follows the below architecture:

Angular
   ↓
ASP.NET Core Web API
   ↓
Business Logic Layer (BLL)
   ↓
Data Access Layer (DAL)
   ↓
ADO.NET
   ↓
SQL Server
   ↓
Stored Procedures

### Layers

- **Frontend:** Angular
- **API:** ASP.NET Core Web API
- **BLL:** Business Logic Layer
- **DAL:** Data Access Layer using ADO.NET
- **Database:** Microsoft SQL Server
- **Database Logic:** Stored Procedures and Table-Valued Parameters

---

## 🛠️ Technologies Used

### Frontend
- Angular
- TypeScript
- HTML5
- CSS3
- Bootstrap

### Backend
- ASP.NET Core Web API
- C#
- ADO.NET
- 3-Layer Architecture
- Dependency Injection

### Database
- Microsoft SQL Server
- Stored Procedures
- Table-Valued Parameters (TVP)
- Transactions

### Development Tools
- Visual Studio
- Visual Studio Code
- SQL Server Management Studio
- Git
- GitHub
- Postman / Swagger

---

## 📦 Main Modules

The application contains the following modules:

### 1. Home

The home page provides navigation to:

- Create Order
- View Order

---

### 2. Create Order

The Create Order module allows users to create a new order.

#### Features

- Select Product
- Enter Quantity
- Select Discount Type
- Calculate Amount
- Calculate Discount
- Calculate Net Amount
- Add products to order detail list
- Remove products from order
- Calculate Sub Total
- Calculate Total Discount
- Calculate Grand Total
- Enter Order Code
- Select Order Date
- Enter Billing Address
- Enter Shipping Address
- Enter Remark
- Create Order

---

### 3. View Order

The View Order module allows users to search an existing order using Order Code.

#### Features

- Search order by Order Code
- Display Order information
- Display Billing Address
- Display Shipping Address
- Display Order Date
- Display Product details
- Display Quantity
- Display Rate
- Display Amount
- Display Discount Type
- Display Discount Amount
- Display Net Amount
- Display Sub Total
- Display Total Discount
- Display Grand Total

---

## 🗄️ Database Design

### Master Tables

#### MstProductMaster

Stores product information.

| Id | Product Name | Rate |
|----|--------------|------|
| 1 | Notebook | 50 |
| 2 | Pen | 200 |
| 3 | Keyboard | 400 |
| 4 | Mouse | 500 |

#### MstDiscountMaster

Stores discount information.

| Id | Discount Type | Value |
|----|---------------|-------|
| 1 | Percentage | 10 |
| 2 | Fixed | 20 |

---

### Transaction Tables

#### TrnOrder

Stores order header information.

Fields include:

- Id
- OrderCode
- OrderDate
- SubTotal
- TotalDiscount
- GrandTotal
- Remark
- BillingAddress
- ShippingAddress

#### TrnOrderDetails

Stores individual products within an order.

Fields include:

- Id
- OrderId
- ProductId
- Quantity
- Rate
- Amount
- DiscountId
- DiscountAmount
- NetAmount

---

## 💰 Discount Calculation

The application supports two types of discounts:

### Percentage Discount

For example:

Product: Pen  
Quantity: 2  
Rate: ₹200  
Amount: ₹400  
Discount: 10%

Discount Amount:

`₹400 × 10 / 100 = ₹40`

Net Amount:

`₹400 - ₹40 = ₹360`

---

### Fixed Discount

For example:

Product: Notebook  
Quantity: 4  
Rate: ₹50  
Amount: ₹200  
Discount: ₹20 per unit

Discount Amount:

`4 × ₹20 = ₹80`

Net Amount:

`₹200 - ₹80 = ₹120`

---

## 🧮 Order Total Calculation

Example order:

| Product | Qty | Rate | Amount | Discount | Discount Amount | Net Amount |
|---------|-----|------|--------|----------|-----------------|------------|
| Notebook | 4 | ₹50 | ₹200 | Fixed ₹20 | ₹80 | ₹120 |
| Pen | 2 | ₹200 | ₹400 | 10% | ₹40 | ₹360 |

### Order Summary

- Sub Total = ₹600
- Total Discount = ₹120
- Grand Total = ₹480

---

## 🔐 Validations

The application implements validations at both application and database levels.

### Frontend Validations

- Product is required
- Quantity must be greater than zero
- Discount is required
- Order Code is required
- Billing Address is required
- Shipping Address is required
- At least one product must be added before creating an order

### Database Validations

The Stored Procedure validates:

- Product existence
- Discount existence
- Quantity
- Amount
- Discount Amount
- Net Amount
- Order totals
- Duplicate Order Code

---

## 🔄 Transaction Management

Order creation is handled using a database transaction.

The order header and order details are inserted as a single transaction.

If any validation or database operation fails, the transaction is rolled back to prevent partial order creation.

---

## 📋 Table-Valued Parameter

A SQL Server Table-Valued Parameter is used to pass multiple order detail records from the application to the Stored Procedure.

This allows multiple order details to be inserted efficiently in a single database operation.

Example TVP:

` sql
CREATE TYPE dbo.OrderDetailType AS TABLE
(
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    Rate DECIMAL(18,2) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    DiscountId INT NOT NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL,
    NetAmount DECIMAL(18,2) NOT NULL
); `

⚙️ Stored Procedures

The application uses Stored Procedures for database operations.

Main Stored Procedures include:

sp_GetProducts
sp_GetProductById
sp_GetDiscounts
sp_GetDiscountById
sp_SaveOrder
sp_GetOrderByCode

The exact Stored Procedure names should match the SQL scripts included in the project.

🌐 API Endpoints
Product APIs
GET /api/Order/products

GET /api/Order/products/{id}
Returns a product by ID.

Discount APIs
GET /api/Order/discounts
Returns all discount types.

GET /api/Order/discounts/{id}
Returns a discount by ID.

Calculate Order Detail
POST /api/Order/calculate-detail

Calculates:

Rate
Amount
Discount Amount
Net Amount

based on Product, Quantity, and Discount.
📁 Project Structure
` OrderManagement
│
├── Backend
│   └── OrderManagement
│       ├── Controllers
│       ├── BLL
│       │   ├── Interfaces
│       │   └── Services
│       ├── DAL
│       │   ├── Interfaces
│       │   └── Repositories
│       ├── Models
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
│       │       ├── Models
│       │       ├── Services
│       │       ├── app-routing.module.ts
│       │       ├── app.module.ts
│       │       └── app.component.html
│       ├── angular.json
│       └── package.json
│
└── README.md 
`
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
Execute the SQL scripts provided in the SQL folder.
Create the required tables.
Insert master data.
Create the Table-Valued Parameter.
Create the Stored Procedures.
Update the database connection string in the API configuration.

🔧 Backend Setup
Open the backend solution in Visual Studio.
Update the SQL Server connection string.
Build the solution.
Run the ASP.NET Core Web API.
Verify the API using Swagger or Postman.

🖥️ Frontend Setup

Open the Angular project in Visual Studio Code.

Install dependencies:

npm install

Run the Angular application:

ng serve

The application will be available at:

http://localhost:4200

Make sure the API is running and the Angular API URL is configured correctly in the environment configuration.

🔗 Frontend and Backend Integration

Angular communicates with the ASP.NET Core Web API using HTTP requests.

Example:

` Angular
   ↓
OrderService
   ↓
ASP.NET Core Web API
   ↓
BLL
   ↓
DAL
   ↓
ADO.NET
   ↓
Stored Procedure
   ↓
SQL Server `

CORS is configured in the API to allow communication from the Angular application.

🧪 Testing

The APIs can be tested using:

Swagger UI
Postman

The following operations were tested:

Get Products
Get Product by ID
Get Discounts
Get Discount by ID
Calculate Order Detail
Create Order
Search Order by Order Code

✨ Key Implementation Highlights
`Angular frontend with reusable components
ASP.NET Core Web API
3-Layer Architecture
Separation of concerns
Business Logic Layer
Data Access Layer
ADO.NET
SQL Server
Stored Procedures
Table-Valued Parameters
Database Transactions
Product and Discount validation
Order total calculation
Duplicate Order Code validation
RESTful API endpoints
Angular Routing
Angular Services
TypeScript interfaces/models
Bootstrap-based responsive UI
Error and success message handling
Clean and maintainable code structure `

👨‍💻 Developer

Pratik Nimbalkar

Software Engineer
.NET | ASP.NET Core | Angular | SQL Server
