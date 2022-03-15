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
        public float TPP { get; set; } // 3 Point %
        public float FGM { get; set; } // Field Goals Made

        //Weigted Stats
        public float WeightedPoints
        {
            get
            {
                return this.Points * this.PTSWeighting;
            }
        }
        public float WeightedRebounds
         {
            get
            {
                return this.Rebounds * this.RBWeighting;
            }
        }
        public float WeightedAssists
         {
            get
            {
                return this.Assists * this.ASTWeighting;
            }
        }
        public float WeightedSteals
         {
            get
            {
                return this.Steals * this.STLWeighting;
            }
        }
        public float WeightedBlocks
         {
            get
            {
                return this.Blocks * this.BLKWeighting;
            }
        }
        public float WeightedTPP
         {
            get
            {
                return this.TPP * this.TPPWeighting;
            }
        }
        public float WeightedFGM
         {
            get
            {
                return this.FGM * this.FGMWeighting;
            }
        }

        //Weightings
        private float PTSWeighting = 1f;
        private float RBWeighting = 1f;
        private float ASTWeighting = 1f;
        private float STLWeighting = 1f;
        private float BLKWeighting = 1f;
        private float TPPWeighting = 1f;
        private float FGMWeighting = 1f;

        public float[] variances { get; set; }
        public float totalVariance { get; set; }
        public Player()
        {
            this.variances = new float[5];
        }
    }
}