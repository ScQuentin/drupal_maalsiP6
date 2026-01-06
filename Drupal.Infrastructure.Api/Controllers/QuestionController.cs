using Drupal.Domain.Interfaces;
using Drupal.Domain.Models;
using Drupal.Infrastructure.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Drupal.Infrastructure.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class QuestionController(IQuestionService questionService) : ControllerBase
{
    [HttpGet("unanswered/{userId}")]
    public async Task<IActionResult> GetUnansweredQuestionsByUserId(Guid userId)
    {
        var questions = await questionService.GetUnansweredQuestionsByUserId(userId);
        return Ok(questions);
    }

    [HttpPost("vote")]
    public async Task<IActionResult> AnswerQuestion(AnswerDto answerDto)
    {
        var answer = questionService.AnswerQuestion(
            answerDto.QuestionId,
            answerDto.UserId,
            answerDto.Answer
        );
        return Ok(answer);
    }
}
