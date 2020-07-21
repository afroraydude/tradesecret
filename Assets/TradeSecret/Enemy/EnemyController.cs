using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace TradeSecret.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        // Load components
        public EnemySight enemySight;
        public EnemyPatrol enemyPatrol;
        public Animator enemyAnimator;
        public EnemyStateMachine stateMachine;

        private enum states
        {
            idle = 0,
            patrol = 1,
            warn = 2,
            pursue = 3
        }

        // For Unity Editor
        [SerializeField] private states debugCurrentState;


        [SerializeField] private states startState;

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
        }
    }
}
