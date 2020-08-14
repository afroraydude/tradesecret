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

        public Types objectType { get; set; }
        public ObjectPosition transfrom { get; set; }

        public InteractableObject(int objectType, ObjectPosition transfrom)
        {
            this.transfrom = transfrom;
            this.objectType = (Types)objectType;
        }
    }
}