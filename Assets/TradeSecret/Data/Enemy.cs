using UnityEngine;
using UnityEngine.AI;

namespace TradeSecret.Data
{
    public struct Enemy
    {
        public enum States
        {
            Idle = 0,
            Patrol = 1,
            Warn = 2,
            Pursue = 3
        }

        public States _startState { get; set; }
        public ObjectPosition[] _patrolPoints { get; set; }
        public ObjectPosition _position { get; set; }
        string _name { get; set; }

        public Enemy(string name, ObjectPosition objectPosition, int state, ObjectPosition[] patrolPoints)
        {
            this._patrolPoints = patrolPoints;
            this._startState = (States)state;
            this._position = objectPosition;
            this._name = name;
        }
    }
}