
using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Models
{
    public class Client
    {
        public Client(string name, string phoneNumber, string emailAddress, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public string Address { get; private set; }
        public List<Patient> Pets { get; private set; } = new List<Patient>();
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

        public void AttachClientToMedic(Medic medic)
        {
            MedicId = medic.Id;
        }
    }
}
