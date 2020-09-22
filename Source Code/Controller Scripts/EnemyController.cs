using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyController : MonoBehaviour // Entirely by NDS8
{
    //Set Up Body, Rigidbody and Player
    [SerializeField]
    GameObject body;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    GameObject player = null;

    // Upon Starting, Allocate All Variables, Set Death after 30 seconds
    void Start()
    {
        body = transform.GetChild(0).gameObject;
        rb = body.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 30);
    }

    // Upon Spawning, Move Toward Player, Collider Removed to Move through Walls like a Ghost
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 2f * Time.deltaTime);
    }
}
