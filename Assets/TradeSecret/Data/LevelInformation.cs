namespace TradeSecret.Data
{
    public struct LevelInformation
    {
        /*
         * setup for level information
         * use get; set; system for JSON
         * serialization/deserialization
         */
        public string title { get; set; }
        public string description { get; set; }
        public int sizeX { get; set; }
        public int sizeY { get; set; }
        
        /*
         * For use specifically in scripts
         */
        public LevelInformation(string title, string description, string sizeX, string sizeY)
        {
            this.title = title;
            this.description = description;
            this.sizeX = int.Parse(sizeX);
            this.sizeY = int.Parse(sizeY);
        }
    }
}