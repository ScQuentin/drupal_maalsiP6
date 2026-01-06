using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;

namespace Drupal.Infrastructure.Database.Repositories;

public class QuestionRepository : IQuestionRepository
{
    public async Task<Question> Create(Question question)
    {
        throw new NotImplementedException();
    }

    public async void Delete(int questionId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Question>> GetAnsweredByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Question> GetById(int questionId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Question>> GetUnansweredByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Question> Update(Question question)
    {
        throw new NotImplementedException();
    }
}
