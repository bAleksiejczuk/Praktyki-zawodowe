
public class Event
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Place { get; set; }


    public List<User> UserName { get; set; }   

}