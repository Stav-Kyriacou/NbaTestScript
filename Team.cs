using System.Collections.Generic;

namespace NbaTest
{
    class Team
    {
        public string Name;
        public List<Player> Players;
        public int Size { get { return Players.Count; } }
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
        public Team()
        {
            this.Players = new List<Player>();
        }
        public void AddPlayer(string PlayerName)
        {
            foreach (var p in Players)
            {
                if (p.Name == PlayerName)
                {
                    this.Players.Add(p);
                }
            }
        }
        public void SetupTeam(string[] playerNames)
        {
            for (int i = 0; i < playerNames.Length; i++)
            {
                AddPlayer(playerNames[i]);
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