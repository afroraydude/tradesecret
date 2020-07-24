using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace TradeSecret.Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {

        public bool patrolling = false;
        public bool autoBrake = true;
        public Transform[] patrolPoints;
        private int destPoint = 0;
        public NavMeshAgent agent;
        
        public float minRemainingDistance = 0.5f;

        private void Awake()
        {
            agent = GetComponentInParent<NavMeshAgent>();

            agent.autoBraking = autoBrake;
        }

        // Start is called before the first frame update
        void Start()
        {
            GoToNextPoint();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GoToNextPoint()
        {
            if (patrolPoints.Length == 0)
            {
                return;
            }
    
            agent.destination = patrolPoints[destPoint].position;

            destPoint = (destPoint + 1) % patrolPoints.Length;
        }
    }
}