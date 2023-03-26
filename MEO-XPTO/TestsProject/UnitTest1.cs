using MEO_XPTO.Controllers;
using MEO_XPTO.Data;
using MEO_XPTO.Models.Business;
using MEO_XPTO.Models.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestsProject
{

    [TestFixture]
    public class FormularioControllerTests
    {
        private XptoFormularioController _formularioController;
        private Mock<ILogger<XptoFormularioController>> _loggerMock;
        private Mock<ApplicationDbContext> _dbContextMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<XptoFormularioController>>();
            _dbContextMock = new Mock<ApplicationDbContext>();
            _formularioController = new XptoFormularioController(_loggerMock.Object, _dbContextMock.Object);
        }

        [Test]
        public void Post_WhenFormularioIsUnique_ShouldAddFormularioToDbContextAndReturnOk()
        {
            // Arrange
            var formulario = new FormularioViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                IsSubscribedToNewsletter = false
            };
            var dbContextMock = new Mock<ApplicationDbContext>();
            dbContextMock.Setup(m => m.Formularios.Where(f => f.Email == formulario.Email).Count())
                .Returns(0);
            var expectedFormulario = new Formulario(formulario.FirstName, formulario.LastName,
                        formulario.Email,
                        formulario.PhoneNumber, formulario.IsSubscribedToNewsletter);
            var expectedStatusCode = new OkResult().StatusCode;

            // Act
            var result = _formularioController.Post(formulario) as OkResult;

            // Assert
            dbContextMock.Verify(m => m.Formularios.Add(expectedFormulario), Times.Once);
            dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStatusCode, result.StatusCode);
        }

        [Test]
        public void Post_WhenFormularioIsNotUnique_ShouldReturnException()
        {
            // Arrange
            var formulario = new FormularioViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                IsSubscribedToNewsletter = false
            };
            var dbContextMock = new Mock<ApplicationDbContext>();
            dbContextMock.Setup(m => m.Formularios.Where(f => f.Email == formulario.Email).Count())
                .Returns(1);
            var expectedMessage = "Cannot create multiple accounts using the same email.";

            // Act & Assert
            Assert.Throws<Exception>(() => _formularioController.Post(formulario), expectedMessage);
        }

        [Test]
        public void Post_WhenDbSaveChangesThrowsException_ShouldReturnException()
        {
            // Arrange
            var formulario = new FormularioViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                IsSubscribedToNewsletter = false
            };
            var dbContextMock = new Mock<ApplicationDbContext>();
            dbContextMock.Setup(m => m.Formularios.Where(f => f.Email == formulario.Email).Count())
                .Returns(0);
            dbContextMock.Setup(m => m.SaveChanges())
                .Throws<Exception>();
            var expectedMessage = "An error occurred while saving the form.";

            // Act & Assert
            Assert.Throws<Exception>(() => _formularioController.Post(formulario), expectedMessage);
        }
    }
}