using AutoMapper;
using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Repo;
using Employee.Portal.Service;
using Employee.Portal.Service.Dto;
using Employee.Portal.Service.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IRepository<EmployeeEntity>> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private IEmployeeService _service;
        public EmployeeServiceTests()
        {
            var expected = new EmployeeDto();

            _mockRepository = new Mock<IRepository<EmployeeEntity>>();
            _mockMapper = new Mock<IMapper>();

            _mockMapper.Setup(x => x.Map<EmployeeEntity, EmployeeDto>(It.IsAny<EmployeeEntity>())).Returns(new EmployeeDto() { Address = "as" });

            var employee = new EmployeeEntity()
            {
                Id = 1,
                FirstName = "Ali",
                LastName = "Essam",
                Address = "Giza",
                BirthDate = new System.DateTime(1991, 12, 10),
                Phone = "+201067645513"
            };

            var employees = new List<EmployeeEntity>() { employee };

            _mockRepository.Setup(s => s.Get(1)).Returns(Task.FromResult(employee));
            _mockRepository.Setup(s => s.SaveChangesAsync()).Returns(Task.FromResult(1));

            _service = new EmployeeService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateEmployee_ReturnTrue()
        {
            // Arrange
            var employee = new EmployeeRequest()
            {
                FirstName = "Ali",
                LastName = "Essam",
                Address = "Giza",
                BirthDate = new System.DateTime(1991, 12, 10),
                Phone = "+201067645513"
            };

            // Act
            var result = await _service.CreateEmployee(employee, Guid.NewGuid());

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateEmployee_ReturnTrue()
        {
            // Arrange
            var employee = new EmployeeRequest()
            {
                Id = 1,
                FirstName = "Ali",
                LastName = "Essam",
                Address = "Giza",
                BirthDate = new System.DateTime(1991, 12, 10),
                Phone = "+201067645513"
            };

            // Act
            var result = await _service.UpdateEmployee(employee, Guid.NewGuid());

            // Assert
            Assert.IsTrue(result);
        }



        [Test]
        public async Task UpdateEmployee_CheckInvalidEmployeeId()
        {
            // Arrange
            var employee = new EmployeeRequest()
            {
                Id = 2,
                FirstName = "Ali",
                LastName = "Essam",
                Address = "Giza",
                BirthDate = new System.DateTime(1991, 12, 10),
                Phone = "+201067645513"
            };

            // Act
            var result = await _service.UpdateEmployee(employee, Guid.NewGuid());

            // Assert
            Assert.False(result);
        }

        [Test]
        public async Task DeleteEmployee_ReturnTrue()
        {
            // Act
            var result = await _service.DeleteEmployee(1);

            // Assert
            Assert.True(result);
        }


        [Test]
        public async Task DeleteEmployee_CheckInvalidEmployeeId()
        {
            // Act
            var result = await _service.DeleteEmployee(2);

            // Assert
            Assert.False(result);
        }
    }
}