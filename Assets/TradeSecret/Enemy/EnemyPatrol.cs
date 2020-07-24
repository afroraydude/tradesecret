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
        public int destPoint = 0;
        public int currentPoint = 0;

        public NavMeshAgent agent;
        
        public Animator enemyAnimator;
        
        public float minRemainingDistance = 0.5f;

        private void Awake()
        {
            agent = GetComponentInParent<NavMeshAgent>();

            agent.autoBraking = autoBrake;

            enemyAnimator = GetComponentInParent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //GoToNextPoint();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GoToNextPoint()
        {
            patrolling = false;
            if (agent.remainingDistance < minRemainingDistance)
                currentPoint = destPoint;
            destPoint = (destPoint + 1) % patrolPoints.Length;
            
            if (patrolPoints.Length == 0)
            {
                return;
            }

            if (patrolPoints[currentPoint].CompareTag("PatrolPoint_Look") && !enemyAnimator.GetBool("isWalking"))
            {
                StartCoroutine(LookThenGo());
            }
            
            else
            {
                patrolling = true;
                agent.destination = patrolPoints[destPoint].position;
            }
        }

        public IEnumerator LookThenGo()
        {
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("isLooking", true);
            yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length+enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            enemyAnimator.SetBool("isWalking", true);
            enemyAnimator.SetBool("isLooking", false);
            patrolling = true;
            agent.destination = patrolPoints[destPoint].position;
        }
    }
}