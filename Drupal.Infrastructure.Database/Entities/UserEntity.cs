namespace Drupal.Infrastructure.Database.Entities;

public record UserEntity(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    string GoogleId
    );