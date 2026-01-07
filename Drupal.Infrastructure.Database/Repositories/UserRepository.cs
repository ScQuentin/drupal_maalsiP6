using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;
using Drupal.Infrastructure.Database.Entities;

namespace Drupal.Infrastructure.Database.Repositories;

public class UserRepository(DrupalDbContext context) : IUserRepository
{
    public async Task<Guid> CreateUser()
    {
        var entity = new UserEntity
        {
            Id = Guid.NewGuid()
        };
        context.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public User GetByGoogleId(string googleId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetById(Guid id)
    {

        var user = await context.Users.FindAsync(id);
        return new User(id);
    }
}
