using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Tests.Controllers
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeRepository> _mock;
        private EmployeeManagementController _controller;

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IEmployeeRepository>();
            _controller = new EmployeeManagementController(_mock.Object);
        }

        [Test]
        public async Task GetAllEmployees_Admin_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee {  Id = 1, Name = "Sharath", Email = "sharath@trimble.com", Position = "Developer", Role = "Employee" },
                new Employee { Id = 2, Name = "David", Email = "david@trimble.com", Position = "Manager", Role = "Admin" }
            };
            _mock.Setup(repo => repo.GetAllEmpployees()).ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployees("Admin") as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.As<IEnumerable<Employee>>().Should().HaveCount(2);
        }

        [Test]
        public async Task GetEmployeeById_Employee_ShouldReturnEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "Sharath", Email = "sharath@trimble.com", Position = "Developer", Role = "Employee" };
            _mock.Setup(repo => repo.GetEmployeeDetailsById(1)).ReturnsAsync(employee);

            // Act
            var result = await _controller.GetEmployeeById("Employee", 1) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.As<Employee>().Name.Should().Be("Sharath");
        }

        [Test]
        public async Task AddEmployee_Admin_ShouldAddEmployee()
        {
            // Arrange
            var newEmployee = new Employee { Id = 3, Name = "Karthik", Email = "karthik@trimbl.com", Position = "Tester", Role = "Employee" };

            // Act
            var result = await _controller.AddEmployee("Admin", newEmployee) as CreatedAtActionResult;

            // Assert
            result.Should().NotBeNull();
            result.Value.As<Employee>().Name.Should().Be("Karthik");
        }
    }
}
