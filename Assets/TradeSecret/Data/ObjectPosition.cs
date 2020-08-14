using UnityEngine;

namespace TradeSecret.Data
{
    public struct ObjectPosition
    {

        public Vector3 position;
        public LocalQuanternion rotation;
        
        public ObjectPosition(float x, float y, float z)
        {
            this.position = new Vector3(x, y, z);
            this.rotation = new LocalQuanternion();
        }

        public ObjectPosition(Vector3 position)
        {
            this.position = position;
            this.rotation = new LocalQuanternion();
        }

        public ObjectPosition(Vector3 position, float x, float y, float z)
        {
            this.position = position;
            this.rotation = new LocalQuanternion();
        }

        public ObjectPosition(float x, float y, float z, Quaternion rotation)
        {
            this.position = new Vector3(x, y, z);
            this.rotation = new LocalQuanternion(rotation);
        }
        
        public ObjectPosition(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = new LocalQuanternion(rotation);
        }
    }
}