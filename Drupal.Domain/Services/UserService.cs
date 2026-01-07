using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;

namespace Drupal.Domain.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Guid> CreateUser()
        => await userRepository.CreateUser();

    public async Task<User> GetById(Guid id)
        => await userRepository.GetById(id);
}
