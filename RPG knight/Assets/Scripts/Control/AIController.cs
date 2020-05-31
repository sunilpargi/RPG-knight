﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;


        private void Update()
        {
            if(DistanceToPlayer() < chaseDistance)
            {

            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }

   
}