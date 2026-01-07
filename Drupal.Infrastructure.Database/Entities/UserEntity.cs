namespace Drupal.Infrastructure.Database.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public ICollection<AnswerEntity> Answers { get; set; } = new List<AnswerEntity>();

};  