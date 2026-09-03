namespace WebApiWithDb;

public class PlayerLeaderboardDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int HighestScore { get; set; }
}