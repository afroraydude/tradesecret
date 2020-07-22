using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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

        [SerializeField] private bool playerSeen = false;
        [SerializeField] private bool cooledDown = true;
        [FormerlySerializedAs("chaseBegun")] [SerializeField] private bool isChasing = false;

        [SerializeField] private float scanStartTime;
        [SerializeField] private float currentTime;
        [SerializeField] private float timeUntilChase = 5;
        
        // cooldown
        [SerializeField] private float cooldownStartTime;
        [SerializeField] private float timeUntilCooldown = 5;

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

        List<IState> iStates = new List<IState>();

        void Awake()
        {
            enemySight = GetComponentInParent<EnemySight>();
            enemyPatrol = GetComponentInParent<EnemyPatrol>();
            enemyAnimator = GetComponentInParent<Animator>();
            stateMachine = GetComponentInParent<EnemyStateMachine>();

            iStates.AddRange(new IState[] { new StateIdle(enemyAnimator), new StatePatrol(enemyAnimator, enemyPatrol), new StateWarn(enemyAnimator), new StatePursue(enemyAnimator) });

            stateMachine.SwitchState(iStates[(int) startState]);
            
        }

        void Start()
        {

        }

        void Update()
        {
            debugCurrentState = (states)iStates.IndexOf(stateMachine._currentState);
            stateMachine.ExecuteStateUpdate();
            
            currentTime += Time.deltaTime;

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
                Debug.Log("Begin chase!");
                stateMachine.SwitchState(iStates[(int) states.pursue]);
                isChasing = true;
                cooledDown = false;
            } else if (!playerSeen && (currentTime - cooldownStartTime) > timeUntilCooldown && !cooledDown)
            {
                Debug.Log("Cooled down!");
                isChasing = false;
                cooledDown = true;
                stateMachine.SwitchState(iStates[(int) startState]);
            }
        }

        public void OnRaycastHit(bool isPlayer)
        {
            if (isChasing) stateMachine.SwitchState(iStates[(int) states.pursue]);
            if (!isChasing)
            {
                if (cooledDown) stateMachine.SwitchState(iStates[(int) startState]);
                if (!cooledDown) stateMachine.SwitchState(iStates[(int)states.warn]);
            }
            if (isPlayer)
            {
                if (!isChasing) stateMachine.SwitchState(iStates[(int)states.warn]);
                if (playerSeen == false) 
                    scanStartTime = currentTime;
                playerSeen = true;
                cooledDown = false;
            }
            else
            {
                playerSeen = false;
                stateMachine.SwitchToPreviousState();
            }
        }
    }
}
