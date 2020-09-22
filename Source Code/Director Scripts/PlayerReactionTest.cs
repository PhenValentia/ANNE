using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReactionTest : MonoBehaviour { // by JB2051
    PlayerReaction pr;
    GameObject objectTest;
    public Camera playerCam;
    public GameObject player;
    public int playerCheckCount; //Frames between checking look/movement distance
    public float lookThreshold; //percentage above/below average that triggers event for looking
    public float movementThreshold; //percentage above/below average that triggers event for movement
    void Start()
    {
        pr = new PlayerReaction(playerCam, player, playerCheckCount, lookThreshold, movementThreshold);
    }

    void Update()
    {
        pr.checkReaction(new Fear("Test", 0, 0, "Test"), objectTest);
    }

    void FixedUpdate()
    {
        pr.DoFixedUpdate();
    }
}
