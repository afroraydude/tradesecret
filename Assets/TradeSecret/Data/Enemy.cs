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

        public States startState { get; set; }
        public ObjectPosition[] patrolPoints { get; set; }
        public ObjectPosition transform { get; set; }
        string name { get; set; }

        public Enemy(string name, ObjectPosition objectPosition, int state, ObjectPosition[] patrolPoints)
        {
            this.patrolPoints = patrolPoints;
            this.startState = (States)state;
            this.transform = objectPosition;
            this.name = name;
        }
    }
}