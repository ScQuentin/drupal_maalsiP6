using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IQuestionRepository
{
    Task<Question> GetById(Guid questionId);
    Task<IEnumerable<Question>> GetUnansweredByUserId(Guid userId);
    Task<IEnumerable<Question>> GetAnsweredByUserId(Guid userId);  
    Task<Question> Create(string wording);    
    Task<Question> Update(Question question);
    Task AnswerQuestion(Guid userId, Guid questionId, Answer answer);
    Task Delete(Guid questionId);

}
