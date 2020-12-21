using System;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5.0f;
        [SerializeField] private float attackRange = 2.0f;


        private void Update()
        {
            if (DistanceToPlayer() < chaseDistance)
            {
                print( gameObject.name + " is chasing ");
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
}