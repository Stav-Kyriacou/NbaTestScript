namespace NbaTest
{
    class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
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
}