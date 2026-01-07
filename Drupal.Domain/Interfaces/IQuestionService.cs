using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IQuestionService
{
    Task<IEnumerable<Question>> GetUnansweredQuestionsByUserId(Guid userId);
    Task AnswerQuestion(Guid questionId, Guid userId, Answer answer);
    Task<Question> CreateQuestion(string wording);
}
