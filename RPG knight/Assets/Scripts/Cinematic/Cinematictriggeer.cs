using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class Cinematictriggeer : MonoBehaviour
    {
        bool alreadytrigger = false;

        private void OnTriggerEnter(Collider other)
        {
            if(!alreadytrigger && other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                alreadytrigger = true;
            }
          
        }
    }
}


