using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    bool playerInSight = false;

  void Start () {
        player = GameObject.FindGameObjectWithTag("Player");  
  }



  void Update () {


      if (player)
      {
          Vector3 forward = transform.TransformDirection(Vector3.forward);
          Vector3 toPlayer = player.transform.position - transform.position;

          playerInSight = Vector3.Dot(forward, toPlayer) > 0;
      }

      if (!playerInSight)
      {
          this.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
      }
      else
      {
          this.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.transform.position;
      }

  }
}
