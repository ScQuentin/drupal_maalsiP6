namespace Drupal.Infrastructure.Database.Entities;

public class QuestionEntity
{
    public Guid Id { get; set; }
    public string Wording { get; set; }
    public ICollection<AnswerEntity> Answers { get; set; } = new List<AnswerEntity>();

};
