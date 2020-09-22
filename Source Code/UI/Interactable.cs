using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour // by JB2051 and NDS8 (90:10)
{
    public bool isItem;
    public int id;
    public string n;
    private string description;
    private string videoURL;
    private string acompanyText;
    private bool playable;
    public Sprite itemSprite;
    public GameObject itemObject;

        
    void Start()
    {
        string[] lines = System.IO.File.ReadAllLines("./Assets/Scripts/UI/ItemLookup.txt"); //Reads data from ItemLookup CVS file
        foreach(string line in lines) //Assigning variables of item to that stored in CSV
        {
            string[] linelist = line.Split(',');
            if(linelist[0].Equals(id.ToString()))
            {
                if(linelist[1] == "f")
                {
                    playable = false;
                }
                else if(linelist[1] == "t")
                {
                    playable = true;
                }
                n = linelist[2];
                description = linelist[3];
                videoURL = linelist[4];
                acompanyText = linelist[5]; 
            }
        }
    }

    void InteractItem()
    {
        GameObject.Find("EventSystem").GetComponent<Inventory>().addItem(new Item(this.id, this.playable, this.n, this.description, this.itemSprite, this.itemObject, this.videoURL, this.acompanyText)); //Adds item to inventory
        itemObject.SetActive(false);
    }

    public GameObject getItem()
    {
        return this.itemObject;
    }

    public void clickedOn()
    {
        if (isItem)
        {
            InteractItem();
        }
    }

}
