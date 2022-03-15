using System.Collections.Generic;

namespace NbaTest
{
    class Team
    {
        public string NameShort;
        public List<Player> Players;
        public int Size { get { return Players.Count; } }
        public float TotalTeamRating
        {
            get
            {
                float total = 0;
                foreach (var p in this.Players)
                {
                    total += p.TotalWeightedRating;
                }
                return total;
            }
        }
        public float Top10PlayerRating
        {
            get
            {
                float total = 0;
                for (int i = 0; i < 10; i++)
                {
                    total += this.Players[i].TotalWeightedRating;
                }
                return total;
            }
        }
        public Team()
        {
            this.Players = new List<Player>();
        }
        public void AddPlayer(Player playerToAdd)
        {
            this.Players.Add(playerToAdd);
        }
        public void PrintEntireTeam()
        {
            System.Console.WriteLine($"{this.GetFullTeamName()} Entire Team");
            System.Console.WriteLine($"Player Count: {Size}");
            foreach (var p in this.Players)
            {
                System.Console.WriteLine($"Rating: {p.TotalRating.ToString("F2")}\t\tName: {p.Name} ");
            }
        }
        public void PrintPlayers(int playersToPrint)
        {
            if (playersToPrint > this.Players.Count)
            {
                PrintEntireTeam();
                return;
            }

            System.Console.WriteLine($"{this.GetFullTeamName()} Top {playersToPrint} Players");
            for (int i = 0; i < playersToPrint; i++)
            {
                System.Console.WriteLine($"Rating: {this.Players[i].TotalRating.ToString("F2")}\t\tName: {this.Players[i].Name} ");
            }
        }
        public string GetFullTeamName()
        {
            string fullName = "";
            switch (this.NameShort)
            {
                case "LAC":
                    fullName = "Los Angeles Clippers";
                    break;
                case "PHX":
                    fullName = "Phoenix Suns";
                    break;
                case "WAS":
                    fullName = "Washington Wizards";
                    break;
                case "GSW":
                    fullName = "Golden State Warriors";
                    break;
                case "NOP":
                    fullName = "New Orleans Pelicans";
                    break;
                case "MEM":
                    fullName = "Memphis Grizzlies";
                    break;
                case "MIA":
                    fullName = "Miami Heat";
                    break;
                case "BKN":
                    fullName = "Brooklyn Nets";
                    break;
                case "DEN":
                    fullName = "Denver Nuggets";
                    break;
                case "MIN":
                    fullName = "Minnesota Timberwolves";
                    break;
                case "PHI":
                    fullName = "Philadelphia 76ers";
                    break;
                case "NYK":
                    fullName = "New York Knicks";
                    break;
                case "HOU":
                    fullName = "Houston Rockets";
                    break;
                case "CLE":
                    fullName = "Cleveland Cavaliers";
                    break;
                case "SAC":
                    fullName = "Sacremento Kings";
                    break;
                case "POR":
                    fullName = "Portland TrailBlazers";
                    break;
                case "SAS":
                    fullName = "San Antonio Spurs";
                    break;
                case "ORL":
                    fullName = "Orlando Magic";
                    break;
                case "CHI":
                    fullName = "Chicago Bulls";
                    break;
                case "TOR":
                    fullName = "Toronto Raptors";
                    break;
                case "CHA":
                    fullName = "Charlotte Hornets";
                    break;
                case "MIL":
                    fullName = "Milwaukee Bucks";
                    break;
                case "BOS":
                    fullName = "Boston Celtics";
                    break;
                case "DET":
                    fullName = "Detroit Pistons";
                    break;
                case "LAL":
                    fullName = "Los Angeles Lakers";
                    break;
                case "OKC":
                    fullName = "Oklahoma City Thunder";
                    break;
                case "ATL":
                    fullName = "Atlanta Hawks";
                    break;
                case "DAL":
                    fullName = "Dallas Mavericks";
                    break;
                case "IND":
                    fullName = "Indiana Pacers";
                    break;
                case "UTA":
                    fullName = "Utah Jazz";
                    break;
                default:
                    break;
            }
            return fullName;
        }
        public int GetActualRanking()
        {
            int ranking = 0;
            switch (this.GetFullTeamName())
            {
                case "Houston Rockets":
                    ranking = 1;
                    break;
                case "Toronto Raptors":
                    ranking = 2;
                    break;
                case "Golden State Warriors":
                    ranking = 3;
                    break;
                case "Boston Celtics":
                    ranking = 4;
                    break;
                case "Philadelphia 76ers":
                    ranking = 5;
                    break;
                case "Cleveland Cavaliers":
                    ranking = 6;
                    break;
                case "Portland TrailBlazers":
                    ranking = 7;
                    break;
                case "Indiana Pacers":
                    ranking = 8;
                    break;
                case "New Orleans Pelicans":
                    ranking = 9;
                    break;
                case "Oklahoma City Thunder":
                    ranking = 10;
                    break;
                case "Utah Jazz":
                    ranking = 11;
                    break;
                case "Minnesota Timberwolves":
                    ranking = 12;
                    break;
                case "San Antonio Spurs":
                    ranking = 13;
                    break;
                case "Denver Nuggets":
                    ranking = 14;
                    break;
                case "Miami Heat":
                    ranking = 15;
                    break;
                case "Milwaukee Bucks":
                    ranking = 16;
                    break;
                case "Washington Wizards":
                    ranking = 17;
                    break;
                case "Los Angeles Clippers":
                    ranking = 18;
                    break;
                case "Detroit Pistons":
                    ranking = 19;
                    break;
                case "Charlotte Hornets":
                    ranking = 20;
                    break;
                case "Los Angeles Lakers":
                    ranking = 21;
                    break;
                case "New York Knicks":
                    ranking = 22;
                    break;
                case "Brooklyn Nets":
                    ranking = 23;
                    break;
                case "Chicago Bulls":
                    ranking = 24;
                    break;
                case "Sacremento Kings":
                    ranking = 25;
                    break;
                case "Orlando Magic":
                    ranking = 26;
                    break;
                case "Atlanta Hawks":
                    ranking = 27;
                    break;
                case "Dallas Mavericks":
                    ranking = 28;
                    break;
                case "Memphis Grizzlies":
                    ranking = 29;
                    break;
                case "Phoenix Suns":
                    ranking = 30;
                    break;
                default:
                    break;
            }
            return ranking;
        }
    }
}