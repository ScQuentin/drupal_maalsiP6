using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;

namespace Drupal.Infrastructure.Database.Repositories;

public class UserRepository : IUserRepository
{
    public User CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public User GetByGoogleId(string googleId)
    {
        throw new NotImplementedException();
    }

    public User GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
