using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FearManager : MonoBehaviour // by NDS8 and JB2051 (70%:30%)
{

    private Fear fear;
    public Camera mainCamera;
    public GameObject player;
    public AudioClip[] clips;
    public float timeToWait;
    public AudioSource audioSource;
    public GameObject mindAudioPos;
    public GameObject wallimage;
    public Sprite spriteimagewall;
    public GameObject floorimage;
    public Sprite spriteimagefloor;
    private AIFitnessFunction SAI;
    private Tai TAI;
    private PlayerReaction Reaction;
    public int playerCheckCount;
    public float lookThreshold; //percentage value cutoff for difference between value and mean for look speed
    public float movementThreshold; // same as above for movement
    private float reactionScore;
    public GameObject tooltip;
    public GameObject spawningPrefab;

    public void Start()
    {
        audioSource.clip = (AudioClip)Resources.Load("Sounds/InGameIntro");
        Debug.Log("Playing: " + audioSource.clip);
        StartCoroutine(playAudio());
        SAI = new AIFitnessFunction();
        TAI = new Tai(90.0f, 30.0f, 70.0f);         // Initialising Selection AI and Time AI
        //TAI = new Tai(30.0f, 10.0f, 70.0f);       //TAI testing values
        Reaction = new PlayerReaction(mainCamera, player, playerCheckCount, lookThreshold, movementThreshold); //Initialising Player reaction script
        StartCoroutine(StartRoutine()); //start process
        //Fear f = new Fear("ShadowHallucination", 100, 2, "Spawn");        // Testing Fear
        //doFear(f);                                                        // Testing Fear Run
    }

    IEnumerator StartRoutine()
    {
        timeToWait = TAI.getTime(); // get time from TAI
        Debug.Log("Time to wait: " + timeToWait);
        fear = SAI.FireOnce(); // get fear from SAI
        Debug.Log("Fear: " + fear.getFear()+ "Fear Type: " + fear.getFearType());
        yield return new WaitForSeconds(timeToWait); //wait time from TAI
        Debug.Log("Wait over, implementing fear");
        fear = doFear(fear); // implement fear
        doFear(new Fear("A", 50, 3, "Spawn"));
        yield return new WaitForSeconds(1f); //wait
        reactionScore = Reaction.checkReaction(fear, fear.returnObject()); //check reaction
        Debug.Log("Reaction Score: " + reactionScore);
        SAI.reballanceElement(fear, reactionScore); //send reaction score to reballance SAI
        yield return new WaitForSeconds(5f);
        StartCoroutine(StartRoutine()); //restart
    }

    IEnumerator DespawnObject(GameObject entity) // Remove Entity (Depriciated)
    {
        yield return new WaitForSeconds(10f);
        entity.SetActive(false);
    }

    private void changeAudioPos() // Change Audio Source to Random Position
    {
        mindAudioPos.transform.position =mainCamera.transform.position + (Random.insideUnitSphere * 3);
    }

    private float getDistance(Vector3 coor1, Vector3 coor2) // Check distance between 2 coordinates
    {
        return Mathf.Sqrt(Mathf.Pow(coor1.x - coor2.x,2) + Mathf.Pow(coor1.y - coor2.y,2));
    }

    private GameObject getWallPosition() // Find the closest wall position
    {
        GameObject shortest = null;
        float shortestDis = 100000f;
        List<GameObject> wallLocations = new List<GameObject>(GameObject.FindGameObjectsWithTag("WallImage"));
        foreach(GameObject Loc in wallLocations)
        {
            float a = getDistance(Camera.main.transform.position, Loc.transform.position);
            if (a < shortestDis)
            {
                shortestDis = a;
                shortest = Loc;
            }
        }
        return shortest;
    }

    private GameObject getFloorPosition() // Find closest floor position
    {
        GameObject shortest = null;
        float shortestDis = 100000f;
        List<GameObject> floorLocations = new List<GameObject>(GameObject.FindGameObjectsWithTag("FloorImage"));
        foreach (GameObject Loc in floorLocations)
        {
            float a = getDistance(Camera.main.transform.position, Loc.transform.position);
            if (a < shortestDis)
            {
                shortestDis = a;
                shortest = Loc;
            }
        }
        return shortest;
    }

    private IEnumerator playAudio() //IEnumerator for audio fears
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }

    private IEnumerator flashLights(GameObject obj) //IEnumerator for flashing lights fear
    {
        foreach (Transform child in obj.transform)
        {
            try
            {
                bool currentState = child.GetComponent<Light>().isActiveAndEnabled;
                child.GetComponent<Light>().enabled = false;
                yield return new WaitForSeconds(0.5f);
                child.GetComponent<Light>().enabled = true;
                yield return new WaitForSeconds(1.5f);
                child.GetComponent<Light>().enabled = false;
                yield return new WaitForSeconds(2f);
                child.GetComponent<Light>().enabled = currentState;
                yield return new WaitForSeconds(0.5f);
            }
            finally
            {
            }
        }
        Debug.Log("Lights Flash");
    }

    private Fear doFear(Fear fear) // Implement fear into game based on the fear's type
    {
        Debug.Log("Running Fear: "+fear.getFear());
        Debug.Log("Running Type: " + fear.getFearType());
        if (fear.getFearType() == "Audio")
        {
            //Debug.Log("AudioRandom Detected");
            changeAudioPos();
            audioSource.clip = (AudioClip)Resources.Load("Sounds/" + fear.getFear());
            Debug.Log("Playing: "+audioSource.clip);
            StartCoroutine(playAudio());
            fear.setGameObject(null);
            return fear;
        }

        if (fear.getFearType() == "AudioBehind")
        {
            mindAudioPos.transform.localPosition =mainCamera.transform.localPosition;
            audioSource.clip = (AudioClip)Resources.Load("Sounds/" + fear.getFear());
            Debug.Log("Playing: " + audioSource.clip);
            StartCoroutine(playAudio());
            fear.setGameObject(null);
            return fear;
        }

        if (fear.getFearType() == "VisualWall")
        {
            spriteimagewall = Resources.Load<Sprite>("Visuals/" + fear.getFear());
            wallimage = getWallPosition();
            wallimage.GetComponent<SpriteRenderer>().sprite = spriteimagewall;
            fear.setGameObject(wallimage);
            return fear;
        }

        if (fear.getFearType() == "VisualFloor")
        {
            spriteimagefloor = Resources.Load<Sprite>("Visuals/" + fear.getFear());
            floorimage = getFloorPosition();
            floorimage.GetComponent<SpriteRenderer>().sprite = spriteimagefloor;
            fear.setGameObject(floorimage);
            return fear;
        }

        if (fear.getFearType() == "Environmental")
        {
            if(fear.getFear() == "LightsFlickering")
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("LightSource");
                foreach (GameObject obj in objs)
                {
                    StartCoroutine(flashLights(obj));
                }
            }
            fear.setGameObject(null);
            return fear;
        }

        if (fear.getFearType() == "Spawn")
        {
            Vector3 spawnPos = new Vector3(player.transform.position.x + (Random.insideUnitCircle.normalized.x * 10), player.transform.position.y, player.transform.position.z + (Random.insideUnitCircle.normalized.y * 10));
            spawningPrefab = Resources.Load("Entities/" + fear.getFear()) as GameObject;
            GameObject being = Instantiate(spawningPrefab, spawnPos, new Quaternion(0,0,0,0));
            fear.setGameObject(being);
            StartCoroutine(DespawnObject(being));
            return fear;
        }

        if (fear.getFearType() == "MoveItem")
        {
            if(fear.getFear() == "DoorClosing")
            {
                float smallestDistance = 10000;
                GameObject theDoor = null;
                List<GameObject> doors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Door"));
                foreach (GameObject door in doors)
                {
                    float distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - door.transform.position.x, 2f) + Mathf.Pow(player.transform.position.z - door.transform.position.z, 2f));
                    if(distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        theDoor = door;
                    }
                }
                theDoor.GetComponent<DoorOpen>().forceDoorState(false);
            }
            fear.setGameObject(null);
            return fear;
        }
        else
        {
            fear.setGameObject(null);
            return fear;
        }
    }
}
