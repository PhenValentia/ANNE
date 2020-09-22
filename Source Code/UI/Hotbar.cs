using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour // by JB2051
{
    public GameObject selectionIndicator;
    public GameObject hotbarItem1;
    public GameObject hotbarItem2;
    public GameObject hotbarItem3;
    private GameObject currentlySelected;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("1")) //Check if number keys are pressed
        {
            currentlySelected = hotbarItem1; //set variable to select relevant item
            selectionIndicator.transform.position = hotbarItem1.transform.position; //move on screen hotbar item identifier
        }
        if (Input.GetKeyDown("2"))
        {
            currentlySelected = hotbarItem2;
            selectionIndicator.transform.position = hotbarItem2.transform.position;
        }
        if (Input.GetKeyDown("3"))
        {
            currentlySelected = hotbarItem3;
            selectionIndicator.transform.position = hotbarItem3.transform.position;
        }
    }

    public void setItemHotbar(int hotbarnumber, Item item)
    {
        if(hotbarnumber == 1)
        {
            hotbarItem1.GetComponentInChildren<Image>().sprite = item.getImage(); //set sprite of item to be the new image for the hotbar item
            hotbarItem1.gameObject.transform.Find("Qty").GetComponent<Text>().text = "x" + item.getQuantity(); //show quantity of the item in hotbar
        }
        else if(hotbarnumber == 2)
        {
            hotbarItem2.GetComponentInChildren<Image>().sprite = item.getImage();
            hotbarItem2.gameObject.transform.Find("Qty").GetComponent<Text>().text = "x" + item.getQuantity();
        }
        else if(hotbarnumber == 3)
        {
            hotbarItem3.GetComponentInChildren<Image>().sprite = item.getImage();
            hotbarItem3.gameObject.transform.Find("Qty").GetComponent<Text>().text = "x" + item.getQuantity();
        }
    }

    public int currentSelected() //return currently selected item
    {
        if(currentlySelected == hotbarItem1)
        {
            return 1;
        }
        else if(currentlySelected == hotbarItem2)
        {
            return 2;
        }
        return 3;
    }
}
