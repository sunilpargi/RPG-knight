using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Control;

namespace RPG.Core
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float suspiciousTime = 5f;

        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointToTolerate = 1f;
        int CurrentWayPointIndex = 0;

        [SerializeField] float wayPointDwellTime = 3f;
         float timeSinceArrivedAtWayPoint = Mathf.Infinity;

        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
            health = GetComponent<Health>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
            
        }

        private void Update()
        {
            if (health.IsDead()) { return; }

            if (IsAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }

            else if (timeSinceLastSawPlayer < suspiciousTime)
            {
                SuspicionBehaviour();
            }

            else
            {
                PatrolBehaviour();

            }
            UpdateTimers();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWayPoint += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().StopAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if(patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArrivedAtWayPoint = 0;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }

            if (timeSinceArrivedAtWayPoint > wayPointDwellTime)
            {
                mover.StartMoveAction(nextPosition,patrolSpeedFraction);
            }
           
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointToTolerate;
        }

        private void CycleWayPoint()
        {
            CurrentWayPointIndex = patrolPath.GetnextIndex(CurrentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(CurrentWayPointIndex);
        }

        private bool IsAttackRangeOfPlayer()
        {
           float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
           return distanceToPlayer < chaseDistance;
        }


        //called by unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

   
}