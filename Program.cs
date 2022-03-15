using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NbaTest
{
    class Program
    {
        //extra stats to check
        //3 point %
        //field goals made
        public static float pointsAverage, reboundsAverage, assistsAverage, stealsAverage, blocksAverage;
        public static float averagePlayerVariance = 0f;
        public static List<Player> AllPlayers = new List<Player>();
        public static List<Team> Teams = new List<Team>();
        static void Main(string[] args)
        {
            //Currently, this program compares each players Points, Rebounds, Assists, Steals and Blocks against the average of all players
            //It then sorts the players based on the calculated rating

            //read player data from csv
            using (var reader = new StreamReader("player_data_2017_2018.csv"))
            {
                while (!reader.EndOfStream)
                {
                    //split each line of the csv at the ,
                    var line = reader.ReadLine();
                    var values = line.Split(",");

                    //add new player to list
                    AllPlayers.Add(new Player
                    {
                        Name = values[0],
                        Team = values[1],
                        Points = float.Parse(values[7]) * 10f,
                        Rebounds = float.Parse(values[19]) * 3f,
                        Assists = float.Parse(values[20]) * 5f,
                        Steals = float.Parse(values[22]) * 1f,
                        Blocks = float.Parse(values[23]) * 1f
                    });
                }
            }

            //Calculating Average Scores
            float pointsTotal = 0;
            float reboundsTotal = 0;
            float assistsTotal = 0;
            float stealsTotal = 0;
            float blocksTotal = 0;

            //add up all the stats we want
            foreach (var p in AllPlayers)
            {
                pointsTotal += p.Points;
                reboundsTotal += p.Rebounds;
                assistsTotal += p.Assists;
                stealsTotal += p.Steals;
                blocksTotal += p.Blocks;
            }

            //divide to get average
            pointsAverage = pointsTotal / AllPlayers.Count;
            reboundsAverage = reboundsTotal / AllPlayers.Count;
            assistsAverage = assistsTotal / AllPlayers.Count;
            stealsAverage = stealsTotal / AllPlayers.Count;
            blocksAverage = blocksTotal / AllPlayers.Count;

            float totalPlayerVariance = 0;

            //compare player score to average
            foreach (var p in AllPlayers)
            {
                p.variances[0] = p.Points - pointsAverage;
                p.variances[1] = p.Rebounds - reboundsAverage;
                p.variances[2] = p.Assists - assistsAverage;
                p.variances[3] = p.Steals - stealsAverage;
                p.variances[4] = p.Blocks - blocksAverage;

                foreach (var v in p.variances)
                {
                    //add up the differences between the player's stats and the average
                    //to get how much better or worse than average they are
                    p.totalVariance += v;
                }
                totalPlayerVariance += p.totalVariance;
            }
            averagePlayerVariance = totalPlayerVariance / AllPlayers.Count;


            //sort list by total variance
            AllPlayers = AllPlayers.OrderBy(x => x.totalVariance).ToList();

            //print all players and their variance
            // foreach (var p in AllPlayers)
            // {
            //     System.Console.WriteLine($"Variance: {p.totalVariance.ToString("F3")} --> Team: {p.Team} --> Name: {p.Name} ");
            // }

            AllPlayers = AllPlayers.OrderByDescending(x => x.totalVariance).ToList();
            float topVariances = 0f;
            for (int i = 0; i < 12; i++)
            {
                topVariances += AllPlayers[i].totalVariance;
            }

            
            //Place all players into their teams
            string currentTeamName;
            bool teamExists = false;
            Team currentTeam = new Team();

            for (int i = 0; i < AllPlayers.Count; i++)
            {
                currentTeamName = AllPlayers[i].Team;

                for (int j = 0; j < Teams.Count; j++)
                {
                    if (currentTeamName == Teams[j].NameShort)
                    {
                        teamExists = true;
                        currentTeam = Teams[j];
                    }
                }
                if (teamExists)
                {
                    currentTeam.AddPlayer(AllPlayers[i]);
                }
                else
                {
                    Team newTeam = new Team();
                    newTeam.NameShort = currentTeamName;
                    newTeam.Players.Add(AllPlayers[i]);
                    Teams.Add(newTeam);
                }

                teamExists = false;
            }

            // Print all teams and their players
            Teams = Teams.OrderBy(x => x.TotalTeamVariance).ToList();

            // foreach (var t in Teams)
            // {
            //     System.Console.WriteLine();
            //     t.PrintPlayers(10);
            //     System.Console.WriteLine($"Team Variance: {t.TotalTeamVariance}");
            // }
            System.Console.WriteLine();


            //Print the predicted team ladder
            System.Console.WriteLine();
            System.Console.WriteLine("Predicted Ranking");
            Teams = Teams.OrderByDescending(x => x.TotalTeamVariance).ToList();
            for (int i = 0; i < Teams.Count; i++)
            {
                System.Console.WriteLine($"{Teams[i].GetFullTeamName()}");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Actual Ranking");
            foreach (var t in Teams)
            {
                System.Console.WriteLine(t.GetActualRanking());
            }


            System.Console.WriteLine("------------General Stats--------------");
            System.Console.WriteLine($"AVERAGES  Points: {pointsAverage}   Rebounds: {reboundsAverage}   Assists: {assistsAverage}   Steals: {stealsAverage}   Blocks: {blocksAverage}");
            // System.Console.WriteLine($"Average player variance: {averagePlayerVariance.ToString("F8")}");
            // System.Console.WriteLine($"Top variances: {topVariances}");
            // System.Console.WriteLine($"Total Players: {AllPlayers.Count}");
            // System.Console.WriteLine($"Teams: {Teams.Count}");

            foreach (var p in AllPlayers)
            {
                System.Console.WriteLine($"Name: {p.Name}\t\t\tPoints: {p.Points}");
            }
        }
        public static Player GetPlayerByName(string name)
        {
            foreach (var p in AllPlayers)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }
    }
}