namespace NigTeam.Model;

public class TeamModel
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public int Contact { get; set; }

    public string? email { get; set; }
}
