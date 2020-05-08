using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine.AI;
using RPG.Atributes;
using GameDevTV.Utils;

namespace RPG.Control
{
    public class AIControler : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 0.0f;
        [SerializeField] float suspitionTime = 0.0f;
        [SerializeField] Patrolpath patrolpath = null;
        [SerializeField] float dwellTime = 0.0f;
        [SerializeField] float waypointTolerance = 0.0f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.0f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        LazyValue<Vector3> guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = new LazyValue<Vector3>(GetGuardPos);
        }
        private void Start()
        {
            guardPosition.ForceInit();
        }

        private Vector3 GetGuardPos()
        {
            return transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) return;

            if (InRangeToPlayer(player) && !PlayerDead())
            {
                GetComponent<Animator>().ResetTrigger("Stop Suspicion");
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspitionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;
            if (patrolpath != null)
            {
                //Debug.Log("patrolpath is not null");
                if(AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > dwellTime)
            {
                mover.MoveTo(nextPosition, patrolSpeedFraction);
            }
        }
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolpath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolpath.GetWaypoint(currentWaypointIndex);
        }
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool PlayerDead()
        {
            return player.GetComponent<Health>().IsDead();
        }

        bool InRangeToPlayer(GameObject player)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}
