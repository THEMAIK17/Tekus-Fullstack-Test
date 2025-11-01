using Moq;
using Tekus.Application.Services;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Tests.Application;

public class ProviderServiceTests
    {
        private readonly Mock<IProviderRepository> _mockProviderRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ProviderService _service;

        // Constructor: Sets up a fresh, isolated service for each test.
        public ProviderServiceTests()
        {
            _mockProviderRepo = new Mock<IProviderRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ProviderService(_mockProviderRepo.Object, _mockUnitOfWork.Object);
        }

        // Test 1: Validates the successful creation path.
        [Fact]
        public async Task CreateProviderAsync_WhenProviderIsValid_ShouldCallAddAndSaveChanges()
        {
            // Arrange
            var newProvider = new Provider { Id = 1, Name = "Test Provider", Nit = "12345" };

            // Act
            await _service.CreateProviderAsync(newProvider);

            // Assert
            _mockProviderRepo.Verify(repo => repo.Add(newProvider), Times.Once());
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Once());
        }

        // Test 2: Validates the business rule for an empty Name.
        [Fact]
        public async Task CreateProviderAsync_WhenNameIsEmpty_ShouldThrowException()
        {
            // Arrange
            var invalidProvider = new Provider { Id = 1, Name = "", Nit = "12345" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => 
                _service.CreateProviderAsync(invalidProvider)
            );

            Assert.Equal("Provider name cannot be empty.", exception.Message);
            _mockProviderRepo.Verify(repo => repo.Add(It.IsAny<Provider>()), Times.Never());
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Never());
        }
        
        // Test 3: Validates the business rule for an empty NIT.
        [Fact]
        public async Task CreateProviderAsync_WhenNitIsEmpty_ShouldThrowException()
        {
            // Arrange
            var invalidProvider = new Provider { Id = 1, Name = "Test Provider", Nit = "" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(() => 
                _service.CreateProviderAsync(invalidProvider)
            );

            Assert.Equal("Provider NIT cannot be empty.", exception.Message);
            _mockProviderRepo.Verify(repo => repo.Add(It.IsAny<Provider>()), Times.Never());
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(default), Times.Never());
        }
    }
