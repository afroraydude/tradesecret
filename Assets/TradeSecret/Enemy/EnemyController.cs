using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeSecret.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public EnemySight enemySight;
        public EnemyPatrol enemyPatrol;

        public enum state
        {
            idle = 0,
            patrol = 1,
            warning = 2,
            chase = 3,
            warningFollow = 4
        }

        public state currentState = state.idle;

        void Awake()
        {
            enemySight = GetComponent<EnemySight>();
            enemyPatrol = GetComponent<EnemyPatrol>();
        }

        void Start()
        {

        }

        void Update()
        {
            if (currentState == state.patrol)
            {
                enemyPatrol.patrolling = true;
            }
        }
    }
}
