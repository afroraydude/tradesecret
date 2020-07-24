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

        private RaycastHit _hit;

        private enum States
        {
            Idle = 0,
            Patrol = 1,
            Warn = 2,
            Pursue = 3
        }

        // For Unity Editor
        [SerializeField] private States debugCurrentState;

        // Original state
        [SerializeField] private States startState = States.Idle;

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
            debugCurrentState = (States)iStates.IndexOf(stateMachine.GetCurrentState());
            stateMachine.ExecuteStateUpdate();
            
            currentTime += Time.deltaTime;
            _hit = enemySight.globalRaycast;

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
                stateMachine.SwitchState(iStates[(int) States.Pursue]);
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

        /// <summary>
        /// Switches state and navmesh destination based on Raycasts
        /// </summary>
        /// <param name="isPlayer"></param>
        public void OnRaycastHit(bool isPlayer)
        {

            
            if (isPlayer)
            {
                Debug.Log(gameObject.name + ": " + stateMachine.GetCurrentState());
                if (!isChasing && stateMachine.GetCurrentState() != iStates[(int) States.Warn])
                {
                    stateMachine.SwitchState(iStates[(int)States.Warn]);
                }
                if (playerSeen == false) 
                    scanStartTime = currentTime;
                playerSeen = true;
                cooledDown = false;
                stateMachine.GetCurrentState().OnHit(_hit);
                Debug.Log(gameObject.name + ": " + stateMachine.GetCurrentState());
            }
            else
            {
                playerSeen = false;
                if (isChasing && stateMachine.GetCurrentState() != iStates[(int) States.Pursue])
                {
                    stateMachine.SwitchState(iStates[(int) States.Pursue]);
                    stateMachine.GetCurrentState().SetPlayer(player);
                    //stateMachine.GetCurrentState().OnHit(hit);
                }
            
                if (!isChasing)
                {
                    if (cooledDown  && stateMachine.GetCurrentState() != iStates[(int) startState])
                    {
                        stateMachine.SwitchState(iStates[(int) startState]);
                    }
                    if (!cooledDown && stateMachine.GetCurrentState() != iStates[(int) States.Warn])
                    {
                        stateMachine.SwitchState(iStates[(int)States.Warn]);
                    }
                }
                //stateMachine.SwitchToPreviousState();
            }
        }
    }
}
