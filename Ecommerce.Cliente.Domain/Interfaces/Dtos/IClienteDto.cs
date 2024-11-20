namespace Ecommerce.Cliente.Domain.Interfaces.Dtos
{
    public interface IClienteDto
    {
        string Nome { get; set; }
        string SobreNome { get; set; }
        string Email { get; set; }
        int Idade { get; set; }
        void Validate();
    }
}
