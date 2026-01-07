using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;

namespace Drupal.Domain.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    public async Task AnswerQuestion(Guid questionId, Guid userId, Answer answer)
        => await questionRepository.AnswerQuestion(userId, questionId, answer);

    public async Task<IEnumerable<Question>> GetUnansweredQuestionsByUserId(Guid userId)
        => await questionRepository.GetUnansweredByUserId(userId);

    public async Task<Question> CreateQuestion(string wording)
        => await questionRepository.Create(wording);

}
