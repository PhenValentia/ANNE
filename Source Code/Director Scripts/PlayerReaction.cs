using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerReaction : MonoBehaviour // by NDS8 and JB2051
{
    int averageCameraMovement;
    public Camera playerCam;
    public GameObject player;
    public int framesToCheck; // frames to check movement between
    public float averageLookSpeed; //overall average look speed of player
    public float lastLookSpeed; //last recorded look speed of player
    public int playerCheckCount; // variable for lookspeed to count to 30 frames
    private Quaternion lastCameraPos; 
    private float lookThreshold; // percentage above/below average that "something is fishy"
    public float averageMovementSpeed;
    public float lastMovementSpeed;
    private Vector3 lastPlayerPosition;
    private float movementThreshold;
    private int countEscapePresses;
    private bool isShifting;
    private bool isCrouching;
    private float totalLookSpeed;
    private int lookSpeedCount;
    private float totalMovementSpeed;
    private int movementSpeedCount;
    private int playerCheckCountMove;
    private ArrayList corners;

    public PlayerReaction(Camera playerCam, GameObject player, int framesToCheck, float lookThreshold, float movementThreshold)
    {
        this.playerCam = playerCam;
        this.player = player;
        this.framesToCheck = framesToCheck;
        this.lookThreshold = lookThreshold;
        this.movementThreshold = movementThreshold;
    }
    public void Start()
    {
        playerCheckCount = 0;
        playerCheckCountMove = 0;
        corners = new ArrayList();
    }

    public void FixedUpdate()
    {
        DoFixedUpdate();
    }

    public void DoFixedUpdate()
    {
        
        playerCheckCount += 1;
        playerCheckCountMove += 1;
        if (playerCheckCount % framesToCheck/2 == 0)
        {
            updateLookSpeed(playerCam.transform.rotation);
        }
        if (playerCheckCount % framesToCheck / 2 == 0)
        {
            updateMovementSpeed(player.GetComponent<Rigidbody>().velocity);  //Get speed of player
        }
        if (Input.GetKeyDown(KeyCode.Escape)) //Monitoring Key presses
        {
            countEscapePresses += 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShifting = true;
        }
        else
        {
            isShifting = false;
        }
    }

    public float checkReaction(Fear fear, GameObject obj) //returns specific value based on fear and measurements
    {
        string fearType = fear.getFearType();
        if(fearType == "AudioRandom")
        {
            return ((1-getLookSpeed()) + getMovementSpeed() / 2);
        }
        else if(fearType == "AudioBehind")
        {
            return (getLookSpeed() + getMovementSpeed() / 2);
        }
        else if(fearType == "VisualWall")
        {
            corners.AddRange(obj.GetComponent<Properties>().getCorners());
            if (checkCoordInView(corners))
            {
                return (getLookSpeed() + getMovementSpeed() / 2);
            }
            else
            {
                return 0.0f;
            }

        }
        else if (fearType == "VisualFloor")
        {
            return getLookSpeed() + getMovementSpeed() / 2;
        }
        else if (fearType == "MoveItem")
        {
            corners.AddRange(obj.GetComponent<Properties>().getCorners());
            if (checkCoordInView(corners))
            {
                return (Math.Abs(getLookSpeed()) + getMovementSpeed() / 2);
            }
            else
            {
                return 0.0f;
            }
        }
        else if (fearType == "Spawn")
        {
            corners.AddRange(obj.GetComponent<Properties>().getCorners());
            if (checkCoordInView(corners))
            {
                return (getLookSpeed() + getMovementSpeed() / 2);
            }
            else
            {
                return 0.0f;
            }
        }
        else if(fearType == "Test")
        {
            Debug.Log("MS");
            Debug.Log(getMovementSpeed());
            Debug.Log("LS");
            Debug.Log(getLookSpeed());
        }
        return 0.0f;
    }

    public float getLookSpeed() //obtains look speed
    {
        if (lastLookSpeed >= averageLookSpeed * (1+lookThreshold) || lastLookSpeed <= averageLookSpeed * lookThreshold)
        {
            return lastLookSpeed - averageLookSpeed / averageLookSpeed;
        }
        else
        {
            return 0f;
        }
    }

    private void updateLookSpeed(Quaternion cameraPos) //updates look speed
    {
        if (playerCheckCount == framesToCheck/2)
        {
            lastCameraPos = cameraPos;
        }
        if (playerCheckCount == framesToCheck)
        {
            lookSpeedCount += 1;
            lastLookSpeed = Math.Abs(lastCameraPos.y - cameraPos.y);
            totalLookSpeed += lastLookSpeed;
            averageLookSpeed = totalLookSpeed / lookSpeedCount;
            playerCheckCount = 0;
        }
    }

    public float getMovementSpeed() //obtains movement speed
    {
        if(lastMovementSpeed >= averageMovementSpeed * (1+movementThreshold) || lastMovementSpeed <= averageMovementSpeed * movementThreshold)
        {
            return lastMovementSpeed - averageMovementSpeed / averageMovementSpeed;
        }
        else
        {
            return 0.0f;
        }
    }

    private void updateMovementSpeed(Vector3 speed) //refreshes movement speed
    {
        if (playerCheckCountMove == framesToCheck)
        {
            movementSpeedCount += 1;
            totalMovementSpeed += speed.magnitude;
            lastMovementSpeed = speed.magnitude;
            averageMovementSpeed = totalMovementSpeed / movementSpeedCount;
            playerCheckCountMove = 0;
        }
    }

    private bool checkCoordInView(ArrayList coordinates) //checks if the coordinates of the corners of an object are in the players Line of sights
    {
        bool returnval = false;
        foreach (Vector3 coordinate in coordinates) {
            returnval = checkCoord(coordinate) || returnval;
        }
        //return returnval;
        return true;
    }

    private bool checkCoord(Vector3 coordinate)
    {
        Vector3 pointcoords = playerCam.WorldToViewportPoint(coordinate);
        return pointcoords.z > 0 && pointcoords.x > 0 && pointcoords.x < 1 && pointcoords.y > 0 && pointcoords.y < 1;
    }


}
