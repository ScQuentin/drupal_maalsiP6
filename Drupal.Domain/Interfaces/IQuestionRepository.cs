using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IQuestionRepository
{
    public Question GetById(int questionId);
    public IEnumerable<Question> GetUnansweredByUserId(Guid userId);
    public IEnumerable<Question> GetAnsweredByUserId(Guid userId);  
    public Question Create(Question question);
    public Question Update(Question question);
    public Question Delete(int questionId);

}
