namespace TradeSecret.Data
{
    public struct LevelFile
    {
        public LevelInformation LevelInformation { get; set; }
        public SceneData sceneData { get; set; }

        public LevelFile(LevelInformation info, SceneData data)
        {
            this.LevelInformation = info;
            this.sceneData = data;
        }
    }
}