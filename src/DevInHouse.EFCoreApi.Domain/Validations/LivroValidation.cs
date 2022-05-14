using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevInHouse.EFCoreApi.Core.Entities;
using FluentValidation;

namespace DevInHouse.EFCoreApi.Domain.Validations
{
    public class LivroValidation : AbstractValidator<Livro>
    {
        public void ValidateLivro()
        {
            RuleFor(livro => livro.Titulo)
                .NotEmpty().WithMessage("Título inválido");

            RuleFor(livro => livro.DataPublicacao)
                .NotEmpty().WithMessage("Data inválido");

            RuleFor(livro => livro.Preco)
                .GreaterThan(0).WithMessage("Preço inválido");
        }
    }
}
