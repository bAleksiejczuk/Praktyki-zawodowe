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

    [HttpGet("ShowAll")]
    public IActionResult ShowAll()
    {

        return Ok(_repository.ShowAll());
    }

    [HttpGet("Search")]
    public IActionResult Search([FromQuery] Search search)
    {

        return Ok(_repository.Search(search));
    }
    [HttpGet("SearchByPrice")]
    public IActionResult SearchByPrice(int From,int To)
    {

        return Ok(_repository.SearchByPrice(From,To));
    }

    [HttpGet("SearchByCategory")]
    public IActionResult SearchByCategory(string CategoryName)
    {

        return Ok(_repository.SearchByCategory(CategoryName));
    }


    [HttpPost("AddOrder")]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        _repository.CreateOrder(order);
        return NoContent();
    }

    [HttpGet("CompleteOrder")]
    public IActionResult CompleteOrder(string UserEmail)
    {

        return Ok(_repository.CompleteOrder(UserEmail));
    }
   [HttpPost("Register")]
    public IActionResult Register([FromBody] Register register)
    {
        _repository.Register(register);
        return NoContent();
    }

}   