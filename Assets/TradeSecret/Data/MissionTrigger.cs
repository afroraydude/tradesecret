using System.Runtime.InteropServices;

namespace TradeSecret.Data
{
    public struct MissionTrigger
    {
        public enum Prefabs
        {
            Undefined = 0,
            Table = 1,
            Desk = 2,
            RefreshmentTable = 3
        }

        public enum Types
        {
            BasicInteractToFinish = 0
        }
        
        public Prefabs prefab { get; set; }
        public Types type { get; set; }
        public ObjectPosition transform { get; set; }
        public object data { get; set; }
        
        public MissionTrigger(int prefab, int objectType, ObjectPosition transform, object data)
        {
            this.prefab = (Prefabs)prefab;
            this.transform = transform;
            this.type = (Types)objectType;
            this.data = data;
        }
    }
}