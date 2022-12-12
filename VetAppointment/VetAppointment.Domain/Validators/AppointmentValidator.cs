using FluentValidation;
using VetAppointment.Domain.Models;

namespace VetAppointment.Domain.Validators
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is requiered.");
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is requiered")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be after Start Date");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Appointment description is requiered.");
        }
    }
}
