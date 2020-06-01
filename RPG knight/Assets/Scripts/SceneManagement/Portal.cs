using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    enum DestinationIdentifier
    {
        A, B , C ,D
    }


    [SerializeField] int Sceneindex = 0;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
       yield return  SceneManager.LoadSceneAsync(Sceneindex);

        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);
        Destroy(gameObject);

    }

    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        player.transform.rotation = otherPortal.spawnPoint.rotation;
    }

    private Portal GetOtherPortal()
    {
          foreach (Portal portal in FindObjectsOfType<Portal>())
          {
            if (portal == this) continue; // continue to other iteration

            if (portal.destination != destination) continue; // if other portal not matches with my destination ID then continue

            return portal;
          }
        return null;
     }
}
