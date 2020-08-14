using UnityEngine.Assertions.Must;

namespace TradeSecret.Data
{
    public struct InteractableObject
    {
        enum Types
        {
            Undefined = 0,
            Table = 1,
            Desk = 2,
            RefreshmentTable = 3
        }

        private Types _objectType;
        private ObjectPosition _position;

        public InteractableObject(int objectType, ObjectPosition position)
        {
            this._position = position;
            this._objectType = (Types)objectType;
        }
    }
}