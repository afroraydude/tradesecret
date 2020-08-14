using UnityEngine;

namespace TradeSecret.Data
{
    public struct Vector
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Vector(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }
    }
}