using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Entities;
using Dapper;
using System.Data.SqlClient;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ForumController : ControllerBase
{
    private Repository _repository;

    public ForumController()
    {
        _repository = new Repository();
    }

    [HttpPost("CreateUser")]
    public IActionResult AddUser([FromBody] AddUser create)
    {
        _repository.AddUser(create);
        return NoContent();
    }

    [HttpDelete("DeleteUser/{id}")]
    public IActionResult DeleteUser([FromRoute] int id)
    {
        _repository.DeleteUser(id);
        return NoContent();
    }

    [HttpGet("ShowAllUsers")]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpPost("AddComment")]
    public IActionResult AddComment([FromBody] AddComment post)
    {
        _repository.AddComment(post);
        return NoContent();
    }

    [HttpDelete("DeleteComment/{id}")]
    public IActionResult DeleteComment([FromRoute] int id)
    {
        _repository.DeleteComment(id);
        return NoContent();
    }

    [HttpGet("ShowOneComment")]
    public IActionResult GetOne(int Id)
    {
        return Ok(_repository.GetOne(Id));
    }

    [HttpGet("ShowAllUserComments")]
    public IActionResult GetAllUserComments(int Id)
    {
        return Ok(_repository.GetAllUserComments(Id));
    }

    [HttpGet("ShowAllComments")]
    public IActionResult GetAllCommentsAndUserName()
    {
        return Ok(_repository.GetAllCommentsAndUserName());
    }

    [HttpGet("ShowChosedWord")]
    public IActionResult Search(string word)
    {
        return Ok(_repository.Search(word));
    }
    [HttpPost("like/")]
    public IActionResult Like([FromBody] Like like, int id)
    {
        _repository.Like(like, id);
        return NoContent();
    }
    [HttpDelete("DeleteLike/")]
    public IActionResult DeleteLike([FromBody] Like like)
    {
        _repository.DeleteLike(like);
        return NoContent();
    }
}


















