using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


using System.ComponentModel.DataAnnotations;
using WebBlogApi.Entites;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet("One")]
    public IActionResult GetOne([FromQuery] int id)
    {


        using (var db = new BloggingContext())
        {

            var post = db.Posts.Where(x => x.Id == id).FirstOrDefault();
            var posts = db.Posts.ToList();


            return Ok(post);
        }
    }
    [HttpGet("All")]
    public IActionResult GetAll()
    {


        using (var db = new BloggingContext())
        {


            var posts = db.Posts.ToList();


            return Ok(posts);
        }
    }
    [HttpPost]
    public IActionResult Create([FromBody] Create create)
    {

        {
            using (var db = new BloggingContext())
            {
                var post = new Post
                {

                    Title = create.Title,
                    Content = create.Content,
                    Date = create.Date,


                };
                db.Posts.Add(post);
                db.SaveChanges();
            }


        }

        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        using (var db = new BloggingContext())
        {

            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var post = db.Posts.Where(x => x.Id == id).FirstOrDefault();
                db.Posts.Remove(post);
                db.SaveChanges();
                return NoContent();

            }


        }
    }
    [HttpPut("{id}")]
    public IActionResult Create([FromRoute] int id, [FromBody] Update update)
    {



        using (var db = new BloggingContext())
        {
            var posts = new Post
            {

                Title = update.Title,
                Content = update.Content,

            };
            if (id == 0)
            {
                return NotFound();
            }
            else
            {


                var post = db.Posts.Where(x => x.Id == id).FirstOrDefault();

                post.Title = posts.Title;
                post.Content = posts.Content;
                db.SaveChanges();

                return Ok();
            }
        }
    }
    [HttpPut("like/{id}")]
    public IActionResult AddLike([FromRoute] int id, [FromBody] Likee like)
    {


        using (var db = new BloggingContext())
        {


            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var post = db.Posts.Where(x => x.Id == id).FirstOrDefault();
                post.Like = post.Like + 1 ;
                db.SaveChanges();
                return Ok();
            }
        }
    }
    [HttpPut("dislike/{id}")]
    public IActionResult DisLike([FromRoute] int id, [FromBody] Likee like)
    {


        using (var db = new BloggingContext())
        {

            var post = db.Posts.Where(x => x.Id == id).FirstOrDefault();
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                if (post.Like > 0)
                {
                    post.Like = post.Like - 1;
                    db.SaveChanges();
                   
                }
                return Ok();
            }
        }
    }
}





