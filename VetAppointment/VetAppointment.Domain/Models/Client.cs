
using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Models
{
    public class Client
    {
        public Client(string name, string phoneNumber, string emailAddress, string address, Guid medicId)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
            MedicId = medicId;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public string Address { get; private set; }
        public List<Patient> Pets { get; private set; } = new List<Patient>();
        public List<Bill> Billings { get; private set; } = new List<Bill>();
        public Guid MedicId { get; private set; }

        public Result RegisterPetsToClient(List<Patient> pets)
        {
            if (!pets.Any())
            {
                return Result.Failure("Add at least a pet to the client");
            }

            pets.ForEach(pet =>
            {
                pet.AttachPetToOwner(this);
                Pets.Add(pet);
            });

            return Result.Success();
        }

        public Result RegisterBillingsToClient(List<Bill> billings)
        {
            if (!billings.Any())
            {
                return Result.Failure("Add at least a billing to the client");
            }

            billings.ForEach(billing =>
            {
                billing.AttachBillToClient(this);
                Billings.Add(billing);
            });

            return Result.Success();
        }

        public void AttachClientToMedic(Medic medic)
        {
            MedicId = medic.Id;
        }
    }
}
