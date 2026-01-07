using Drupal.Domain.Models;

namespace Drupal.Infrastructure.Database.Entities;

public class AnswerEntity
{
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public Answer Answer { get; set; }
    public UserEntity User { get; set; } = null!;
    public QuestionEntity Question { get; set; } = null!;
}
