using FluentValidation;
using VetAppointment.Domain.Models;

namespace VetAppointment.Domain.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.PhoneNumber).Length(10).WithMessage("Please specify a valid phone number");
            RuleFor(x => x.Address).Length(10, 250);
            RuleFor(x => x.EmailAddress).EmailAddress(
                FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Please enter a valid email address");
            RuleForEach(x => x.Pets).SetValidator(new PatientValidator());
            RuleForEach(x => x.Billings).SetValidator(new BillValidator());
        }
    }
}
