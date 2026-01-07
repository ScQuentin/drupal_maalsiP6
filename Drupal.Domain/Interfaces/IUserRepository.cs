using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateUser();
    Task<User> GetById(Guid id);
    User GetByGoogleId(string googleId);

}
