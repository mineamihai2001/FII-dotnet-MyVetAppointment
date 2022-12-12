using FluentValidation;
using FluentValidation.Validators;
using VetAppointment.Domain.Models;

namespace VetAppointment.Domain.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Species).NotEmpty();
            RuleFor(x => x.Race).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.Weight).InclusiveBetween(1, 50);
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleForEach(x => x.Appointments).SetValidator(new AppointmentValidator());
        }
    }
}
