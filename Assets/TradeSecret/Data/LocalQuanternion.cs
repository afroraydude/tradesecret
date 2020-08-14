using UnityEngine;

namespace TradeSecret.Data
{
    public struct LocalQuanternion
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }

        public LocalQuanternion(Quaternion orig)
        {
            this.x = orig.x;
            this.y = orig.y;
            this.z = orig.z;
            this.w = orig.w;
        }
    }
}