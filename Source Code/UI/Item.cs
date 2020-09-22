using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Item // by JB2051 and NDS8 (Bug Fixes) (90:10)
{
    private int id;
    private bool playable;
    private string name;
    private string description;
    private string acompanyingText;
    private Sprite image;
    private GameObject itemObject;
    private string videoURL;
    private int quantity;
    public Item(int itemId, bool playable, string itemName, string itemDesc, Sprite img, GameObject obj, string vidURL = null, string acompText = null) //Initialisation for Item
    {
        this.id = itemId;
        this.playable = playable;
        this.name = itemName;
        this.description = itemDesc;
        this.acompanyingText = acompText;
        this.videoURL = vidURL;
        this.image = img;
        this.itemObject = obj;
        this.quantity = 1;
    }
    //Get and set methods for each of the variables
    public int getID()
    {
        return this.id;
    }

    public bool isPlayable()
    {
        return playable;
    }

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public string getAcompanyingText()
    {
        return acompanyingText;
    }

    public Sprite getImage()
    {
        return image;
    }

    public string getURL()
    {
        return videoURL;
    }

    public GameObject getObject()
    {
        return itemObject;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public void changeQuantity(int ammount)
    {
        quantity = quantity + ammount;
    }

    public void moveToHand() //Moves item to users hand
    {
        itemObject.SetActive(false); //Making item disappear
        itemObject.transform.parent = GameObject.Find("EventSystem").GetComponent<Inventory>().getHandPos().transform;//set it to be a child of hand
        itemObject.transform.localPosition = new Vector3(0, 0, 0);
        itemObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        itemObject.transform.localScale = new Vector3(1, 1, 1); //move it to the right place
        itemObject.SetActive(true); //Make item reappear
        wait();
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    public void hideObject()
    {
        itemObject.SetActive(false);
    }
}
