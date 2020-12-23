using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5.0f;
        [SerializeField] private Fighter fighter;
        [SerializeField] private HealthComponent health;
        [SerializeField] private float maxSuspicionTime = 2.0f;

        private GameObject player;
        private Vector3 guardPosition;
        private Mover mover;
        private float timeLastSawPlayer = Mathf.Infinity;
        

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }


        private void Update()
        {
            if (IsInDistanceToPlayer() && health.IsAlive())
            {
                timeLastSawPlayer = 0.0f;
                AttackBehavior();
            }
            else if (timeLastSawPlayer < maxSuspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }

            timeLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehavior()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private bool IsInDistanceToPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

      


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}