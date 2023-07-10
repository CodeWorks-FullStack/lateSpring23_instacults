namespace instaCults.Models;

public class CultMember
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CultId { get; set; }
    public string AccountId { get; set; }
}