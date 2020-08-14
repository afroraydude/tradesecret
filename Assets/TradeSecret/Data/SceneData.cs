namespace TradeSecret.Data
{
    public struct SceneData
    {
        public ObjectData objectData { get; set; }

        public SceneData(ObjectData data)
        {
            this.objectData = data;
        }
    }
}