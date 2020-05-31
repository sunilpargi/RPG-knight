using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
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
        }
    }

}