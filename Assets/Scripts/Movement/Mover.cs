using RPG.Core;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {


        [SerializeField] private NavMeshAgent agent;
        private Ray lastRay;
        private Animator animator;
        private HealthComponent health;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        // Start is called before the first frame update
        void Start()
        {

            health = GetComponent<HealthComponent>();
            animator = GetComponent<Animator>();
            

        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimation();
            agent.enabled = health.IsAlive();
        }

        public void StartMoveAction(Vector3 direction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            if (agent.enabled)
            {
                agent.SetDestination(direction);
            }
            
           
            
        }

        public void MoveTo(Vector3 location)
        {
            agent.isStopped = false;
            
            agent.SetDestination(location);
        }

        public void Cancel()
        {
            agent.isStopped = true;
            agent.ResetPath();
        }


        private void UpdateAnimation()
        {
            Vector3 velocity = agent.velocity;

            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float currentSpeed = localVelocity.z;

            animator.SetFloat(ForwardSpeed, currentSpeed);
        }
       
    }
}
