﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TradeSecret.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        // Load components
        public EnemySight enemySight;
        public EnemyPatrol enemyPatrol;
        public Animator enemyAnimator;
        public EnemyStateMachine stateMachine;
        public GameObject player;

        [SerializeField] private bool playerSeen = false;
        [SerializeField] private bool cooledDown = true;
        [FormerlySerializedAs("chaseBegun")] [SerializeField] private bool isChasing = false;

        
        [SerializeField] private float currentTime;
        
        [SerializeField] private float scanStartTime;
        [SerializeField] private float timeUntilChase = 2;
        
        // cooldown
        [SerializeField] private float cooldownStartTime;
        [SerializeField] private float timeUntilCooldown = 10;

        private RaycastHit hit;

        private enum states
        {
            idle = 0,
            patrol = 1,
            warn = 2,
            pursue = 3
        }

        // For Unity Editor
        [SerializeField] private states debugCurrentState;


        [SerializeField] private states startState = states.idle;

        List<EnemyState> iStates = new List<EnemyState>();

        void Awake()
        {
            enemySight = GetComponentInParent<EnemySight>();
            enemyPatrol = GetComponentInParent<EnemyPatrol>();
            enemyAnimator = GetComponentInParent<Animator>();
            stateMachine = GetComponentInParent<EnemyStateMachine>();

            iStates.AddRange(new EnemyState[] { new EnemyStateIdle(enemyAnimator, enemyPatrol), new EnemyStatePatrol(enemyAnimator, enemyPatrol), new EnemyStateWarn(enemyAnimator, enemyPatrol), new EnemyStatePursue(enemyAnimator, enemyPatrol, player) });

            stateMachine.SwitchState(iStates[(int) startState]);
            
        }

        void Start()
        {

        }

        void Update()
        {
            debugCurrentState = (states)iStates.IndexOf(stateMachine.currentEnemyState);
            stateMachine.ExecuteStateUpdate();
            
            currentTime += Time.deltaTime;
            hit = enemySight.globalRaycast;

            if (playerSeen)
            {
                cooldownStartTime = currentTime;
            }
            
            if (!playerSeen)
            {
                scanStartTime = currentTime;
            }

            if (playerSeen && (currentTime - scanStartTime) > timeUntilChase && !isChasing)
            {
                Debug.Log(gameObject.name + ": " + "Begin chase!");
                stateMachine.SwitchState(iStates[(int) states.pursue]);
                isChasing = true;
                cooledDown = false;
            } else if (!playerSeen && (currentTime - cooldownStartTime) > timeUntilCooldown && !cooledDown)
            {
                Debug.Log(gameObject.name + ": " + "Cooled down!");
                isChasing = false;
                cooledDown = true;
                stateMachine.SwitchState(iStates[(int) startState]);
            }
        }

        public void OnRaycastHit(bool isPlayer)
        {

            
            if (isPlayer)
            {
                Debug.Log(gameObject.name + ": " + stateMachine.currentEnemyState);
                if (!isChasing && stateMachine.currentEnemyState != iStates[(int) states.warn])
                {
                    stateMachine.SwitchState(iStates[(int)states.warn]);
                }
                if (playerSeen == false) 
                    scanStartTime = currentTime;
                playerSeen = true;
                cooledDown = false;
                stateMachine.currentEnemyState.OnHit(hit);
                Debug.Log(gameObject.name + ": " + stateMachine.currentEnemyState);
            }
            else
            {
                playerSeen = false;
                if (isChasing && stateMachine.currentEnemyState != iStates[(int) states.pursue])
                {
                    stateMachine.SwitchState(iStates[(int) states.pursue]);
                    stateMachine.currentEnemyState.SetPlayer(player);
                    //stateMachine.currentEnemyState.OnHit(hit);
                }
            
                if (!isChasing)
                {
                    if (cooledDown  && stateMachine.currentEnemyState != iStates[(int) startState])
                    {
                        stateMachine.SwitchState(iStates[(int) startState]);
                    }
                    if (!cooledDown && stateMachine.currentEnemyState != iStates[(int) states.warn])
                    {
                        stateMachine.SwitchState(iStates[(int)states.warn]);
                    }
                }
                //stateMachine.SwitchToPreviousState();
            }
        }
    }
}
