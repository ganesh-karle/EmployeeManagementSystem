# Employee Management System (EMS) 🚀

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-Code%20First-68217A)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC292B?logo=microsoftsqlserver&logoColor=white)
![Bootstrap 5](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap&logoColor=white)
![Chart.js](https://img.shields.io/badge/Chart.js-Analytics-FF6384?logo=chartdotjs&logoColor=white)

An enterprise-grade, modern web application developed using **ASP.NET Core MVC**, **Entity Framework Core (Code-First)**, and **SQL Server**. The application simplifies human resource operations through role-based access control, real-time analytics, automated attendance tracking, leave management, and dynamic reporting.

---

## 🌟 Key Features

### 1. 📊 Interactive Analytics Dashboard
* **Real-time KPI Metrics:** Instant workforce statistics including Total Headcount, Active Departments, Staff Present Today, Staff on Leave, and Available Leave quotas.
* **Working Hours Visualization:** Interactive weekly trend graphs comparing work hours vs. break hours powered by **Chart.js**.
* **Upcoming Holidays Widget:** Live company holiday schedule preview with categorization (National, Public, Festival).

### 2. 👥 Employee & Department Management
* **Full CRUD Operations:** Add, view, edit, and delete employee profiles with validations.
* **Department Allocation:** Categorize employees by departments (IT, HR, Finance, Marketing, Operations) with associated designations and compensations.
* **Server-Side Search, Filter & Pagination:** High-performance LINQ `Skip()` and `Take()` pagination with real-time text searching and department filtering.

### 3. 🕒 Workforce Attendance & Leave System
* **Daily Attendance Log:** Real-time tracking of daily clock-in/clock-out timestamps and punctuality statuses (**Present**, **Late**, **On Leave**, **Absent**).
* **Click-to-View Monthly Attendance:** Clicking on any staff member across the dashboard or employee tables asynchronously retrieves their monthly attendance & leave breakdown via a RESTful JSON endpoint (`/Employee/GetMonthlyAttendanceJson`) inside a quick modal.
* **Full Attendance Calendar:** Dedicated monthly attendance view (`/Employee/MonthlyAttendance/{id}`) providing comprehensive history and day-by-day logs.
* **Leave Balance Deduction:** Automatic calculation of remaining annual leave balance (starting from 18 annual leaves).

### 4. 🔐 Authentication & Role-Based Authorization
* **Secure Access Control:** Built using **ASP.NET Core Identity** and cookie authentication.
* **Role Permissions:**
  * **Admin:** Full administrative control (manage employees, edit departments, adjust roles, update attendance statuses).
  * **Employee:** Read-only access to directory, personal profile inspection, and dashboard metrics.
* **Claim-Based Authorization:** Pages restricted via `[Authorize(Roles = "Admin")]` attributes and claims configured during authentication.

### 5. 🎨 Modern UI & Responsive UX
* **Dual Theme Engine:** Instant toggle between **Light Mode** and high-contrast **Dark Mode** with state persistence via HTML5 `localStorage`.
* **Collapsible Sidebar:** Smooth `<` and `>` slide animation for maximizing screen real estate.
* **Application Settings Modal:** Floating modal for theme preferences, user role information, and database engine metadata.

### 6. 🛡️ Robust Architecture & Exception Handling
* **Repository Pattern & Service Layer:** Clean separation of concerns decoupling database queries from business rules and controllers.
* **Server-Side Validation:** Validations enforced using **Data Annotations** (`[Required]`, `[EmailAddress]`, `[Range]`, `[Phone]`).
* **Centralized Global Exception Handling:** Custom error middleware capturing unhandled exceptions and routing users to safe, informative views.

---

## 🏗️ Architecture & Design Pattern

The solution is structured using the **Repository Pattern** and **Service Layer** on top of ASP.NET Core MVC:

```
EmployeeManagementSystem/
├── Controllers/              # Handles incoming HTTP requests & routes
│   ├── AccountController.cs   # Authentication & Role management
│   ├── AttendanceController.cs# Attendance logs & status marking
│   ├── DepartmentController.cs# Department CRUD
│   ├── EmployeeController.cs  # Employee operations & Monthly Attendance JSON API
│   ├── HolidayController.cs   # Company holidays
│   └── HomeController.cs      # Analytics Dashboard & Chart datasets
├── Models/                   # Domain entities & ViewModels
│   ├── AttendanceRecord.cs
│   ├── Department.cs
│   ├── Employee.cs
│   ├── Holiday.cs
│   └── ViewModels/
├── Data/                     # ApplicationDbContext & SeedData
├── Repositories/             # Data access layer (Repository Pattern)
│   ├── IEmployeeRepository.cs & EmployeeRepository.cs
│   └── IDepartmentRepository.cs & DepartmentRepository.cs
├── Services/                 # Business logic layer
│   ├── IEmployeeService.cs & EmployeeService.cs
│   └── IDepartmentService.cs & DepartmentService.cs
├── Views/                    # Razor Views (.cshtml) with Bootstrap 5
└── wwwroot/                  # Static assets (CSS, JS, Fonts, Icons)
```

---

## 💻 Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | .NET 10.0 / ASP.NET Core MVC |
| **Language** | C# 13.0 |
| **ORM** | Entity Framework Core 10.0 (Code-First) |
| **Database** | Microsoft SQL Server / SQL Express |
| **Frontend** | Bootstrap 5.3, HTML5, CSS3, JavaScript, jQuery |
| **Icons & Charts**| FontAwesome 6, Chart.js |
| **Security** | ASP.NET Core Identity, Cookie Auth, BCrypt Password Hashing |

---

## 🚀 Getting Started

### Prerequisites
* [.NET SDK 10.0 or 8.0+](https://dotnet.microsoft.com/download)
* [SQL Server (Express or LocalDB)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* Visual Studio 2022 / VS Code / Rider

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/ganesh-karle/EmployeeManagementSystem.git
   cd EmployeeManagementSystem
   ```

2. **Configure Database Connection:**
   Update `appsettings.json` with your SQL Server instance:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.\\SQLEXPRESS;Database=EmployeeManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

3. **Run Database Migrations & Auto-Seeding:**
   The application automatically executes database migrations and populates initial sample data (Roles, Admin, Employee, Departments, Attendance Logs, and Holidays) upon launch.

4. **Run the Application:**
   ```bash
   dotnet run
   ```
   Open your browser and navigate to: `http://localhost:5000`

---

## 🔑 Default Login Credentials

| Role | Username / Email | Password |
|---|---|---|
| **Admin** | `admin@ems.com` | `Admin@123` |
| **Employee** | `employee@ems.com` | `Emp@123` |

---

## 👤 Author

* **Ganesh Karle**
* **GitHub:** [@ganesh-karle](https://github.com/ganesh-karle)
* **LinkedIn:** [Ganesh Karle](https://www.linkedin.com)
* **Email:** ganeshkarle1234@gmail.com
