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
        private HealthComponent attackTarget;


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (!attackTarget)
            {
                print("No Attack Target");
                return;
            }

            bool isInRange = Vector3.Distance(transform.position, attackTarget.transform.position) < attackRange;
            
            if (!isInRange)
            {
                mover.MoveTo(attackTarget.transform.position);
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
            if (!attackTarget.IsAlive()) return;
            transform.LookAt(attackTarget.transform.position);
            animator.ResetTrigger("StopAttack");
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0.0f;
        }

        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            attackTarget = target.transform.GetComponent<HealthComponent>();
            print(attackTarget.name);
        }


        public void Cancel()
        {
            attackTarget = null;
            animator.SetTrigger("StopAttack");
            animator.ResetTrigger("Attack");
        }

        // Animation Event
        void Hit()
        {
            if (!attackTarget)
            {
                return;
            }

            if (!IsInAttackRange())
            {
                return;
            }
            attackTarget.AdjustHealth(-weaponDamage);
        }


        private bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, attackTarget.transform.position) < attackRange;
        }
    }
}