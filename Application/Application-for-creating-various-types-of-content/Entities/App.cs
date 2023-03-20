using Dapper;
using System.Data.SqlClient;

namespace WebApi.Entities;



public class Repository
{
    public void PostArticle(Article article)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            connection.Open();



            var newArticle = new Article()
            {

                Title = article.Title,
                Content = article.Content,


            };

            String insert = $"INSERT INTO Article(Title,Content) VALUES(@Title,@Content)";

            connection.Execute(insert, new
            {

                newArticle.Title,
                newArticle.Content,


            });
        }
    }
    public void PostEvent(Event pEvent)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            connection.Open();



            var @event = new Event()
            {

                Name = pEvent.Name,
                Description = pEvent.Description,
                StartTime = pEvent.StartTime,
                EndTime = pEvent.EndTime,
                Place = pEvent.Place,


            };

            String insert = $"INSERT INTO Event([Name],[Description],StartTime,EndTime,Place) VALUES(@Name,@Description,@StartTime,@EndTime,@Place)";

            connection.Execute(insert, new
            {

                pEvent.Name,
                pEvent.Description,
                pEvent.StartTime,
                pEvent.EndTime,
                pEvent.Place


            });
        }

    }
    public void PostProcedure(Procedure procedure)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            connection.Open();



            var newProcedure = new Procedure()
            {

                Title = procedure.Title,
                Description = procedure.Description


            };

            String insert = $"INSERT INTO [Procedure](Title,[Description]) VALUES(@Title,@Description)";

            connection.Execute(insert, new
            {

                newProcedure.Title,
                newProcedure.Description,


            });
        }

    }
    public void PostStep(Steps steps)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            connection.Open();



            var newStep = new Steps()
            {

                Number = steps.Number,
                Step = steps.Step,
                ProcedureId = steps.ProcedureId


            };

            String insert = $"INSERT INTO [Steps]([Number],Step,ProcedureId) VALUES(@Number,@Step,@ProcedureId)";

            connection.Execute(insert, new
            {

                newStep.Number,
                newStep.Step,
                newStep.ProcedureId


            });
        }

    }
    public IEnumerable<Article> ArticleShow()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            var show = new List<Article>();
            connection.Open();
            String selectTitleAndContent = $"SELECT Title,Content FROM Article";
            show = connection.Query<Article>(selectTitleAndContent).ToList();
            return show;
        }


    }
    public IEnumerable<Event> EventShow()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            var show = new List<Event>();
            connection.Open();
            String selectEvent = $"SELECT Id,Name,Description,StartTime,EndTime,Place FROM Event";
            show = connection.Query<Event>(selectEvent).ToList();
            if (show != null)
            {
                foreach (var item in show)
                {
                    String SelectOne = $"SELECT [User].[Name] [UserName] From [User] INNER JOIN [EventParticipants] On [EventParticipants].UserId = [User].Id INNER JOIN [Event] ON [Event].Id = [EventParticipants].EventId WHERE EventId ={item.Id}";



                    item.UserName = connection.Query<User>(SelectOne).ToList();
                }

            }
            return show;
        }


    }
    public IEnumerable<Procedure> ProcedureShow()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            var show = new List<Procedure>();
            connection.Open();
            String selectEvent = $"SELECT Id,[Title],[Description] FROM [Procedure]";
            show = connection.Query<Procedure>(selectEvent).ToList();

            if (show != null)
            {
                foreach (var item in show)
                {
                    String SelectOne = $"SELECT [Number],Step From Steps Inner JOIN [Procedure] On [Procedure].Id = {item.Id}";

                    item.Steps = connection.Query<Steps>(SelectOne).ToList();
                }

            }

            return show;
        }


    }

    public Procedure ProcedureStepsShow(int id)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            var show = new Procedure();
            connection.Open();
            String selectEvent = $"SELECT Id,[Title],[Description] FROM [Procedure] WHERE Id = {id}";
            show = connection.Query<Procedure>(selectEvent).FirstOrDefault();

            if (show != null)
            {
                String SelectOne = $"SELECT [Number],Step From Steps Inner JOIN [Procedure] On [Procedure].Id = {id}";

                show.Steps = connection.Query<Steps>(SelectOne).ToList();
            }

            return show;
        }
    }
    public void PostComment(Comment comment)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            connection.Open();



            var newComment = new Comment()
            {

                Content = comment.Content,
                ArticleId = comment.ArticleId


            };

            String insert = $"INSERT INTO [Comment]([Comment],ArticleId) VALUES(@Content,@ArticleId)";

            connection.Execute(insert, new
            {

                newComment.Content,

                newComment.ArticleId


            });
        }


    }
    public IEnumerable<Article> CommentForArticleShow()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            var show = new List<Article>();
            connection.Open();
            String selectEvent = $"SELECT [Title],[Content] FROM [Article]";
            show = connection.Query<Article>(selectEvent).ToList();

            if (show != null)
            {
                foreach (var item in show)
                {
                    String SelectOne = $"SELECT Comment [Content] From dbo.Comment LEFT JOIN Article On Article.Id  = {item.Id}";

                    item.Comment = connection.Query<Comment>(SelectOne).ToList();
                }

            }

            return show;
        }
    }
    public void CreateUser(User user)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {
            connection.Open();



            var newUser = new User()
            {

                UserName = user.UserName,



            };

            String insert = $"INSERT INTO [User]([Name]) VALUES(@UserName)";

            connection.Execute(insert, new
            {

                newUser.UserName,




            });
        }
    }
    public void UserWantToJoinEvent(EventParticipants join)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Application;Integrated Security=True"))
        {


            connection.Open();



            String find = $"Select UserId,EventId From EventParticipants WHERE UserId = @UserId AND EventId = @EventId";
            int _find = connection.Query<int>(find, new
            {
                join.UserId,
                join.EventId
            }).FirstOrDefault();

            var _comment = new EventParticipants()
            {
                
                UserId = join.UserId,
                EventId = join.EventId,

            };



            if (_find == 0)
            {

                String insert = $"INSERT INTO EventParticipants(UserId,EventId) VALUES(@UserId,@EventId)";



                connection.Execute(insert, new
                {
                    
                    _comment.UserId,
                    _comment.EventId,
                });
            }
        }
    }
}


