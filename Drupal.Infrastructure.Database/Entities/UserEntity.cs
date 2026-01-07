namespace Drupal.Infrastructure.Database.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string GoogleId { get; set; }
    public ICollection<AnswerEntity> Answers { get; set; } = new List<AnswerEntity>();

};  