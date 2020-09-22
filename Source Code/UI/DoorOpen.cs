using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour // by HC395 and JB2051 (Small Redesign) and NDS8 (Bug Fixes) (70:20:10)
{
    private Animator _animator;
    private bool isThere;
    private bool open;
    AudioSource myAudio;
    private bool forcedState;
    private bool forceOpen;
    public GameObject UIObject;
    
    

    // Start is called before the first frame update
    void Start()
    {
        //get the animator 
        _animator = GetComponent<Animator>();
        //get the audioclip
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = Resources.Load<AudioClip>("Sounds/door_open");
        //set the ui text to fasle 
        UIObject.SetActive(false);
    }

   void OnTriggerEnter(Collider other)
   {
       if(other.tag == "Player")
       { 
           //player is next to the door
           isThere = true;
           UIObject.SetActive(true);
       } 
   }

   void OnTriggerExit(Collider other)
   {
       if(other.tag == "Player")
       {
           //player is not next to the door
           isThere = false; 
           UIObject.SetActive(false);
       }
   }

    public void forceDoorState(bool state)
    {
        forcedState = true;
        forceOpen = state;
    }

   

    void Update()
    {
       if(isThere == true && open == false){
           if(Input.GetMouseButtonDown(0)){
               //play audio 
              myAudio.Play();
              //open door
               _animator.SetTrigger("Open");
               open = true;
           }
       } else if(isThere == true && open == true){
          
           if(Input.GetMouseButtonDown(0)){
               //play audio 
               myAudio.Play();
               //close door
               _animator.SetTrigger("Close");
               open = false;
           }
 
       } else if(forcedState == true)
        {
            if (forceOpen)
            {
                myAudio.Play();
                _animator.SetTrigger("Open");
                open = true;
                forcedState = false;
            }
            else
            {
                myAudio.Play();
                _animator.SetTrigger("Close");
                open = false;
                forcedState = false;
            }
        }
    
       
       

    }
}
  