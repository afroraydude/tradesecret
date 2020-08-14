using UnityEngine;

namespace TradeSecret.Data
{
    public struct ObjectPosition
    {
        private float x;
        private float y;
        private float z;

        public ObjectPosition(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public ObjectPosition(Vector3 position)
        {
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
        }
    }
}