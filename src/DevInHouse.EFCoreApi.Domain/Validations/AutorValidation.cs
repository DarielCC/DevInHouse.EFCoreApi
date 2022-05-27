using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevInHouse.EFCoreApi.Core.Entities;
using FluentValidation;

namespace DevInHouse.EFCoreApi.Domain.Validations
{
    public class AutorValidation : AbstractValidator<Autor>
    {
        public void ValidateAutor()
        {
            RuleFor(autor => autor.Nome)
                .NotEmpty().WithMessage("Nome inválido");

            RuleFor(autor => autor.Sobrenome)
                .NotEmpty().WithMessage("Sobrenome inválido");
        }
    }
}
