using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2.0f;
        [SerializeField] private Mover mover;
        [SerializeField] private Animator animator;
        [SerializeField] private float timeBetweenAttacks = 1.0f;
        [SerializeField] private float weaponDamage = 10.0f;

        private float timeSinceLastAttack;
        private Transform attackTarget;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (!attackTarget)
            {
                print("No Attack Target");
                return;
            }

            bool isInRange = Vector3.Distance(transform.position, attackTarget.position) < attackRange;
            
            if (!isInRange)
            {
                mover.MoveTo(attackTarget.position);
            }
            else
            {
                mover.Cancel();
                
                AttackBehavior();
                
            }

            


        }

        private void AttackBehavior()
        {
            if (!(timeSinceLastAttack > timeBetweenAttacks)) return;
            
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0.0f;
           




        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            attackTarget = target.transform;
            
            
        }


        public void Cancel()
        {
            attackTarget = null;
        }

        // Animation Event
        void Hit()
        {
           HealthComponent health = attackTarget.GetComponent<HealthComponent>();
           health.AdjustHealth(-weaponDamage);
        }
    }
}