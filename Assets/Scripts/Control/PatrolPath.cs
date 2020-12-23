using System.Collections.Generic;
using UnityEngine;



namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] public float radius = 2.0f; 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), radius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        private int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }

            return i + 1;
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
    
}
