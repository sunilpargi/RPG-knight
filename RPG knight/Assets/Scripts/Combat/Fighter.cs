using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        Health target;

        [SerializeField] float weaponRange = 2f;
        [SerializeField] int weaponDamage = 10;
        [SerializeField] float timeBetweenAttack = 1f;

        float timeSinceTimeAttack = Mathf.Infinity;

        Animator anim;
 

      

        private void Start()
        {
            anim = GetComponent<Animator>();

        }
        private void Update()
        {
            timeSinceTimeAttack += Time.deltaTime;

            if (!target) { return; }

            if (target.IsDead()) { return; }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position,1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceTimeAttack > timeBetweenAttack)
            {
                TriggerAttack();
                timeSinceTimeAttack = 0;
                Hit();
            }
        }

        private void TriggerAttack()
        {
            anim.ResetTrigger("stopAttack");
            anim.SetTrigger("Attack");
        }

        //Animation Event
        void Hit()
        {
          if(target == null) { return; }

            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < 2f;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();

        }

        public void Attack(GameObject CombatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = CombatTarget.GetComponent<Health>();
           
         
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            anim.ResetTrigger("Attack");
            anim.SetTrigger("stopAttack");
        }

    }
}
