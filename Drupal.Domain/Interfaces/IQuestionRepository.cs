using Drupal.Domain.Models;

namespace Drupal.Domain.Interfaces;

public interface IQuestionRepository
{
    public Task<Question> GetById(int questionId);
    public Task<IEnumerable<Question>> GetUnansweredByUserId(Guid userId);
    public Task<IEnumerable<Question>> GetAnsweredByUserId(Guid userId);  
    public Task<Question> Create(Question question);    
    public Task<Question> Update(Question question);
    public void Delete(int questionId);

}
