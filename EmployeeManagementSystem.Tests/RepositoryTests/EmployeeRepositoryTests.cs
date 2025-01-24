using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Tests.Repositories
{
    [TestFixture]
    public class EmployeeRepositoryTests
    {
        private EmployeeDBContext _context;
        private EmployeeRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EmployeeDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new EmployeeDBContext(options);
            _repository = new EmployeeRepository(_context);

            // Seed test data
            _context.Employees.AddRange(
               new Employee { Id = 1, Name = "Sharath", Email = "sharath@trimble.com", Position = "Developer", Role = "Employee" },
                new Employee { Id = 2, Name = "David", Email = "david@trimble.com", Position = "Manager", Role = "Admin" }
            );
            _context.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            // Dispose of the context
            _context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            var employees = await _repository.GetAllEmpployees();
            employees.Should().HaveCount(2);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectEmployee()
        {
            var employee = await _repository.GetEmployeeDetailsById(1);
            employee.Should().NotBeNull();
            employee.Name.Should().Be("Sharath");
        }

        [Test]
        public async Task AddAsync_ShouldAddNewEmployee()
        {
            var newEmployee = new Employee { Name = "Karthik", Email = "karthik@trimbl.com", Position = "Tester", Role = "Employee" };
            await _repository.UpdateEmployeeDetailsById(newEmployee);

            var employees = await _repository.GetAllEmpployees();
            employees.Should().HaveCount(3);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateEmployeeDetails()
        {
            var employee = await _repository.GetEmployeeDetailsById(1);
            employee.Position = "Senior Developer";

            await _repository.UpdateEmployeeDetailsById(employee);

            var updatedEmployee = await _repository.GetEmployeeDetailsById(1);
            updatedEmployee.Position.Should().Be("Senior Developer");
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveEmployee()
        {
            await _repository.DeleteEmployeeDetailsById(1);
            var employees = await _repository.GetAllEmpployees();
            employees.Should().HaveCount(1);
        }
    }
}
