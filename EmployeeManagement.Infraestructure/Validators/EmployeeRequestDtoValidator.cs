using EmployeeManagement.Entity.Dto.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeManagement.Infraestructure.Validators
{
    public class EmployeeRequestDtoValidator :  AbstractValidator<EmployeeRequestDto>
    {
        public EmployeeRequestDtoValidator()
        {
            RuleFor(s => s.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(s => s.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(s => s.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(s => s.Position).NotEmpty().WithMessage("Position is required");
            RuleFor(s => s.Email).NotEmpty().WithMessage("Email is required")
                .Must(ValidateEmail).WithMessage("A valid email is required");
        }

        private bool ValidateEmail(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
                return false;

            const string emailPattern = @"^(?!.*\.\.)[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z]{2,})+$";

            var regex = new Regex(emailPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(account);
        }


    }
}
