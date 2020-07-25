using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using NotImplementedException = System.NotImplementedException;

namespace TradeSecret.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyState currentEnemyState;
        private EnemyState previousEnemyState;

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
        
        void SetPlayer(GameObject gameObject);
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
        
        public void SetPlayer(GameObject gameObject)
        {
            throw new NotImplementedException();
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
            if (patrol.agent && patrol.patrolPoints != null && patrol.patrolPoints.Length > 0)
                patrol.agent.destination = patrol.patrolPoints[patrol.destPoint].position;
            Debug.Log(patrol.agent.remainingDistance);
        }

        public void OnUpdate()
        {
            animator.SetBool("isWalking", true);
            if (patrol.agent.remainingDistance < patrol.minRemainingDistance)
            {
                animator.SetBool("isWalking", false);
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
        
        public void SetPlayer(GameObject gameObject)
        {
            throw new NotImplementedException();
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
            patrol.agent.destination = _raycastHit.point;
        }
        
        public void SetPlayer(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }

    public class EnemyStatePursue : EnemyState
    {
        // TODO: Finish Pursuit state
        private readonly Animator _animator;
        private EnemyPatrol _patrol;
        private RaycastHit _raycastHit;
        private GameObject _player;
        private float _oldSpeed;


        public EnemyStatePursue(Animator animator, EnemyPatrol patrol, GameObject player)
        {
            this._animator = animator;
            this._patrol = patrol;
            this._player = player;
            if (patrol.agent)
            this._oldSpeed = patrol.agent.speed;
        }

        public void OnEnter()
        {
            _animator.SetBool("isWalking", true);
            _patrol.agent.speed = 5.0F;
        }

        public void OnUpdate()
        {
            _animator.SetBool("isWalking", true);
            _patrol.agent.destination = _player.transform.position;
        }

        public void OnExit()
        {
            _animator.SetBool("isWalking", false);
            _patrol.agent.speed = _oldSpeed;
        }

        public void OnHit(RaycastHit hit)
        {
            _raycastHit = hit;
        }

        public void SetPlayer(GameObject gameObject)
        {
            this._player = gameObject;
        }
    }
}