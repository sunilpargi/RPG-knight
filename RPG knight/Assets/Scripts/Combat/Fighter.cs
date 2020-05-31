using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        Transform target;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttack = 1f;
        float timeSinceTimeAttack = 0;

        Animator anim;
 

        [SerializeField] int weaponDamage = 10;

        private void Start()
        {
            anim = GetComponent<Animator>();

        }
        private void Update()
        {
            timeSinceTimeAttack += Time.deltaTime;

            if (!target) { return; }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
                print("Hi");
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceTimeAttack > timeBetweenAttack)
            {
                print("Hello");
                anim.SetTrigger("Attack");
                timeSinceTimeAttack = 0;
                Hit();
            }
        }

        //Animation Event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < 2f;
        }

        public void Attack(CombatTarget CombatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = CombatTarget.transform;
           
         
        }

        public void Cancel()
        {
            target = null;
        }

       
    }
}
