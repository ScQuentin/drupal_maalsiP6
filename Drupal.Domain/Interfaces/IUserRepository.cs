using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IUserRepository
{
    public User CreateUser(User user);
    public User GetById(Guid id);
    public User GetByGoogleId(string googleId);

}
