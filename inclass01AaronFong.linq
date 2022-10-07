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