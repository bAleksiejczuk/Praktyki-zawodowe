using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;


namespace WebApi.Entities;



public class Repository
{

    public IEnumerable<ShowAll> ShowAll()
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            var show = new List<ShowAll>();
            connection.Open();
            String selectAll = $"SELECT Product.Name,Product.Price,Category.categoryName FROM Product INNER JOIN Category on  Product.CategoryId = Category.Id;";
            show = connection.Query<ShowAll>(selectAll).ToList();
            return show;
        }
    }



        public IEnumerable<Search> Search(Search search)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            var show = new List<Search>();
            connection.Open();
            var newSearch = new Search()
            {

                Name = search.Name + "%",
            };
            // trzeba przekazać Name jako parametr bo będzie sql injection (ZROBIONE)
            String searchBeer = $"Select Name,Price from Product WHERE Name Like @Name";
            show = connection.Query<Search>(searchBeer, newSearch).ToList();

            return show;



        }
    }
    public IEnumerable<SearchByPrice> SearchByPrice(int From, int To)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            var show = new List<SearchByPrice>();
            connection.Open();
            String searchFromTo = $"SELECT Name,Price FROM Product WHERE Price BETWEEN {From} AND {To}";
            //entery

            show = connection.Query<SearchByPrice>(searchFromTo).ToList();

            return show;
        }
    }

    public IEnumerable<SearchByCategory> SearchByCategory(string CategoryName)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            var show = new List<SearchByCategory>();
            connection.Open();
            //to samo co wyżej z parametrem
            String searchCategoryId = $"SELECT [Id] From Category WHERE categoryName = '{CategoryName}'";
            int querry = connection.Query<int>(searchCategoryId).FirstOrDefault();

            String searchCategoryName = $"SELECT [Name],[Price] From Product Where CategoryId={querry}";
            show = connection.Query<SearchByCategory>(searchCategoryName).ToList();

            return show;
        }
    }
    public void CreateOrder(Order order)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            connection.Open();

            var newOrder = new Order()
            {

                CustomerName = order.CustomerName,
                Amount = order.Amount,
                CustomerEmail = order.CustomerEmail,
                AlcoholName = order.AlcoholName,
                Cost = order.Cost
                //entery


            };


            bool emialVerifivation = order.CustomerEmail.Contains("@");

            if (emialVerifivation == true)
            {

                String searchPrice = "Select Price From Product where Name = @AlcoholName;";
                newOrder.Cost = connection.Query<float>(searchPrice, newOrder).FirstOrDefault();


                String insert = $"INSERT INTO [Order] (CustomerName, Cost, CustomerEmail,Amount,AlcoholName) VALUES (@CustomerName,0, @CustomerEmail,@Amount,@AlcoholName); " +
                    $"UPDATE [Order] SET Cost=Round(@Cost * Amount,2) Where CustomerEmail = @CustomerEmail and AlcoholName = @AlcoholName;";

                connection.Execute(insert, new
                {

                    newOrder.CustomerName,
                    newOrder.CustomerEmail,
                    newOrder.Amount,
                    newOrder.AlcoholName,
                    newOrder.Cost,


                });
            }

        }
    }
    public IEnumerable<CompleteOrder> CompleteOrder(string UserEmail)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            var show = new List<CompleteOrder>();
            connection.Open();


            String completeOrder = $"SELECT Cost, Amount, CustomerEmail, AlcoholName From [Order] Where CustomerEmail = '{UserEmail}'; SELECT Sum(Cost) From [Order] Where CustomerEmail = '@';";

            show = connection.Query<CompleteOrder>(completeOrder).ToList();

            return show;
        }
    }
    public void Register(Register register)
    {

        using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AlkoLegro;Integrated Security=True"))
        {
            connection.Open();

            string hashPassword(string password)
            {
                var sha = SHA256.Create();
                var asBytaArray = Encoding.Default.GetBytes(password);
                var hashedPassword = sha.ComputeHash(asBytaArray);
                return Convert.ToBase64String(hashedPassword);
            }


            string password = hashPassword(register.Password);
            register.Password = password;

            string repetepassword = hashPassword(register.RepetePassword);
            register.RepetePassword = repetepassword;


            if (register.Password == register.RepetePassword)
            {
                
                var chceckEmail = new List<Register>();

                String searchEmail = $"SELECT CustomerEmail From [User] Where CustomerEmail = @CustomerEmail;";
                String insert = $"INSERT INTO [User] (CustomerEmail,Password) VALUES (@CustomerEmail,@Password);";

                var newRegister = new Register()
                {

                    CustomerEmail = register.CustomerEmail,
                    Password = register.Password,
                    RepetePassword = register.RepetePassword,
                    //entery


                };

                chceckEmail = connection.Query<Register>(searchEmail, newRegister).ToList();
               if (chceckEmail.Count <1)
                {
                    connection.Execute(insert, new
                    {

                        newRegister.CustomerEmail,
                        newRegister.Password,
                        newRegister.RepetePassword,


                    });
                }
            }
        }
    }
}

