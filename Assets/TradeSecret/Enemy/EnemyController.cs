using System.Collections;
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

        [SerializeField] private bool playerSeen;
        [SerializeField] private bool cooledDown = true;
        [FormerlySerializedAs("chaseBegun")] [SerializeField] private bool isChasing = false;
        [SerializeField] private bool chaseTimerRunning;
        [SerializeField] private bool cooldownTimerRunning;

        
        [SerializeField] private float currentTime;
        
        [FormerlySerializedAs("scanStartTime")] [SerializeField] private float chaseStartTime;
        [SerializeField] private float timeUntilChase = 2;
        
        // cooldown
        [SerializeField] private float cooldownStartTime;
        [SerializeField] private float timeUntilCooldown = 10;

        private Vector3 _hit;

        public enum States
        {
            Idle = 0,
            Patrol = 1,
            Warn = 2,
            Pursue = 3
        }

        // For Unity Editor
        [SerializeField] private States debugCurrentState;

        // Original state
        public States startState = States.Idle;

        List<EnemyState> iStates = new List<EnemyState>();

        void Awake()
        {
            player = GameObject.Find("player");
            enemySight = GetComponentInParent<EnemySight>();
            enemyPatrol = GetComponentInParent<EnemyPatrol>();
            enemyAnimator = GetComponentInParent<Animator>();
            stateMachine = GetComponentInParent<EnemyStateMachine>();
            
            }

        void Start()
        {
            iStates.AddRange(new EnemyState[]
            {
                new EnemyStateIdle(enemyAnimator, enemyPatrol), 
                new EnemyStatePatrol(enemyAnimator, enemyPatrol), 
                new EnemyStateWarn(enemyAnimator, enemyPatrol), 
                new EnemyStatePursue(enemyAnimator, enemyPatrol, player)
            });

            if (enemyPatrol.patrolPoints.Length > 0)
                stateMachine.SwitchState(iStates[(int) startState]);
            _hit = enemySight.globalRaycast;
        }

        void Update()
        {
            debugCurrentState = (States)iStates.IndexOf(stateMachine.GetCurrentState());
            
            currentTime += Time.deltaTime;

            if (playerSeen)
            {
                cooldownStartTime = currentTime;
            }
            
            if (!playerSeen)
            {
                //("resetting chase timer 1");
                chaseStartTime = currentTime;
            }

            if (playerSeen && (currentTime - chaseStartTime) > timeUntilChase && !isChasing)
            {
                stateMachine.SwitchState(iStates[(int) States.Pursue]);
                isChasing = true;
                cooledDown = false;
            } else if (!playerSeen && (currentTime - cooldownStartTime) > timeUntilCooldown && !cooledDown)
            {
                isChasing = false;
                cooledDown = true;
                stateMachine.SwitchState(iStates[(int) startState]);
            }
            
            stateMachine.ExecuteStateUpdate();
        }

        /// <summary>
        /// Switches state and navmesh destination based on Raycasts
        /// </summary>
        /// <param name="isPlayer"></param>
        public void OnRaycastHit(bool isPlayer)
        {
            if (isPlayer)
            {
                _hit = enemySight.globalRaycast;
                //(gameObject.name + ": " + stateMachine.GetCurrentState());
                if (!isChasing && stateMachine.GetCurrentState() != iStates[(int) States.Warn])
                {
                    stateMachine.SwitchState(iStates[(int)States.Warn]);
                }

                if (!playerSeen)
                {
                    //("resetting chase timer 2");
                    chaseStartTime = currentTime;
                }

                playerSeen = true;
                cooledDown = false;
                stateMachine.GetCurrentState().OnHit(_hit);
                //(gameObject.name + ": " + stateMachine.GetCurrentState());
            }
            else
            {
                //.Log("Player not seen");
                playerSeen = false;
                if (isChasing && stateMachine.GetCurrentState() != iStates[(int) States.Pursue])
                {
                    stateMachine.SwitchState(iStates[(int) States.Pursue]);
                    stateMachine.GetCurrentState().SetPlayer(player);
                    //stateMachine.GetCurrentState().OnHit(hit);
                }
            
                if (!isChasing)
                {
                    if (iStates.Count > 0)
                    {
                        if (cooledDown && stateMachine.GetCurrentState() != iStates[(int) startState])
                        {
                            stateMachine.SwitchState(iStates[(int) startState]);
                        }

                        if (!cooledDown && stateMachine.GetCurrentState() != iStates[(int) States.Warn])
                        {
                            stateMachine.SwitchState(iStates[(int) States.Warn]);
                        }
                    }
                }
                //stateMachine.SwitchToPreviousState();
            }
        }
        
        public void OnSoundHeard(Vector3 soundPosition)
        {
            _hit = soundPosition;
            if (stateMachine.GetCurrentState() != iStates[(int) States.Warn])
                {
                    Debug.Log("Sound heard");
                    stateMachine.SwitchState(iStates[(int) States.Warn]);
                    playerSeen = true;
                    cooledDown = false;
                    stateMachine.GetCurrentState().OnHit(_hit);
                }
        } 
    }
}
