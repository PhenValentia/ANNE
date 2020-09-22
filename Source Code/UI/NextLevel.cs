using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour // by HC2051
{
    private int nextSceneToLoad;
    private bool isThere;

    // Start is called before the first frame update
    private void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

     void OnTriggerEnter(Collider other)
   {
       if(other.tag == "Player")
       { 
           //player is next to the door
           isThere = true;
       } 
   }

void Update()
    {
       if(isThere == true){
           if(Input.GetMouseButtonDown(0)){
               SceneManager.LoadScene(nextSceneToLoad);
           }
    }
    }
}
