# Employee Records Management System

## **Project Overview**
This project implements a simple Employee Records Management System using **C# .NET Core Web API** targeting **.NET 7**. It provides APIs to manage employee records, including adding, updating, retrieving, and deleting employees. The system ensures role-based access control, input validation, error handling, and comprehensive unit tests.

---

## **Features**
1. **Employee Management**
   - Add employees (Admin only).
   - Retrieve a list of employees or individual employee details.
   - Update employee details (Admin or the employee themselves).
   - Delete employees (Admin only).

2. **Role-Based Access Control**
   - Role passed as a header parameter (`Admin` or `Employee`).
   - Unauthorized access returns proper HTTP 401 (Unauthorized).

3. **Input Validation**
   - Ensures valid data is provided for employee properties.

4. **Error Handling**
   - Returns meaningful HTTP status codes for validation and runtime errors.

5. **Unit Testing**
   - Achieves 75%+ code coverage using **NUnit**.

6. **In-Memory Database**
   - Uses Entity Framework Core's InMemory provider for simplicity.

---

## **Technologies Used**
- **.NET 7**
- **Entity Framework Core (InMemory)**
- **NUnit** for unit testing
- **Swagger** for API documentation
- **Postman** for API testing

---

## **Installation Instructions**
1. **Prerequisites**
   - Install .NET SDK 7 or higher.
   - Install a code editor (e.g., Visual Studio or Visual Studio Code).

2. **Clone the Repository**
   ```bash
   git clone <repository_url>
   cd EmployeeManagementSystem
   ```

3. **Build the Project**
   ```bash
   dotnet build
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```
   The API will be available at `http://localhost:5000`.

5. **Run Unit Tests**
   ```bash
   dotnet test
   ```
   Ensure all tests pass successfully.

---

## **API Endpoints**

### **POST /employees**
- **Access:** Admin only
- **Description:** Adds a new employee.
- **Request Body:**
  ```json
  {
    "name": "Sharath",
    "email": "sharath@trimble.com",
    "position": "Senior Developer",
    "salary": 2000000,
    "role": "Employee"
  }
  ```
- **Response:**
  - `201 Created`: Employee added successfully.
  - `401 Unauthorized`: Role missing or not Admin.
  - `400 Bad Request`: Validation errors.

---

### **GET /employees**
- **Access:** All roles
- **Description:** Retrieves a list of employees.
- **Response:**
  - `200 OK`: List of employees.

---

### **GET /employees/{id}**
- **Access:** All roles
- **Description:** Retrieves details of an employee by ID.
- **Response:**
  - `200 OK`: Employee details.
  - `404 Not Found`: Employee not found.

---

### **PUT /employees/{id}**
- **Access:** Admin or the employee themselves
- **Description:** Updates employee details (except salary).
- **Request Body:**
  ```json
  {
    "name": "Updated Name",
    "email": "updated.email@example.com",
    "position": "Updated Position"
  }
  ```
- **Response:**
  - `204 No Content`: Employee updated successfully.
  - `401 Unauthorized`: Role missing or unauthorized user.
  - `404 Not Found`: Employee not found.
  - `400 Bad Request`: Validation errors.

---

### **DELETE /employees/{id}**
- **Access:** Admin only
- **Description:** Deletes an employee by ID.
- **Response:**
  - `204 No Content`: Employee deleted successfully.
  - `401 Unauthorized`: Role missing or not Admin.
  - `404 Not Found`: Employee not found.

---

## **Testing the Application**

### **Swagger**
1. Run the application.
2. Navigate to `http://localhost:5000/swagger` to access Swagger UI.
3. Use Swagger to test the API endpoints interactively.

### **Postman Collection**
- Import the provided Postman collection (`PostmanCollection.json`) into Postman.
- Configure headers (e.g., `role` as `Admin` or `Employee`).
- Test each endpoint with various scenarios.

---

## **Known Issues and Limitations**
1. The role is passed via the header, which is not secure for production-grade systems.
2. Data is not persisted beyond application runtime due to the use of the InMemory database.
3. No authentication or user management is implemented.

---

## **Author**
Sharath S 
Senior Software Engineer.

