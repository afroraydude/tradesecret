using UnityEngine.Assertions.Must;

namespace TradeSecret.Data
{
    public struct InteractableObject
    {
        public enum Types
        {
            Undefined = 0,
            Table = 1,
            Desk = 2,
            RefreshmentTable = 3
        }

        public Types _objectType { get; set; }
        public ObjectPosition _position { get; set; }

        public InteractableObject(int objectType, ObjectPosition position)
        {
            this._position = position;
            this._objectType = (Types)objectType;
        }
    }
}