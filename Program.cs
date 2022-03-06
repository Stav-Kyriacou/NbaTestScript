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
                p.variance[0] = p.Points - pointsAverage;
                p.variance[1] = p.Rebounds - reboundsAverage;
                p.variance[2] = p.Assists - assistsAverage;
                p.variance[3] = p.Steals - stealsAverage;
                p.variance[4] = p.Blocks - blocksAverage;

                foreach (var v in p.variance)
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
            var p1 = GetPlayerByName("Kevin Durant");
            var p2 = GetPlayerByName("Stephen Curry");
            var p3 = GetPlayerByName("Klay Thompson");
            var p4 = GetPlayerByName("Draymond Green");
            var p5 = GetPlayerByName("Andre Iguodala");
            var p6 = GetPlayerByName("Shaun Livingston");
            var p7 = GetPlayerByName("David West");
            var p8 = GetPlayerByName("Ian Clark");
            var p9 = GetPlayerByName("Zaza Pachulia");
            var p10 = GetPlayerByName("Patrick McCaw");
            var p11 = GetPlayerByName("JaVale McGee");
            var p12 = GetPlayerByName("James Michael McAdoo");

            float winningTeamVariance = p1.totalVariance + p2.totalVariance + p3.totalVariance + p4.totalVariance + p5.totalVariance + p6.totalVariance + p7.totalVariance + p8.totalVariance + p10.totalVariance + p9.totalVariance + p11.totalVariance + p12.totalVariance;




            System.Console.WriteLine("------------------------------------");
            System.Console.WriteLine($"AVERAGES  Points: {pointsAverage}   Rebounds: {reboundsAverage}   Assists: {assistsAverage}   Steals: {stealsAverage}   Blocks: {blocksAverage}");
            System.Console.WriteLine($"Average player variance: {averagePlayerVariance.ToString("F8")}");
            System.Console.WriteLine($"Top variances: {topVariances}");
            System.Console.WriteLine($"Winning Team Variance: {winningTeamVariance}");
            System.Console.WriteLine($"Winning Team Percentage: {(winningTeamVariance / topVariances) * 100}");
            System.Console.WriteLine($"Total Players: {players.Count}");
        }
        static Player GetPlayerByName(string name)
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
        public float[] variance { get; set; }
        public float totalVariance { get; set; }
        public Player()
        {
            this.variance = new float[5];
        }
    }
}