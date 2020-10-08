using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class Patrolpath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = 0.0f;
        [SerializeField] bool isRandom = true;
        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i);
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointGizmoRadius);
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(j).position);
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i)
        {
            if (isRandom)
            {
                float rnd = Random.Range(0, transform.childCount);
                int j = Mathf.FloorToInt(rnd);
                return j;
            }
            else
            {
                if (i + 1 == transform.childCount)
                {
                    return 0;
                }
                return i + 1;
            }

        }
    }
}
