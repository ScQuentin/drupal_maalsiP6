using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;
using Drupal.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drupal.Infrastructure.Database.Repositories;

public class QuestionRepository(DrupalDbContext context) : IQuestionRepository
{
    public async Task<Question> Create(string wording)
    {
        var entity = new QuestionEntity
        {
            Id = Guid.NewGuid(),
            Wording = wording
        };
        context.Questions.Add(entity);
        await context.SaveChangesAsync();

        return new Question(entity.Id, entity.Wording);
    }

    public async Task Delete(Guid questionId)
    {
        var entity = await context.Questions.FindAsync(questionId);
        if (entity != null)
        {
            context.Questions.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Question> GetById(Guid questionId)
    {
        var entity = await context.Questions.FindAsync(questionId);

        if (entity == null)
            return null;

        return new Question(entity.Id, entity.Wording);
    }

    public async Task<IEnumerable<Question>> GetAnsweredByUserId(Guid userId)
    {
        var questions = await context.Answers
            .Where(a => a.UserId == userId)
            .Include(a => a.Question)
            .Select(a => a.Question)
            .Distinct()
            .ToListAsync();

        return questions.Select(q => new Question(q.Id, q.Wording));
    }

    public async Task<IEnumerable<Stats>> GetStats()
    {
        var questionsWithAnswers = await context.Questions
            .Include(q => q.Answers)
            .ToListAsync();

        var stats = questionsWithAnswers.Select(q => new Stats(
            new Question(q.Id, q.Wording),
            q.Answers.Count(a => a.Answer == Answer.Yes),
            q.Answers.Count(a => a.Answer == Answer.No)
        ));

        return stats;
    }


    public async Task<IEnumerable<Question>> GetUnansweredByUserId(Guid userId)
    {
        var answeredQuestionIds = await context.Answers
            .Where(a => a.UserId == userId)
            .Select(a => a.QuestionId)
            .ToListAsync();

        var unansweredQuestions = await context.Questions
            .Where(q => !answeredQuestionIds.Contains(q.Id))
            .ToListAsync();

        return unansweredQuestions.Select(q => new Question(q.Id, q.Wording));
    }

    public async Task AnswerQuestion(Guid userId, Guid questionId, Answer answer)
    {
        // 1. Vérifie si l'utilisateur existe
        var userExists = await context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            throw new KeyNotFoundException($"User with ID {userId} not found");

        // 2. Vérifie si la question existe
        var questionExists = await context.Questions.AnyAsync(q => q.Id == questionId);
        if (!questionExists)
            throw new KeyNotFoundException($"Question with ID {questionId} not found");

        // 3. Vérifie si l'utilisateur a déjà répondu à cette question
        var alreadyAnswered = await context.Answers
            .AnyAsync(a => a.UserId == userId && a.QuestionId == questionId);

        if (alreadyAnswered)
            throw new InvalidOperationException(
                $"User {userId} has already answered question {questionId}");

        // 4. Crée la réponse
        var answerEntity = new AnswerEntity
        {
            UserId = userId,
            QuestionId = questionId,
            Answer = answer,
        };

        context.Answers.Add(answerEntity);
        await context.SaveChangesAsync();
    }

    public async Task<Question> Update(Question question)
    {
        var entity = await context.Questions.FindAsync(question.Id);

        if (entity == null)
            throw new KeyNotFoundException($"Question with ID {question.Id} not found");

        entity.Wording = question.Wording;

        await context.SaveChangesAsync();

        return new Question(entity.Id, entity.Wording);
    }
}
