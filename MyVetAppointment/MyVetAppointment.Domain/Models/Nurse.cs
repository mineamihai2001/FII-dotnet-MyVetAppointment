namespace VetAppointment.Domain.Models
{
    public class Nurse
    {
        public Nurse(Guid id, string name, string phoneNumber, string emailAddress)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
    }
}
