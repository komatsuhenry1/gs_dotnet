using Ecommerce.Cliente.Domain.Entities;

namespace Ecommerce.Cliente.Domain.Interfaces
{
    public interface IClienteRepository
    {
        ClienteEntity? ObterPorId(int id);
        IEnumerable<ClienteEntity>? ObterTodos();
        ClienteEntity? Adicionar(ClienteEntity cliente);
        ClienteEntity? Editar(ClienteEntity cliente);
        ClienteEntity? Remover(int id);
    }
}