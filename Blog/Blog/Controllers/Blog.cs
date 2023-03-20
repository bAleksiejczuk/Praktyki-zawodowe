using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Entities;
using Dapper;
using System.Data.SqlClient;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] int id)
    {
        var _products = new List<Product>();

        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True"))
        {
            connection.Open();
            var querry = _products;
            String SelectAll = $"SELECT * FROM BlogTable";

            if (id > 0)
            {
                SelectAll = $"{SelectAll} WHERE Id={id}";
                _products = connection.Query<Product>(SelectAll).ToList();
            }
            else
            {
                _products = connection.Query<Product>(SelectAll).ToList();
            }
        }

        return Ok(_products);
    }
    [HttpPost]
    public IActionResult Create([FromBody] Create product)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True"))
        {
            connection.Open();
            String LastId = $"SELECT MAX(Id) FROM BlogTable;";
            int _id = connection.Query<int>(LastId).FirstOrDefault();

            var _product = new Product()
            {

                Id = _id + 1,
                Title = product.Title,
                Content = product.Content,
                Date = product.Date,
                Like = 0
            };

            String InsertTaC = $"INSERT INTO BlogTable(Id, Title, Content,[Date],[Like]) VALUES(@Id,@Title,@Content,@Date,@Like)";

            connection.Execute(InsertTaC, new
            {
                _product.Id,
                _product.Title,
                _product.Content,
                _product.Date,
                _product.Like,
            });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var _products = new Product();

        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True"))
        {
            connection.Open();
            String SearchLast = $"SELECT COUNT(*) FROM BlogTable WHERE Id = {id}";
            int _id = connection.Query<int>(SearchLast).FirstOrDefault();

            if (_id == 0)
            {
                return NotFound();
            }
            else
            {
                String Delete = $"Delete from BlogTable WHERE Id = {id}";
                connection.Execute(Delete);

                return NoContent();
            }
        }
    }
    [HttpPut("{id}")]
    public IActionResult Create([FromRoute] int id, [FromBody] Update product)
    {
        var _products = new Product();

        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True"))
        {
            connection.Open();
            String SearchLast = $"SELECT COUNT(*) FROM BlogTable WHERE Id = {id}";
            int _id = connection.Query<int>(SearchLast).FirstOrDefault();

            if (_id == 0)
            {
                return NotFound();
            }
            else
            {
                String UpdateTaC = $"UPDATE BlogTable SET Title='{product.Title}', Content = '{product.Content}' WHERE Id = {id}";
                connection.Execute(UpdateTaC);

                return Ok();
            }
        }
    }

    [HttpPut("like/{id}")]
    public IActionResult AddLike([FromRoute] int id, [FromBody] Product product)
    {
        var _products = new Product();

        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True"))
        {
            connection.Open();
            String SelectC = $"SELECT COUNT(*) FROM BlogTable WHERE Id = {id}";
            int _id = connection.Query<int>(SelectC).FirstOrDefault();

            if (_id == 0)
            {
                return NotFound();
            }
            else
            {
                String AddLike = $"UPDATE BlogTable SET [Like]=[Like]+1 WHERE Id = {id}";
                connection.Execute(AddLike);

                return Ok();
            }
        }
    }

    [HttpPut("dislike/{id}")]
    public IActionResult DisLike([FromRoute] int id, [FromBody] Product product)
    {
        var _products = new Product();

        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Blog;Integrated Security=True"))
        {
            connection.Open();
            String SelectC = $"SELECT COUNT(*) FROM BlogTable WHERE Id = {id}";
            String SelectL = $"SELECT [Like] FROM BlogTable WHERE Id = {id}";

            int _id = connection.Query<int>(SelectC).FirstOrDefault();
            int _id2 = connection.Query<int>(SelectL).FirstOrDefault();

            if (_id == 0)
            {
                return NotFound();
            }
            else
            {
                if (_id2 > 0)
                {
                    String DisLike = $"UPDATE BlogTable SET [Like]=[Like]-1 WHERE Id = {id}";
                    connection.Execute(DisLike);
                }

                return Ok();
            }
        }
    }
}
















