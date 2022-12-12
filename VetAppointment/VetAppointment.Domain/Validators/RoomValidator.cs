using FluentValidation;
using VetAppointment.Domain.Models;

namespace VetAppointment.Domain.Validators
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Room type is required");
            RuleFor(x => x.RoomNumber)
                .NotEmpty()
                .WithMessage("Room number is required")
                .GreaterThan(0)
                .WithMessage("Room number must be greater than 0");
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Room capacity is required")
                .GreaterThan(0)
                .WithMessage("Room capacity must be greater than 0");
            RuleForEach(x => x.Appointments).SetValidator(new AppointmentValidator());
        }
    }
}
