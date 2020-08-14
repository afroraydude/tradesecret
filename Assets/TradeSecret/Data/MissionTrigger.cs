using System.Runtime.InteropServices;

namespace TradeSecret.Data
{
    public struct MissionTrigger
    {
        enum Prefabs
        {
            Undefined = 0,
            Table = 1,
            Desk = 2,
            RefreshmentTable = 3
        }

        enum Types
        {
            BasicInteractToFinish = 0
        }
        
        private Prefabs _prefab;
        private Types _type;
        private ObjectPosition _position;
        private object _data;

        public MissionTrigger(int prefab, int objectType, ObjectPosition position, object data)
        {
            this._prefab = (Prefabs)prefab;
            this._position = position;
            this._type = (Types)objectType;
            this._data = data;
        }
    }
}