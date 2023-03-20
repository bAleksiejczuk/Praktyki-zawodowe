using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Entities;
using Dapper;
using System.Data.SqlClient;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]

public class ApplicationController : ControllerBase
{
    private Repository _repository;

    public ApplicationController()
    {
        _repository = new Repository();
    }

    [HttpPost("PostArticle")]
    public IActionResult PostArticle([FromBody] Article article)
    {
        _repository.PostArticle(article);
        return NoContent();
    }

    [HttpPost("PostEvent")]
    public IActionResult PostEvent([FromBody] Event pEvent)
    {
        _repository.PostEvent(pEvent);
        return NoContent();
    }
    [HttpPost("PostProcedure")]
    public IActionResult PostProcedure([FromBody] Procedure procedure)
    {
        _repository.PostProcedure(procedure);
        return NoContent();
    }
    [HttpPost("PostStep")]
    public IActionResult PostStep([FromBody] Steps steps)
    {
        _repository.PostStep(steps);
        return NoContent();
    }
    [HttpGet("SelectAllArticles")]
    public IActionResult ArticleShow()
    {
        return Ok(_repository.ArticleShow());
    }
    [HttpGet("SelectAllEvents")]
    public IActionResult EventShow()
    {
        return Ok(_repository.EventShow());
    }
    [HttpGet("SelectAllProcedures")]
    public IActionResult ProcedureShow()
    {
        return Ok(_repository.ProcedureShow());
    }
    [HttpGet("ShowStepsForProcedure")]
    public IActionResult ProcedureStepsShow(int Id)
    {
        
        return Ok(_repository.ProcedureStepsShow(Id));
    }
    [HttpPost("PostComment")]
    public IActionResult PostComment([FromBody] Comment comment)
    {
        _repository.PostComment(comment);
        return NoContent();
    }
    [HttpGet("SelectAllArticlesWithComments")]
    public IActionResult CommentForArticleShow()
    {
        return Ok(_repository.CommentForArticleShow());
    }
    [HttpPost("AddUser")]
    public IActionResult CreateUser([FromBody] User user)
    {
        _repository.CreateUser(user);
        return NoContent();
    }
    [HttpPost("JoinToEvent/")]
    public IActionResult Like([FromBody] EventParticipants join)
    {
        _repository.UserWantToJoinEvent(join);
        return NoContent();
    }


}   