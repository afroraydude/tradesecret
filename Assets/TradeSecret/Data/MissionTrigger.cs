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
        
        public Prefabs _prefab { get; set; }
        public Types _type { get; set; }
        public ObjectPosition _position { get; set; }
        public object _data { get; set; }

        public MissionTrigger(int prefab, int objectType, ObjectPosition position, object data)
        {
            this._prefab = (Prefabs)prefab;
            this._position = position;
            this._type = (Types)objectType;
            this._data = data;
        }
    }
}