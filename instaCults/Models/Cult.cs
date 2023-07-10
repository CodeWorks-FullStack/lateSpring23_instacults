namespace instaCults.Models;

public class Cult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tags { get; set; }
    public string Description { get; set; }
    public int Popularity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string LeaderId { get; set; }
    public Profile Leader { get; set; }

}