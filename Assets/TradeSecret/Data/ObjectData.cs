namespace TradeSecret.Data
{
    public struct ObjectData
    {
        public ObjectPosition[] walls { get; set; }
        public Enemy[] enemies { get; set; }
        public InteractableObject[] interactableObjects { get; set; }
        public MissionTrigger[] missionTriggers { get; set; }
        public ObjectPosition playerPosition { get; set; }

        public ObjectData(ObjectPosition[] walls, Enemy[] enemies, InteractableObject[] interactableObjects,
            MissionTrigger[] missionTriggers, ObjectPosition playerPosition)
        {
            this.walls = walls;
            this.enemies = enemies;
            this.interactableObjects = interactableObjects;
            this.missionTriggers = missionTriggers;
            this.playerPosition = playerPosition;
        }
    }
}