using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 20;

        public void TakeDamage(int damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0);
        }
    }

}