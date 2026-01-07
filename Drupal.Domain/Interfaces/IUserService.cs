using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IUserService
{
    Task<Guid> CreateUser();
    Task<User> GetById(Guid id);
}
