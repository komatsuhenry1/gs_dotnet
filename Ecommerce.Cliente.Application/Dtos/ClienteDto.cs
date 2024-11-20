using Ecommerce.Cliente.Domain.Interfaces.Dtos;
using FluentValidation;

namespace Ecommerce.Cliente.Application.Dtos
{
    public class ClienteDto: IClienteDto
    {
        public string Nome { get; set; } = string.Empty;
        public string SobreNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Idade { get; set; }
        public void Validate()
        {
            var validateResult = new CategoriaDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new ArgumentException(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class CategoriaDtoValidation : AbstractValidator<ClienteDto>
    {
        public CategoriaDtoValidation()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3).WithMessage(x => $"O campo {nameof(x.Nome)}, deve ter no minimo 3 caracteres")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Nome)}, não pode ser vazio");
            
            RuleFor(x => x.SobreNome)
                .MinimumLength(3).WithMessage(x => $"O campo {nameof(x.SobreNome)}, deve ter no minimo 3 caracteres")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.SobreNome)}, não pode ser vazio");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(x => $"O {nameof(x.Email)}, não é valido")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Equals)}, não pode ser vazio");
        }
    }
}
