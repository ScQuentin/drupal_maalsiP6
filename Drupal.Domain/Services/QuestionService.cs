using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;

namespace Drupal.Domain.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    public async Task<Answer> AnswerQuestion(Guid questionId, Guid userId, Answer answer)
    {
        return answer;
    }

    public async Task<IEnumerable<Question>> GetUnansweredQuestionsByUserId(Guid userId)
        => await questionRepository.GetUnansweredByUserId(userId);
}
