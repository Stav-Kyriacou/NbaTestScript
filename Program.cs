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
        public static float pointsAverage, reboundsAverage, assistsAverage, stealsAverage, blocksAverage, tppAverage, fgmAverage;
        public static float averagePlayerVariance = 0f;
        public static List<Player> AllPlayers = new List<Player>();
        public static List<Team> Teams = new List<Team>();
        static void Main(string[] args)
        {
            //Currently, this program compares each players Points, Rebounds, Assists, Steals and Blocks against the average of all players
            //It then sorts the players based on the calculated rating

            float[,] minMaxStats = new float[7, 2];
            minMaxStats[0, 0] = 100;
            minMaxStats[1, 0] = 100;
            minMaxStats[2, 0] = 100;
            minMaxStats[3, 0] = 100;
            minMaxStats[4, 0] = 100;
            minMaxStats[5, 0] = 100;
            minMaxStats[6, 0] = 100;

            //read player data from csv
            using (var reader = new StreamReader("player_data_2017_2018.csv"))
            {
                while (!reader.EndOfStream)
                {
                    //split each line of the csv at the ,
                    var line = reader.ReadLine();
                    var values = line.Split(",");

                    //store the desired stats from each line
                    var points = float.Parse(values[7]);
                    var rebounds = float.Parse(values[19]);
                    var assists = float.Parse(values[20]);
                    var steals = float.Parse(values[22]);
                    var blocks = float.Parse(values[23]);
                    var tpp = float.Parse(values[13]);
                    var fgm = float.Parse(values[8]);

                    //find the min and max of each stat
                    if (points < minMaxStats[0, 0]) minMaxStats[0, 0] = points;
                    if (points > minMaxStats[0, 1]) minMaxStats[0, 1] = points;
                    if (rebounds < minMaxStats[1, 0]) minMaxStats[1, 0] = rebounds;
                    if (rebounds > minMaxStats[1, 1]) minMaxStats[1, 1] = rebounds;
                    if (assists < minMaxStats[2, 0]) minMaxStats[2, 0] = assists;
                    if (assists > minMaxStats[2, 1]) minMaxStats[2, 1] = assists;
                    if (steals < minMaxStats[3, 0]) minMaxStats[3, 0] = steals;
                    if (steals > minMaxStats[3, 1]) minMaxStats[3, 1] = steals;
                    if (blocks < minMaxStats[4, 0]) minMaxStats[4, 0] = blocks;
                    if (blocks > minMaxStats[4, 1]) minMaxStats[4, 1] = blocks;
                    if (tpp < minMaxStats[5, 0]) minMaxStats[5, 0] = tpp;
                    if (tpp > minMaxStats[5, 1]) minMaxStats[5, 1] = tpp;
                    if (fgm < minMaxStats[6, 0]) minMaxStats[6, 0] = fgm;
                    if (fgm > minMaxStats[6, 1]) minMaxStats[6, 1] = fgm;

                    //add new player to list
                    AllPlayers.Add(new Player
                    {
                        Name = values[0],
                        Team = values[1],
                        Points = points,
                        Rebounds = rebounds,
                        Assists = assists,
                        Steals = steals,
                        Blocks = blocks,
                        TPP = tpp,
                        FGM = fgm
                    });
                }
            }


            //Print All Player Stats
            foreach (var p in AllPlayers)
            {
                // System.Console.WriteLine($"PTS: {p.Points.ToString("F2")}\tRBS: {p.Rebounds.ToString("F2")} \tAST: {p.Assists.ToString("F2")} \tSTL: {p.Steals.ToString("F2")} \tBLK: {p.Blocks.ToString("F2")} \tTPP: {p.TPP.ToString("F2")} \tFGM: {p.FGM.ToString("F2")} \t{p.Name}");
            }

            //Calculating Average Scores
            float pointsTotal = 0;
            float reboundsTotal = 0;
            float assistsTotal = 0;
            float stealsTotal = 0;
            float blocksTotal = 0;
            float tppTotal = 0;
            float fgmTotal = 0;

            //add up all the stats we want
            foreach (var p in AllPlayers)
            {
                pointsTotal += p.Points;
                reboundsTotal += p.Rebounds;
                assistsTotal += p.Assists;
                stealsTotal += p.Steals;
                blocksTotal += p.Blocks;
                tppTotal += p.TPP;
                fgmTotal += p.FGM;
            }

            //divide to get average
            pointsAverage = pointsTotal / AllPlayers.Count;
            reboundsAverage = reboundsTotal / AllPlayers.Count;
            assistsAverage = assistsTotal / AllPlayers.Count;
            stealsAverage = stealsTotal / AllPlayers.Count;
            blocksAverage = blocksTotal / AllPlayers.Count;
            tppAverage = tppTotal / AllPlayers.Count;
            fgmAverage = fgmTotal / AllPlayers.Count;


            //loop through all players
            //for every stats
            //divide player stat by max stat
            //assign the rating

            foreach (var p in AllPlayers)
            {
                p.StatRatings[0] = p.Points / minMaxStats[0, 1];
                p.StatRatings[1] = p.Rebounds / minMaxStats[1, 1];
                p.StatRatings[2] = p.Assists / minMaxStats[2, 1];
                p.StatRatings[3] = p.Steals / minMaxStats[3, 1];
                p.StatRatings[4] = p.Blocks / minMaxStats[4, 1];
                p.StatRatings[5] = p.TPP / minMaxStats[5, 1];
                p.StatRatings[6] = p.FGM / minMaxStats[6, 1];
            }

            AllPlayers = AllPlayers.OrderBy(x => x.TotalRating).ToList();

            foreach (var p in AllPlayers)
            {
                // System.Console.WriteLine($"Rating: {p.TotalRating.ToString("F2")}\t{p.Name}");
            }


            // float totalPlayerVariance = 0;

            // //compare player score to average
            // foreach (var p in AllPlayers)
            // {
            //     p.StatRatings[0] = p.Points - pointsAverage;
            //     p.StatRatings[1] = p.Rebounds - reboundsAverage;
            //     p.StatRatings[2] = p.Assists - assistsAverage;
            //     p.StatRatings[3] = p.Steals - stealsAverage;
            //     p.StatRatings[4] = p.Blocks - blocksAverage;

            //     foreach (var v in p.StatRatings)
            //     {
            //         //add up the differences between the player's stats and the average
            //         //to get how much better or worse than average they are
            //         // p.TotalRating += v;
            //     }
            //     totalPlayerVariance += p.TotalRating;
            // }
            // averagePlayerVariance = totalPlayerVariance / AllPlayers.Count;





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
            Teams = Teams.OrderBy(x => x.Top10PlayerRating).ToList();

            foreach (var t in Teams)
            {
                System.Console.WriteLine();
                t.PrintPlayers(10);
                System.Console.WriteLine($"Team Rating: {t.Top10PlayerRating}");
            }
            System.Console.WriteLine();


            //Print the predicted team ladder
            System.Console.WriteLine();
            System.Console.WriteLine("Predicted Ranking");
            Teams = Teams.OrderByDescending(x => x.Top10PlayerRating).ToList();
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
            // System.Console.WriteLine($"Total Players: {AllPlayers.Count}");
            // System.Console.WriteLine($"Teams: {Teams.Count}");


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