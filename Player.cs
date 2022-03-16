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
        private float PTSWeighting = 3f;
        private float RBWeighting = 10f;
        private float ASTWeighting = 0.5f;
        private float STLWeighting = 7.5f;
        private float BLKWeighting = 0.5f;
        private float TPPWeighting = 3f;
        private float FGMWeighting = 4f;

        public float[] StatRatings { get; set; }
        public float TotalRating
        {
            get
            {
                float total = 0;
                foreach (var rating in StatRatings)
                {
                    total += rating;
                }
                return total;
            }
        }
        public float TotalWeightedRating
        {
            get
            {
                float total = 0;
                total += StatRatings[0] * PTSWeighting;
                total += StatRatings[1] * RBWeighting;
                total += StatRatings[2] * ASTWeighting;
                total += StatRatings[3] * STLWeighting;
                total += StatRatings[4] * BLKWeighting;
                total += StatRatings[5] * TPPWeighting;
                total += StatRatings[6] * FGMWeighting;

                return total;
            }
        }
        public Player()
        {
            this.StatRatings = new float[7];
        }
    }
}