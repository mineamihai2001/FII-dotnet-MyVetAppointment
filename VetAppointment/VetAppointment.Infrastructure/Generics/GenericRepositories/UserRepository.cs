using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Models.AuthenticationModels;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {

        }
    }
}