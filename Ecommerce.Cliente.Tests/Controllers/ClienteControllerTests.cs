using Ecommerce.Cliente.API.Controllers;
using Ecommerce.Cliente.Application.Dtos;
using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Ecommerce.Cliente.Tests.Controllers
{
    public class ClienteControllerTests
    {
        private readonly Mock<IClienteApplicationService> _applicationServiceMock;
        private readonly ClienteController _controller;

        public ClienteControllerTests()
        {
            _applicationServiceMock = new Mock<IClienteApplicationService>();
            _controller = new ClienteController(_applicationServiceMock.Object);
        }

        [Fact]
        public void Get_DeveRetornarOkComListaDeClientes()
        {
            // Arrange
            var clientes = new List<ClienteEntity>
            {
                new ClienteEntity { Id = 1, Nome = "João", SobreNome = "Silva", Email = "joao@email.com", Idade = 30 },
                new ClienteEntity { Id = 2, Nome = "Maria", SobreNome = "Santos", Email = "maria@email.com", Idade = 25 }
            };

            _applicationServiceMock.Setup(service => service.ObterTodosClientes()).Returns(clientes);

            // Act
            var resultado = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(clientes, okResult.Value);
        }

        [Fact]
        public void GetPorId_DeveRetornarOkComCliente()
        {
            // Arrange
            var cliente = new ClienteEntity { Id = 1, Nome = "João", SobreNome = "Silva", Email = "joao@email.com", Idade = 30 };

            _applicationServiceMock.Setup(service => service.ObterClientePorId(1)).Returns(cliente);

            // Act
            var resultado = _controller.GetPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(cliente, okResult.Value);
        }

        [Fact]
        public void Post_DeveRetornarOkQuandoClienteCriado()
        {
            // Arrange
            var clienteDto = new ClienteDto { Nome = "João", SobreNome = "Silva", Email = "joao@email.com", Idade = 30 };
            var cliente = new ClienteEntity { Id = 1, Nome = "João", SobreNome = "Silva", Email = "joao@email.com", Idade = 30 };

            _applicationServiceMock.Setup(service => service.AdicionarCliente(clienteDto)).Returns(cliente);

            // Act
            var resultado = _controller.Post(clienteDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(cliente, okResult.Value);
        }
    }
}
