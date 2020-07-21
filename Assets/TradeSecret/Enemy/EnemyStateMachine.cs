using UnityEngine;
using System.Collections;

namespace TradeSecret.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        public IState _currentState;
        public IState _previousState;

        /// <summary>
        /// change our state
        /// </summary>
        /// <param name="newState"></param>
        public void SwitchState(IState newState)
        {
            if (_currentState != null)
            {
                this._currentState.OnExit();
                this._previousState = _currentState;
            }

            this._currentState = newState;
            this._currentState.OnEnter();
        }

        /// <summary>
        /// Update the states execution
        /// </summary>
        public void ExecuteStateUpdate()
        {
            // if our state is null return
            if (this._currentState == null)
                return;

            this._currentState.OnUpdate();
        }

        /// <summary>
        /// Switch to our previous state is there was one
        /// </summary>
        public void SwitchToPreviousState()
        {
            if (this._currentState == null || _previousState == null)
                return;

            this._currentState.OnExit();
            this._currentState = _previousState;
            this._currentState.OnEnter();
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }
    }

    public interface IState
    {

        /// <summary>
        /// This is called when this state is entered
        /// </summary>
        void OnEnter();

        /// <summary>
        /// This is called every frame
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// This is called when we exit the state
        /// </summary>
        void OnExit();
    }

    public class StateIdle : IState
    {
        private readonly Animator animator;
        private static readonly int isState = Animator.StringToHash("isIdling");


        public StateIdle(Animator animator)
        {
            this.animator = animator;
        }

        public void OnEnter()
        {
            animator.SetTrigger(isState);
        }

        public void OnUpdate()
        {
        }

        public void OnExit()
        {

        }
    }

    public class StatePatrol : IState
    {
        private readonly Animator animator;
        private static readonly int isState = Animator.StringToHash("isPatrolling");
        private EnemyPatrol patrol;


        public StatePatrol(Animator animator, EnemyPatrol patrol)
        {
            this.animator = animator;
            this.patrol = patrol;
        }

        public void OnEnter()
        {
            animator.SetTrigger(isState);
        }

        public void OnUpdate()
        {
        }

        public void OnExit()
        {

        }
    }

    public class StateWarn : IState
    {
        private readonly Animator animator;
        private static readonly int isState = Animator.StringToHash("isWarning");


        public StateWarn(Animator animator)
        {
            this.animator = animator;
        }

        public void OnEnter()
        {
            animator.SetTrigger(isState);
        }

        public void OnUpdate()
        {
        }

        public void OnExit()
        {

        }
    }

    public class StatePursue : IState
    {
        private readonly Animator animator;
        private static readonly int isState = Animator.StringToHash("isPursuing");


        public StatePursue(Animator animator)
        {
            this.animator = animator;
        }

        public void OnEnter()
        {
            animator.SetTrigger(isState);
        }

        public void OnUpdate()
        {
        }

        public void OnExit()
        {

        }
    }
}