using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        NavMeshAgent navAgent;
        Animator anim;
        Health health;
        [SerializeField] float maxSpeed = 6f;


        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();

        }

        void Update()
        {
            navAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }


        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speed)
        {
          
            navAgent.destination = destination;
            navAgent.speed = maxSpeed * Mathf.Clamp01(speed);
            navAgent.isStopped = false;
        }

        public void Cancel()
        {
            navAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            anim.SetFloat("forwardSpeed", speed);

        }

    }
}
