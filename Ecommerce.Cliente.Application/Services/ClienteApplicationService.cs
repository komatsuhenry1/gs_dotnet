using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces;
using Ecommerce.Cliente.Domain.Interfaces.Dtos;

namespace Ecommerce.Cliente.Application.Services
{
    public class ClienteApplicationService : IClienteApplicationService
    {
        private readonly IClienteRepository _repository;

        public ClienteApplicationService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public ClienteEntity? AdicionarCliente(IClienteDto entity)
        {
            return _repository.Adicionar(new ClienteEntity
            {
                Nome = entity.Nome,
                SobreNome = entity.SobreNome,
                Email = entity.Email,
                Idade = entity.Idade,
            });
        }

        public ClienteEntity? EditarCliente(int id, IClienteDto entity)
        {
            return _repository.Editar(new ClienteEntity
            {
                Id = id,
                Nome = entity.Nome,
                SobreNome = entity.SobreNome,
                Email = entity.Email,
                Idade = entity.Idade,
            });
        }

        public ClienteEntity? ObterClientePorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public IEnumerable<ClienteEntity>? ObterTodosClientes()
        {
            return _repository.ObterTodos();
        }

        public ClienteEntity? RemoverCliente(int id)
        {
            return _repository.Remover(id);
        }
    }
}
