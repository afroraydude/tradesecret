using UnityEngine;
using UnityEngine.AI;

namespace TradeSecret.Data
{
    public struct Enemy
    {
        private enum States
        {
            Idle = 0,
            Patrol = 1,
            Warn = 2,
            Pursue = 3
        }

        private States _startState;
        private ObjectPosition[] _patrolPoints;
        private ObjectPosition _position;
        private string _name;

        public Enemy(string name, ObjectPosition objectPosition, int state, ObjectPosition[] patrolPoints)
        {
            this._patrolPoints = patrolPoints;
            this._startState = (States)state;
            this._position = objectPosition;
            this._name = name;
        }
    }
}