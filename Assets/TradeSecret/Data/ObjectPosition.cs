using UnityEngine;

namespace TradeSecret.Data
{
    public struct ObjectPosition
    {

        public Vector3 position;
        public LocalQuanternion rotation;
        public Vector3 scale;
        
        public ObjectPosition(float x, float y, float z)
        {
            this.position = new Vector3(x, y, z);
            this.rotation = new LocalQuanternion();
            this.scale = new Vector3(1, 1, 1);
        }

        public ObjectPosition(Vector3 position)
        {
            this.position = position;
            this.rotation = new LocalQuanternion();
            this.scale = new Vector3(1, 1, 1);
        }

        public ObjectPosition(Vector3 position, float x, float y, float z)
        {
            this.position = position;
            this.rotation = new LocalQuanternion();
            this.scale = new Vector3(1, 1, 1);
        }

        public ObjectPosition(float x, float y, float z, Quaternion rotation)
        {
            this.position = new Vector3(x, y, z);
            this.rotation = new LocalQuanternion(rotation);
            this.scale = new Vector3(1, 1, 1);
        }
        
        public ObjectPosition(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = new LocalQuanternion(rotation);
            this.scale = new Vector3(1, 1, 1);
        }

        public ObjectPosition(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = new LocalQuanternion(rotation);
            this.scale = scale;
        }
    }
}