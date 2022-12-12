using FluentValidation;
using VetAppointment.Domain.Models;

namespace VetAppointment.Domain.Validators
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            RuleFor(x => x.BillingDate).NotEmpty().WithMessage("Billing date is requiered");
            RuleFor(x => x.PaymentSum).GreaterThan(0).WithMessage("Payment sum must be bigger than 0");
            RuleForEach(x => x.Prescription).SetValidator(new MedicineValidator());
        }
    }
}
