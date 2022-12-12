using FluentValidation;
using VetAppointment.Domain.Models;

namespace VetAppointment.Domain.Validators
{
    public class MedicValidator : AbstractValidator<Medic>
    {
        public MedicValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is requiered");
            RuleFor(x => x.PhoneNumber).Length(10).WithMessage("Please specify a valid phone number");
            RuleFor(x => x.EmailAddress).EmailAddress(
                FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                    .WithMessage("Please enter a valid email address");
            RuleForEach(x => x.Clients).SetValidator(new ClientValidator());
            RuleForEach(x => x.Patients).SetValidator(new PatientValidator());
        }
    }
}
