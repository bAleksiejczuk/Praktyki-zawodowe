namespace WebApi.Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Date { get; set; }
    public int Like { get; set; }
}
public class Update
{
    public string Title { get; set; }
    public string Content { get; set; }
}

public class Create
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Date { get; set; }

}

