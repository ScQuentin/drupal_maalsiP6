namespace Drupal.Domain.Models;

public record Stats(
    Question Question,
    int AnsweredYes,
    int AnsweredNo
    );