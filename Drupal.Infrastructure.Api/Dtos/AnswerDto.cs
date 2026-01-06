using Drupal.Domain.Models;

namespace Drupal.Infrastructure.Api.Dtos;

public record AnswerDto(
    Guid QuestionId,
    Guid UserId,
    Answer Answer
);
