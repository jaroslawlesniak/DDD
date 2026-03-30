using BDA.Application.Common.Interfaces.Persistence;
using BDA.Domain.Entities;

namespace BDA.Infrastructure.Persistence;

public sealed class UserRepository : IUserRepository
{
    private static readonly List<User> Users = [];
    
    public User? GetUserByEmail(string email)
    {
       return Users.SingleOrDefault(x => x.Email == email);
    }

    public void Add(User user)
    {
        Users.Add(user);
    }
}