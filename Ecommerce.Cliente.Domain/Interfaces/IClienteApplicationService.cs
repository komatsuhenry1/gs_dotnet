using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces.Dtos;

namespace Ecommerce.Cliente.Domain.Interfaces
{
    public interface IClienteApplicationService
    {
        IEnumerable<ClienteEntity> ObterTodosClientes();
        ClienteEntity ObterClientePorId(int id);
        ClienteEntity AdicionarCliente(IClienteDto entity);
        ClienteEntity EditarCliente(int id, IClienteDto entity);
        ClienteEntity RemoverCliente(int id);
    }
}
    