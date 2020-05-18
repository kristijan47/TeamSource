using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TeamSource.Enteties;
using TeamSource.Helpers;

namespace GameCenter
{
    class Program
    {
        static void Main(string[] args)
        {
            var teams = TeamsDataBase.GetAllTeams();

            // Find all TEAMS with names starting with LA
            var teamsStartingWithLA = teams.Where(team => team.Name.StartsWith("LA")).ToList();
            // teamsStartingWithLA.ForEach(team => Console.WriteLine(team.Name));


            // Find all team NAMES which are playing in "Staples Center"
            var teamsPlayingInStaplesCenter = teams.Where(team => team.Arena.Equals("Staples Center"))
                                                      .Select(team => team.Name).ToList();
            // teamsPlayingInStaplesCenter.ForEach(team => Console.WriteLine(team));



            // Find all teams coaches
            var allCoaches = teams.Select(team => team.Coach).ToList();
            // allCoaches.ForEach(coach => Console.WriteLine(coach.FullName));



            // Find 3 oldest coaches NAMES
            var oldest3CoahcesNames = allCoaches.OrderByDescending(coach => coach.Age)
                                                          .Take(3)
                                                            .Select(coach => coach.FullName)
                                                              .ToList();
            // oldest3CoahcesNames.ForEach(trainerName => Console.WriteLine(trainerName));



            // Group all teams by their arenas
            var groupedTeamsByArenas = teams.GroupBy(team => team.Arena).ToList();
            //foreach (var group in groupedTeamsByArenas)
            //{
            //    Console.WriteLine($"{group.Key}");
            //    foreach (var team in group)
            //    {
            //        Console.WriteLine($"-------------{team.Name}");
            //    }
            //}


            // Find all players in one LIST
            var allPlayers = new List<Player>();
            teams.ForEach(team => allPlayers.AddRange(team.Players));
            //allPlayers.ForEach(player => Console.WriteLine(player.FullName));

            //Find player with best avgPtsPerGame
            var playerWithMostPtsPerGame = allPlayers.OrderByDescending(player => player.PlayerStatistic["PtsPerGame"])
                                                          .FirstOrDefault();
            //Console.WriteLine(playerWithMostPtsPerGame.FullName);


            // HOMEWORK

            // Find all coaches NAMES with Age > 50
            var coachesOverFifty = teams.
                Where(team => team.Coach.Age > 50).ToList();
            //coachesOverFifty.ForEach(coach => Console.WriteLine(coach.Coach.FullName));

            // Order players by AGE - DESC

            var playersSortedByAge = allPlayers.
                OrderByDescending(player => player.Age).
                ToList();
            //foreach (var player in playersSortedByAge)
            //{
            //    Console.WriteLine($"{player.FullName} : {player.Age}");
            //}
            // Find player with highest RebPerGame

            var playerWithMostReboundsPerGame = allPlayers.OrderByDescending(player => player.PlayerStatistic["RebPerGame"])
                .FirstOrDefault();

            //Console.WriteLine(playerWithMostReboundsPerGame.FullName);
            // Find all players with PtsPerGame > 20
            var playersWithMoreThanTwentyPPG = allPlayers.
               FindAll(player => player.PlayerStatistic["PtsPerGame"] > 20).
               Select(player => player);
            //foreach (var player in playersWithMoreThanTwentyPPG)
            //{
            //    Console.WriteLine($"{player.FullName} : {player.PlayerStatistic["PtsPerGame"] }");
            //}
            // Find all players NAMES older then 30 years

            var allPlayersNamesOlderThirty = allPlayers.
                Where(player => player.Age > 30).
                Select(player => player.FullName).
                ToList();

            //allPlayersNamesOlderThirty.ForEach(player => Console.WriteLine(player));
            // Group players by age

            var groupPlayersAge = allPlayers.
                GroupBy(player => player.Age).
                ToList();
            //foreach (var player in groupPlayersAge)
            //{
            //    Console.WriteLine(player.Key);
            //}

            // Find All players NAMES and PtsPerGame if have RebPerGame > 7.0

            var playerNamesWithSevenReboundsOrMore = allPlayers.
                Where(player => player.PlayerStatistic["RebPerGame"] >= 7.0).
                Select(player => new
                {
                    Name = player.FullName,
                    Points = player.PlayerStatistic["PtsPerGame"],
                    Rebounds = player.PlayerStatistic["RebPerGame"]
                })
               ;

            //foreach (var player in playerNamesWithSevenReboundsOrMore)
            //{
            //    Console.WriteLine(player);
            //}


            // Find first 3 players with highest PtsPerGame

            var firstThreeScorers = allPlayers.
                OrderByDescending(player => player.PlayerStatistic["PtsPerGame"]).Take(3).ToList();
            //firstThreeScorers.ForEach(player => Console.WriteLine
            //($"{player.FullName}: {player.PlayerStatistic["PtsPerGame"]}"));

            // Find the team which has the player with highest PtsPerGame

            var teamHighestScorer = teams.SingleOrDefault(team => team.Players.Contains(playerWithMostPtsPerGame));
            //Console.WriteLine(teamHighestScorer.Name);

            // Find first 4 players with highest RebPerGame and order them by PtsPerGame - ASC

            var topFourRebounders = allPlayers.
                OrderByDescending(player => player.PlayerStatistic["RebPerGame"]).
                Take(4)
                .ToList().
                OrderBy(player => player.PlayerStatistic["PtsPerGame"]);

            foreach (var player in topFourRebounders)
            {
                Console.WriteLine($"{player.FullName}, Points: {player.PlayerStatistic["PtsPerGame"]} ,Rebounds: {player.PlayerStatistic["RebPerGame"]} ");
            }
        }
    }
}