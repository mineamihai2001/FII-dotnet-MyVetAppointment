using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Domain.Models.AuthenticationModels;

namespace VetAppointment.Infrastructure.Generics.GenericFilters
{
    public class FilterUser : IFilter<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public FilterUser(string username, string password)
        {
            Username=username;
            Password=password;
        }

        public User? Filter(IEnumerable<User> queryableBase)
        {
            return queryableBase.Where(u => u.Username == Username && u.Password == Password).First();
        }
    }
}
