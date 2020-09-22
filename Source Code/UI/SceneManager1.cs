using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour // by HC395
{
    public static SceneManager1 instance = null;

    public GameObject player;
    public GameObject [] stairArray;

    public int currentStairNumber;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }

        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player");

        }

        if(stairArray.Length == 0){
            stairArray = GameObject.FindGameObjectsWithTag("Stair");
        }
    }

    void OnLevelWasLoaded()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stairArray = GameObject.FindGameObjectsWithTag("Stair");
    }

    public void LoadScene(int passedStairNumber)
    {
      currentStairNumber = passedStairNumber;

       if(Application.loadedLevel == 1)
       {
        SceneManager.LoadScene(2);
       }
       else if(Application.loadedLevel == 2)
       {
        Application.LoadLevel(1);
       }

       
    }

}
