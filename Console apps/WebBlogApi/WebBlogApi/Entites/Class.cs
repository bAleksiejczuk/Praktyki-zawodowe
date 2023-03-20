using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WebBlogApi.Entites
{



    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; } = default!;

        public virtual List<Post> Posts { get; set; } = default!;
    }



    public class Post

    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public int Like { get; set; }

    }


    public class Create
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }

    }
    public class Update
    {
        public string Title { get; set; }
        public string Content { get; set; }

    }

    public class Likee
    {
        public int Like { get; set; }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;

    }
 /*   public interface IStudentRepository : IDisposable
    {
        IEnumerable<Post> GetStudents();
        Post GetStudentByID(int studentId);
        void InsertStudent(Post student);
        void DeleteStudent(int studentID);
        void UpdateStudent(Post student);
        void Save();
    }*/
}













