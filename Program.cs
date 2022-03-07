using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NbaTest
{
    class Program
    {
        public static float pointsAverage, reboundsAverage, assistsAverage, stealsAverage, blocksAverage;
        public static float averagePlayerVariance = 0f;
        public static List<Player> players = new List<Player>();
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
                    players.Add(new Player
                    {
                        Name = values[0],
                        Points = float.Parse(values[7]),
                        Rebounds = float.Parse(values[19]),
                        Assists = float.Parse(values[20]),
                        Steals = float.Parse(values[22]),
                        Blocks = float.Parse(values[23])
                    });
                }
            }

            //calculate average scores
            float pointsTotal = 0;
            float reboundsTotal = 0;
            float assistsTotal = 0;
            float stealsTotal = 0;
            float blocksTotal = 0;

            //add up all the stats we want
            foreach (var p in players)
            {
                pointsTotal += p.Points;
                reboundsTotal += p.Rebounds;
                assistsTotal += p.Assists;
                stealsTotal += p.Steals;
                blocksTotal += p.Blocks;

                // Console.WriteLine($"Name: {p.Name}  Points: {p.Points}   Rebounds: {p.Rebounds}   Assists: {p.Assists}    Steals: {p.Steals}    Blocks: {p.Blocks}");
            }

            //divide to get average
            pointsAverage = pointsTotal / players.Count;
            reboundsAverage = reboundsTotal / players.Count;
            assistsAverage = assistsTotal / players.Count;
            stealsAverage = stealsTotal / players.Count;
            blocksAverage = blocksTotal / players.Count;

            float totalPlayerVariance = 0;
            //compare player score to average
            foreach (var p in players)
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
            averagePlayerVariance = totalPlayerVariance / players.Count;


            //sort list by total variance
            players = players.OrderBy(x => x.totalVariance).ToList();

            //print all players and their variance
            foreach (var p in players)
            {
                System.Console.WriteLine($"Variance: {p.totalVariance.ToString("F3")} --> Name: {p.Name} ");
            }

            players = players.OrderByDescending(x => x.totalVariance).ToList();
            float topVariances = 0f;
            for (int i = 0; i < 13; i++)
            {
                topVariances += players[i].totalVariance;
            }

            //get the total variances for the 2021 winning team
            string[] team1Names = {
                "Kevin Durant",
                "Stephen Curry",
                "Klay Thompson",
                "Draymond Green",
                "Andre Iguodala",
                "Shaun Livingston",
                "David West",
                "Ian Clark",
                "Zaza Pachulia",
                "Patrick McCaw",
                "JaVale McGee",
                "James Michael McAdoo"
            };
            var team1 = new Team("Team 1", players);
            team1.SetupTeam(team1Names);

            string[] team2Names = {
                "LeBron James",
                "Kevin Love",
                "JR Smith",
                "George Hill",
                "Rodney Hood",
                "Tristan Thompson",
                "Larry Nance Jr.",
                "Jeff Green",
                "Jordan Clarkson",
                "Kyle Korver",
                "Ante Zizic",
                "Jose Calderón"
            };
            var team2 = new Team("Team 2", players);
            team2.SetupTeam(team2Names);


            System.Console.WriteLine("------------------------------------");
            System.Console.WriteLine($"AVERAGES  Points: {pointsAverage}   Rebounds: {reboundsAverage}   Assists: {assistsAverage}   Steals: {stealsAverage}   Blocks: {blocksAverage}");
            System.Console.WriteLine($"Average player variance: {averagePlayerVariance.ToString("F8")}");
            System.Console.WriteLine($"Top variances: {topVariances}");
            System.Console.WriteLine($"Winning Team Variance: {team1.TotalTeamVariance}");
            System.Console.WriteLine($"Winning Team Percentage: {(team1.TotalTeamVariance / topVariances) * 100}");
            System.Console.WriteLine($"Second Team Variance: {team2.TotalTeamVariance}");
            System.Console.WriteLine($"Second Team Percentage: {(team2.TotalTeamVariance / topVariances) * 100}");
            System.Console.WriteLine($"Total Players: {players.Count}");
            //find the average score of the winning teams from the past 5-10 years
            //compare the selected team to those averages
        }
        public static Player GetPlayerByName(string name)
        {
            foreach (var p in players)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }
    }
    class Player
    {
        public string Name { get; set; }
        public float Points { get; set; }
        public float Rebounds { get; set; }
        public float Assists { get; set; }
        public float Steals { get; set; }
        public float Blocks { get; set; }
        public float[] variances { get; set; }
        public float totalVariance { get; set; }
        public Player()
        {
            this.variances = new float[5];
        }
    }
    class Team
    {
        public List<Player> AllPlayers;
        public List<Player> Players;
        public string Name;
        public float TotalTeamVariance
        {
            get
            {
                float total = 0;
                foreach (var p in this.Players)
                {
                    total += p.totalVariance;
                }
                return total;
            }
        }
        public Team(string Name, List<Player> AllPlayers)
        {
            this.Players = new List<Player>();
            this.AllPlayers = AllPlayers;
            this.Name = Name;
        }
        public void SetupTeam(string[] playerNames)
        {
            for (int i = 0; i < playerNames.Length; i++)
            {
                AddPlayer(playerNames[i]);
            }
        }
        public void AddPlayer(string PlayerName)
        {
            foreach (var p in AllPlayers)
            {
                if (p.Name == PlayerName)
                {
                    this.Players.Add(p);
                }
            }
        }
        public void PrintTeam()
        {
            System.Console.WriteLine($"Printing {Name}'s Players");
            foreach (var p in this.Players)
            {
                System.Console.WriteLine($"Name: {p.Name}  Variance: {p.totalVariance}");
            }
        }
    }
}