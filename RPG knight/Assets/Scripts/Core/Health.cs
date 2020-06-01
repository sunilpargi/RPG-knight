using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 20;

        bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(int damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0);
            if(health <= 0)
            {
                Die();
            }
           
        }

     void Die()
        {
            if (isDead) { return; }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().StopAction();
        }
    }

}