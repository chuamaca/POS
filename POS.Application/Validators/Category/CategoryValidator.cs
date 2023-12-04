using FluentValidation;
using POS.Application.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Validators.Category
{
    public class CategoryValidator : AbstractValidator<CategoryRequestDTO>
    {
        //Validar Campos 
        public CategoryValidator() {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser Nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser Vacio");
        
        }

    }
}
