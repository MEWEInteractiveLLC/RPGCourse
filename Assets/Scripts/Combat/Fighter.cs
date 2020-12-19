using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2.0f;
        public float GetAttackRange => attackRange;

        [SerializeField] private Mover mover;

        private Transform attackTarget;

       

        private void Update()
        {
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
                print("We are in range");
                return;
            }
            
            
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
    }
}