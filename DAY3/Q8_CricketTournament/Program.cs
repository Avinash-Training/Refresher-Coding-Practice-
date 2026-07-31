using System;
using System.Collections.Generic;
using System.Linq;

class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; } // Batsman, Bowler, All-rounder, WK
}

class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Player> Players { get; set; } = new List<Player>();
}

class Fixture
{
    public int Id { get; set; }
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public DateTime MatchDate { get; set; }
    public string Venue { get; set; }
}

class Tournament
{
    public string Name { get; set; }
    private List<Team> teams = new List<Team>();
    private List<Fixture> fixtures = new List<Fixture>();

    public void AddTeam(Team t)
    {
        teams.Add(t);
    }

    public void AddFixture(Fixture f)
    {
        fixtures.Add(f);
    }

    public int TotalTeams()
    {
        return teams.Count;
    }

    public List<Fixture> GetFixturesByTeam(int teamId)
    {
        return fixtures.Where(f => f.Team1.Id == teamId || f.Team2.Id == teamId).ToList();
    }

    public List<Player> GetPlayersByTeam(int teamId)
    {
        var team = teams.FirstOrDefault(t => t.Id == teamId);
        return team?.Players ?? new List<Player>();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Q8: Cricket Tournament ===\n");

        var team1 = new Team { Id = 1, Name = "Chennai Super Kings" };
        team1.Players.Add(new Player { Id = 1, Name = "MS Dhoni", Role = "WK" });
        team1.Players.Add(new Player { Id = 2, Name = "Ruturaj", Role = "Batsman" });
        team1.Players.Add(new Player { Id = 3, Name = "Jadeja", Role = "All-rounder" });
        team1.Players.Add(new Player { Id = 4, Name = "Deepak Chahar", Role = "Bowler" });

        var team2 = new Team { Id = 2, Name = "Mumbai Indians" };
        team2.Players.Add(new Player { Id = 5, Name = "Rohit Sharma", Role = "Batsman" });
        team2.Players.Add(new Player { Id = 6, Name = "Bumrah", Role = "Bowler" });
        team2.Players.Add(new Player { Id = 7, Name = "Pollard", Role = "All-rounder" });
        team2.Players.Add(new Player { Id = 8, Name = "Ishan Kishan", Role = "WK" });

        var team3 = new Team { Id = 3, Name = "Royal Challengers" };
        team3.Players.Add(new Player { Id = 9, Name = "Virat Kohli", Role = "Batsman" });
        team3.Players.Add(new Player { Id = 10, Name = "Siraj", Role = "Bowler" });
        team3.Players.Add(new Player { Id = 11, Name = "Maxwell", Role = "All-rounder" });
        team3.Players.Add(new Player { Id = 12, Name = "Dinesh Karthik", Role = "WK" });

        var tournament = new Tournament { Name = "IPL 2026" };
        tournament.AddTeam(team1);
        tournament.AddTeam(team2);
        tournament.AddTeam(team3);

        tournament.AddFixture(new Fixture { Id = 1, Team1 = team1, Team2 = team2, MatchDate = new DateTime(2026, 8, 5), Venue = "Wankhede" });
        tournament.AddFixture(new Fixture { Id = 2, Team1 = team2, Team2 = team3, MatchDate = new DateTime(2026, 8, 7), Venue = "Chinnaswamy" });
        tournament.AddFixture(new Fixture { Id = 3, Team1 = team1, Team2 = team3, MatchDate = new DateTime(2026, 8, 9), Venue = "Chepauk" });

        Console.WriteLine($"Total teams in tournament: {tournament.TotalTeams()}\n");

        Console.WriteLine($"Fixtures for '{team1.Name}':");
        foreach (var f in tournament.GetFixturesByTeam(team1.Id))
            Console.WriteLine($"  {f.Team1.Name} vs {f.Team2.Name} | {f.MatchDate:dd-MMM-yyyy} | {f.Venue}");

        Console.WriteLine($"\nPlayers in '{team2.Name}':");
        foreach (var p in tournament.GetPlayersByTeam(team2.Id))
            Console.WriteLine($"  {p.Name} | {p.Role}");
    }
}
