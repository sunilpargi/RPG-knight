using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (InteractiveToCombat()) { return; }
            if (InteractiveToMovement()) { return; } 
        }

        private bool InteractiveToCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {

                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!GetComponent<Fighter>().CanAttack(target))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractiveToMovement()
        {
           
          RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveActioon(hit.point);
                   
                }
               
                return true;
            }
            return false;

        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}