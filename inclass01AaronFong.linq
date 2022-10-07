<Query Kind="Statements">
  <Connection>
    <ID>a5232901-1c0e-4d48-9714-082823435329</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.\sqlexpress</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>FSIS_2018</Database>
  </Connection>
</Query>

// q1
Guardians
	.Where(x => x.Players.Count() > 1)
	.Select(x => new {
		Name = x.FirstName + " " + x.LastName,
		Children = x.Players.Select(x => new {
			Name = x.FirstName,
			Age = x.Age,
			Gender = x.Gender,
			Team = x.Team.TeamName
		})
	})
	.OrderByDescending(x => x.Children.Count())
	.Dump();

//q2
Players
	.GroupBy(x => x.Gender)
	.Select(x => new {
		Gender = x.Key == "F" ? "Female" : "Male",
		Count = x.Key.Count()
	})
	.Dump();
	
//q3
Teams
	.OrderBy(x => x.TeamName)
	.Select(x => new {
		Team = x.TeamName,
		Coach = x.Coach,
		Players = x.Players
			.OrderBy(x => x.LastName)
			.ThenBy(x => x.FirstName)
			.Select(x => new {
			LastName = x.LastName,
			FirstName = x.FirstName,
			Gender = x.Gender == "F" ? "Female" : "Male",
			Age = x.Age
		})
	})
	.Dump();
	
//q4
var wins = Teams.Select(x => x);

Teams
	.Select(x => new {
		TeamName = x.TeamName,
		Wins = x.Wins
	})
	.Where(x => x.Wins == wins.Max(x => x.Wins))
	.Dump();
	
//q5
PlayerStats
	.Select(x => new {
		name = x.Player.FirstName + " " + x.Player.LastName,
		teamname = x.Player.Team.TeamName,
		goals = x.Goals,
		assists = x.Assists,
		redccards = x.RedCard == true ? 1 : 0,
		yellowcards = x.YellowCard == true ? 1: 0
	})
	.OrderBy(x => x.name)
	.Dump();