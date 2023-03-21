using Dapper;
using System.Data.SqlClient;

namespace WebApi.Entities;

public interface IRepository
{
    void AddUser(AddUser create);
    void AddComment(AddComment comment);
    void DeleteUser(int Id);
    void DeleteComment(int Id);
    Search GetOne(int Id);
    IEnumerable<User> GetAll();
    IEnumerable<Post> GetAllUserComments(int Id);
    IEnumerable<SearchAll> GetAllCommentsAndUserName();
    IEnumerable<Search> Search(string word);
    void Like(Like like, int id);
    void DeleteLike(Like like);
}

public class Repository : IRepository
{
    public void AddComment(AddComment comment)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            connection.Open();
            String lastId = $"SELECT MAX(Id) FROM Comment;";
            int _id = connection.Query<int>(lastId).FirstOrDefault();


            var _comment = new Post()
            {
                Id = _id + 1,
                Content = comment.Content,
                UserId = comment.UserId,

            };

            String insert = $"INSERT INTO Comment(Id,Content,UserId) VALUES(@Id,@Content,@UserId)";

            connection.Execute(insert, new
            {
                _comment.Id,
                _comment.Content,
                _comment.UserId,

            });
        }
    }

    public void AddUser(AddUser addUser)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            connection.Open();
            String LastId = $"SELECT MAX(Id) FROM [User];";
            int _id = connection.Query<int>(LastId).FirstOrDefault();

            var _user = new User()
            {
                Id = _id + 1,
                Name = addUser.Name,
            };

            String InsertTaC = $"INSERT INTO [User](Id,Name) VALUES(@Id,@Name)";

            connection.Execute(InsertTaC, new
            {
                _user.Id,
                _user.Name,
            });
        }
    }

    public IEnumerable<Search> Search(string word)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            var _word = new List<Search>();

            connection.Open();
            String SearchWord = $"SELECT [User].[Name], Content,[Like] From Comment Inner JOIN [User] On Comment.UserId = [User].Id Where Content LIKE '%{word}%';";
            _word = connection.Query<Search>(SearchWord).ToList();

            return _word;
        }
    }

    public void DeleteComment(int Id)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            connection.Open();
            String SearchId = $"SELECT Id FROM Comment WHERE Id = {Id}";
            int _id = connection.Query<int>(SearchId).FirstOrDefault();

            if (_id > 0)
            {
                String Delete = $"Delete from Comment WHERE Id = {Id}";
                connection.Execute(Delete);
            }
        }
    }

    public void DeleteUser(int Id)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            connection.Open();
            String SearchId = $"SELECT Id FROM [User] WHERE Id = {Id}";
            int _id = connection.Query<int>(SearchId).FirstOrDefault();

            if (_id > 0)
            {
                String Delete = $"Delete from [User] WHERE Id = {Id}";
                String userCommentDelete = $"Delete from Comment WHERE UserId = {Id}";

                connection.Execute(userCommentDelete);
                connection.Execute(Delete);
            }
        }
    }

    public IEnumerable<User> GetAll()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {


            var _all = new List<User>();
            connection.Open();
            String SelectAll = $"SELECT * FROM [User]";
            _all = connection.Query<User>(SelectAll).ToList();



            return _all;
        }
    }

    public IEnumerable<SearchAll> GetAllCommentsAndUserName()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            var _allComments = new List<SearchAll>();

            connection.Open();
            String SelectAllComment = $"SELECT Comment.Id, [User].Name, Content From Comment Inner JOIN [User] On Comment.UserId = [User].Id";
            _allComments = connection.Query<SearchAll>(SelectAllComment).ToList();

            return _allComments;
        }
    }

    public IEnumerable<Post> GetAllUserComments(int Id)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            var _allUserComments = new List<Post>();

            connection.Open();
            String SelectAllUserComments = $"SELECT Comment.Id, Comment.UserId, Content From Comment Inner JOIN [User] On Comment.UserId = [User].Id Where[User].Id = {Id}";
            _allUserComments = connection.Query<Post>(SelectAllUserComments).ToList();

            return _allUserComments;
        }
    }

    public Search GetOne(int Id)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {
            var _oneComment = new Search();


            connection.Open();
            String SelectOne = $"SELECT[User].[Name], Content  From Comment Inner JOIN[User] On Comment.UserId = [User].Id WHERE [Comment].Id = {Id} ";

            String likes = $"SELECT COUNT(*) From [Like] WHERE CommentId = {Id}";
            int _likes = connection.Query<int>(likes).FirstOrDefault();
            _oneComment = connection.Query<Search>(SelectOne).FirstOrDefault();
            _oneComment.Likes = _likes;




            return _oneComment;
        }
    }

    public void Like(Like like, int Id)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {


            connection.Open();


            String selectLikeId = $"SELECT MAX(Id) FROM [Like];";
            int _likeId = connection.Query<int>(selectLikeId).FirstOrDefault();

            String find = $"Select UserId,CommentId From [Like] WHERE UserId = @UserId AND CommentId = @CommentId";
            int _find = connection.Query<int>(find, new
            {
                like.UserId,
                like.CommentId
            }).FirstOrDefault();

            var _comment = new Like()
            {
                Id = _likeId + 1,
                UserId = like.UserId,
                CommentId = like.CommentId,

            };



            if (_find == 0)
            {

                String insert = $"INSERT INTO [Like](Id,UserId,CommentId) VALUES(@Id,@UserId,@CommentId)";



                connection.Execute(insert, new
                {
                    _comment.Id,
                    _comment.UserId,
                    _comment.CommentId,
                });
            }
        }
    }

    public void DeleteLike(Like like)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Forum;Integrated Security=True"))
        {


            connection.Open();


            String selectLikeId = $"SELECT MAX(Id) FROM [Like];";
            int _likeId = connection.Query<int>(selectLikeId).FirstOrDefault();



            var _comment = new Like()
            {

                UserId = like.UserId,
                CommentId = like.CommentId,

            };

            String insert = $"Delete from [Like] WHERE UserId = @UserId AND CommentId = @CommentId ";
            connection.Execute(insert, new
            {
              
                _comment.UserId,
                _comment.CommentId,
            });

        }
    }

}





