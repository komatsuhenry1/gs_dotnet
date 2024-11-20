using Ecommerce.Cliente.Data.AppData;
using Ecommerce.Cliente.Domain.Entities;
using Ecommerce.Cliente.Domain.Interfaces;

namespace Ecommerce.Cliente.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationContext _context;

        public ClienteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public ClienteEntity? Adicionar(ClienteEntity cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public ClienteEntity? Editar(ClienteEntity cliente)
        {
            var entity = _context.Cliente.Find(cliente.Id);
            
            if (entity is not null)
            {
                entity.Nome = cliente.Nome;
                entity.SobreNome = cliente.SobreNome;
                entity.Email = cliente.Email;
                entity.Idade = cliente.Idade;
                
                _context.Cliente.Update(entity);
                _context.SaveChanges();
            }
            return null;    
        }

        public ClienteEntity? ObterPorId(int id)
        {
            var entity = _context.Cliente.Find(id);
            
            if (entity is not null)
            {
                return entity;
            }
            return null;   
        }

        public IEnumerable<ClienteEntity> ObterTodos()
        {
            var entity = _context.Cliente.ToList();

            return entity;
        }

        public ClienteEntity? Remover(int id)
        {
            var entity = _context.Cliente.Find(id);
            
            if (entity is not null)
            {
                _context.Cliente.Remove(entity);
                _context.SaveChanges();

                return entity;
            }
            return null;  
        }
    }
}
