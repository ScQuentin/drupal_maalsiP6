using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;

namespace Drupal.Infrastructure.Database.Repositories;

public class QuestionRepository : IQuestionRepository
{
    public Question Create(Question question)
    {
        throw new NotImplementedException();
    }

    public Question Delete(int questionId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Question> GetAnsweredByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Question GetById(int questionId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Question> GetUnansweredByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Question Update(Question question)
    {
        throw new NotImplementedException();
    }
}
