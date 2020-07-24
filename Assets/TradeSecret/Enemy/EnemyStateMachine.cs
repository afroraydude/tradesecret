using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace TradeSecret.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [FormerlySerializedAs("_currentState")] public EnemyState currentEnemyState;
        [FormerlySerializedAs("_previousState")] public EnemyState previousEnemyState;

        /// <summary>
        /// change our state
        /// </summary>
        /// <param name="newEnemyState"></param>
        public void SwitchState(EnemyState newEnemyState)
        {
            if (currentEnemyState != null)
            {
                this.currentEnemyState.OnExit();
                this.previousEnemyState = currentEnemyState;
            }

            this.currentEnemyState = newEnemyState;
            this.currentEnemyState.OnEnter();
        }

        /// <summary>
        /// Update the states execution
        /// </summary>
        public void ExecuteStateUpdate()
        {
            // if our state is null return
            if (this.currentEnemyState == null)
                return;

            this.currentEnemyState.OnUpdate();
        }

        /// <summary>
        /// Switch to our previous state is there was one
        /// </summary>
        public void SwitchToPreviousState()
        {
            if (this.currentEnemyState == null || previousEnemyState == null)
                return;

            this.currentEnemyState.OnExit();
            this.currentEnemyState = previousEnemyState;
            this.currentEnemyState.OnEnter();
        }

        public EnemyState GetCurrentState()
        {
            return currentEnemyState;
        }

        public void SetHit(RaycastHit hit)
        {
            this.currentEnemyState.OnHit(hit);
        }
    }

    public interface EnemyState
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

        void OnHit(RaycastHit hit);
    }

    public class EnemyStateIdle : EnemyState
    {
        private readonly Animator animator;
        private EnemyPatrol patrol;
        private RaycastHit _raycastHit;


        public EnemyStateIdle(Animator animator, EnemyPatrol patrol)
        {
            this.animator = animator;
            this.patrol = patrol;
        }

        public void OnEnter()
        {
            animator.SetBool("isWalking", false);
        }

        public void OnUpdate()
        {
        }

        public void OnExit()
        {
            animator.SetBool("isWalking", false);
        }

        public void OnHit(RaycastHit hit)
        {
            _raycastHit = hit;
        }
    }

    public class EnemyStatePatrol : EnemyState
    {
        private readonly Animator animator;
        private EnemyPatrol patrol;
        private RaycastHit _raycastHit;


        public EnemyStatePatrol(Animator animator, EnemyPatrol patrol)
        {
            this.animator = animator;
            this.patrol = patrol;
        }

        public void OnEnter()
        {
            animator.SetBool("isWalking", true);
        }

        public void OnUpdate()
        {
            animator.SetBool("isWalking", true);
            if (patrol.agent.remainingDistance < patrol.minRemainingDistance)
            {
                patrol.GoToNextPoint();
            }
        }

        public void OnExit()
        {
            animator.SetBool("isWalking", false);
        }
        
        public void OnHit(RaycastHit hit)
        {
            _raycastHit = hit;
        }
    }

    public class EnemyStateWarn : EnemyState
    {
        private readonly Animator animator;
        private EnemyPatrol patrol;
        private RaycastHit _raycastHit;


        public EnemyStateWarn(Animator animator, EnemyPatrol patrol)
        {
            this.animator = animator;
            this.patrol = patrol;
        }
        
        public void OnEnter()
        {
            animator.SetBool("isWalking", false);
            patrol.agent.destination = _raycastHit.point;
        }

        public void OnUpdate()
        {
            
            if (patrol.agent.remainingDistance < 0.5)
            {
                Debug.Log("At point");
                animator.SetBool("isWalking", false);
                animator.SetBool("isLooking", true);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isLooking", false);
            }
        }

        public void OnExit()
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isLooking", false);
        }
        
        public void OnHit(RaycastHit hit)
        {
            _raycastHit = hit;
        }
    }

    public class EnemyStatePursue : EnemyState
    {
        private readonly Animator animator;
        private EnemyPatrol patrol;
        private RaycastHit _raycastHit;


        public EnemyStatePursue(Animator animator, EnemyPatrol patrol)
        {
            this.animator = animator;
            this.patrol = patrol;
        }

        public void OnEnter()
        {
            animator.SetBool("isWalking", true);
        }

        public void OnUpdate()
        {
            animator.SetBool("isWalking", true);
        }

        public void OnExit()
        {
            animator.SetBool("isWalking", false);
        }
        
        public void OnHit(RaycastHit hit)
        {
            _raycastHit = hit;
        }
    }
}