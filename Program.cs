using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NbaTest
{
    class Program
    {
        public static List<Player> AllPlayers = new List<Player>();
        public static List<Team> Teams = new List<Team>();
        static void Main(string[] args)
        {
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

            //Scale every stat from a player between 0-1
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

            //Sort players in each team by their weighted ratings
            foreach (var t in Teams)
            {
                t.Players = t.Players.OrderByDescending(x => x.TotalWeightedRating).ToList();
            }

            //Print Teams in their rating order
            Teams = Teams.OrderBy(x => x.TeamRating).ToList();
            foreach (var t in Teams)
            {
                System.Console.WriteLine();
                t.PrintPlayers(5);
                System.Console.WriteLine($"Team Rating: {t.TeamRating}");
            }

            //Print the teams 
            System.Console.WriteLine();
            System.Console.WriteLine("Actual Ranking");
            Teams = Teams.OrderByDescending(x => x.TeamRating).ToList();
            foreach (var t in Teams)
            {
                System.Console.WriteLine(t.GetActualRanking());
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