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


        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();

        }

        void Update()
        {

            UpdateAnimator();
        }


        public void StartMoveActioon(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
          
            navAgent.destination = destination;         
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
