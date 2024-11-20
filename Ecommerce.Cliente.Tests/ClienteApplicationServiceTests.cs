using Ecommerce.Cliente.Application.Services;
using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces;
using Ecommerce.Cliente.Domain.Interfaces.Dtos;
using Moq;

namespace Ecommerce.Cliente.Tests
{
    public class ClienteApplicationServiceTests
    {
        private readonly Mock<IClienteRepository> _repositoryMock;
        private readonly ClienteApplicationService _clienteService;

        public ClienteApplicationServiceTests()
        {
            _repositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void AdicionarCliente_DeveRetornarClienteEntity_QuandoAdicionarComSucesso()
        {
            var clienteDtoMock = new Mock<IClienteDto>();
            clienteDtoMock.Setup(c => c.Nome).Returns("Murilo");
            clienteDtoMock.Setup(c => c.SobreNome).Returns("Jose");
            clienteDtoMock.Setup(c => c.Email).Returns("Murilo.jose@gmail.com");
            clienteDtoMock.Setup(c => c.Idade).Returns(20);

            var clienteEsperado = new ClienteEntity { Nome = "Murilo", SobreNome = "Jose", Email = "murilo.jose@gmail.com", Idade = 20 };
            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<ClienteEntity>())).Returns(clienteEsperado);

            var resultado = _clienteService.AdicionarCliente(clienteDtoMock.Object);

            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }

        [Fact]
        public void EditarCliente_DeveRetornarClienteEntity_QuandoEditarComSucesso()
        {
            var clienteDtoMock = new Mock<IClienteDto>();
            clienteDtoMock.Setup(c => c.Nome).Returns("Henry");
            clienteDtoMock.Setup(c => c.SobreNome).Returns("Komatsu");
            clienteDtoMock.Setup(c => c.Email).Returns("Henry.Komatsu@gmail.com");
            clienteDtoMock.Setup(c => c.Idade).Returns(25);

            var clienteEsperado = new ClienteEntity { Id = 1, Nome = "Henry", SobreNome = "Komatsu", Email = "henry.komatsu@gmail.com", Idade = 21 };
            _repositoryMock.Setup(r => r.Editar(It.IsAny<ClienteEntity>())).Returns(clienteEsperado);

            var resultado = _clienteService.EditarCliente(1, clienteDtoMock.Object);

            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Id, resultado.Id);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }

        [Fact]
        public void ObterClientePorId_DeveRetornarClienteEntity_QuandoClienteExiste()
        {
            var clienteEsperado = new ClienteEntity { Id = 1, Nome = "Arthur", SobreNome = "Koga", Email = "arthur.koga@gmail.com", Idade = 18 };
            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(clienteEsperado);

            var resultado = _clienteService.ObterClientePorId(1);

            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Id, resultado.Id);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }

        [Fact]
        public void ObterTodosClientes_DeveRetornarListaDeClientes_QuandoExistiremClientes()
        {
            var clientesEsperados = new List<ClienteEntity>
            {
                new ClienteEntity { Id = 1, Nome = "Gabriel", SobreNome = "Martins", Email = "gabriel.martins@gmail.com", Idade = 31 },
                new ClienteEntity { Id = 2, Nome = "Luiz", SobreNome = "Ferreira", Email = "luiz.ferreira@gmail.com", Idade = 21 }
            };
            _repositoryMock.Setup(r => r.ObterTodos()).Returns(clientesEsperados);

            var resultado = _clienteService.ObterTodosClientes();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Equal(clientesEsperados.First().Nome, resultado.First().Nome);
        }

        [Fact]
        public void RemoverCliente_DeveRetornarClienteEntity_QuandoRemoverComSucesso()
        {
            var clienteEsperado = new ClienteEntity { Id = 1, Nome = "Phelipe", SobreNome = "Souza", Email = "phelipe.souza@example.com", Idade = 40 };
            _repositoryMock.Setup(r => r.Remover(1)).Returns(clienteEsperado);

            var resultado = _clienteService.RemoverCliente(1);

            Assert.NotNull(resultado);
            Assert.Equal(clienteEsperado.Id, resultado.Id);
            Assert.Equal(clienteEsperado.Nome, resultado.Nome);
            Assert.Equal(clienteEsperado.SobreNome, resultado.SobreNome);
            Assert.Equal(clienteEsperado.Email, resultado.Email);
            Assert.Equal(clienteEsperado.Idade, resultado.Idade);
        }
    }
}
