using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Mixer : MonoBehaviour  // HC395 
{
    private bool isThere;
    public GameObject inv;
    public GameObject UIObject;
    public GameObject UIObject2;

    // Start is called before the first frame update
    void Start()
    {
        UIObject.SetActive(false);
        UIObject2.SetActive(false);
    }

void OnTriggerEnter(Collider other)
   {
       if(other.tag == "Player")
       { 
           //player is next to the mixer
           isThere = true;
        UIObject2.SetActive(true);
       } 
   }

   void OnTriggerExit(Collider other)
   {
       if(other.tag == "Player")
       {
           //player is not next to the mixer
           isThere = false; 
           UIObject2.SetActive(false);
           UIObject.SetActive(false);
       }
   }

    // Update is called once per frame
    void Update()
    {
        UIObject2.SetActive(false);
        UIObject.SetActive(false);
        if (isThere == true){
           if(Input.GetMouseButtonDown(0)){
               //myAudio.Play();
                Debug.Log("clicked the mixed");
                if (inv.GetComponent<Inventory>().checkIngredients())
                {
                    Debug.Log("have all 3");
                    SceneManager.LoadScene("MixerWin");
                }
                else
                {
                    Debug.Log("do not have all 3");
                    UIObject2.SetActive(false);
                    UIObject.SetActive(true);
                }
           }
       } 
    }
}
