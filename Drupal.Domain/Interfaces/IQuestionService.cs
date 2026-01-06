using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IQuestionService
{
    public Task<IEnumerable<Question>> GetUnansweredQuestionsByUserId(Guid userId);
    public Task<Answer> AnswerQuestion(Guid questionId, Guid userId, Answer answer);
}
