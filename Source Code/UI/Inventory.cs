using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour // by JB2051 and NDS8 (65:35)
{
    private Item hotbarItem1;
    private Item hotbarItem2;
    private Item hotbarItem3;
    public Canvas canvas;
    public List<Item> allItems;
    public bool isShown;
    Item i;
    public GameObject inventoryObject;
    public GameObject playerMovement;
    public GameObject Grid;
    public Sprite sprtlighter;
    public GameObject handPosition;
    public int allThree; 
    public bool containAllIngredients;
    public GameObject gui1;
    public GameObject gui2;
    public GameObject gui3;

    // Establishing all values
    void Start()
    {
        allItems = new List<Item>();
        hotbarItem1 = null;
        hotbarItem2 = null;
        hotbarItem3 = null;
        isShown = false;
        Grid.GetComponent<InventoryItemController>().deleteList();
        containAllIngredients = false;
    }

    // Allows user to interact with the inventory in each frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleInventory();
            inventoryObject.SetActive(isShown);
            playerMovement.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().setActive(!isShown);
        }
        if(canvas.GetComponent<Hotbar>().currentSelected() == 1)
        {
            if (hotbarItem1 != null)
            {
                hotbarItem1.moveToHand();
            }
            if (hotbarItem2 != null)
            {
                hotbarItem2.hideObject();
            }
            if(hotbarItem3 != null)
            {
                hotbarItem3.hideObject();
            }
        }
        if (canvas.GetComponent<Hotbar>().currentSelected() == 2)
        {
            if (hotbarItem1 != null)
            {
                hotbarItem1.hideObject();
            }
            if (hotbarItem2 != null)
            {
                hotbarItem2.moveToHand();
            }
            if (hotbarItem3 != null)
            {
                hotbarItem3.hideObject();
            }
        }
        if (canvas.GetComponent<Hotbar>().currentSelected() == 3)
        {
            if (hotbarItem1 != null)
            {
                hotbarItem1.hideObject();
            }
            if (hotbarItem2 != null)
            {
                hotbarItem2.hideObject();
            }
            if (hotbarItem3 != null)
            {
                hotbarItem3.moveToHand();
            }
        }
    }

    public void addItem(Item item)  // Add an item to the inventory
    {
        int j = 0;
        foreach(Item i in allItems)
        {
            if(i.getID() == item.getID())
            {
                j = 1;
                i.changeQuantity(1);
            }
        }
        if(j == 0)
        {
            allItems.Add(item);
        }
        if(item.getID() == 2)
        {
            gui1.SetActive(true);
        }
        if (item.getID() == 3)
        {
            gui2.SetActive(true);
        }
        if (item.getID() == 4)
        {
            gui3.SetActive(true);
        }
    }

    public void removeItem(Item item)  // Remove an item from the inventory
    {
        foreach(Item i in allItems)
        {
            if(i.getID() == item.getID())
            {
                if(i.getQuantity() > 1)
                {
                    i.changeQuantity(-1);
                }
                else
                {
                    allItems.Remove(i);
                }
            }
        }
    }

    public void addItemHotbar1(Item item)  // Add the selected item to slot 1
    {
        if (hotbarItem1 != null)
        {
            hotbarItem1.hideObject();
        }
        hotbarItem1 = item;
        canvas.GetComponent<Hotbar>().setItemHotbar(1, item);

    }

    public void addItemHotbar2(Item item)  // Add the selected item to slot 2
    {
        if (hotbarItem2 != null)
        {
            hotbarItem2.hideObject();
        }
        hotbarItem2 = item;
        canvas.GetComponent<Hotbar>().setItemHotbar(2, item);
    }

    public void addItemHotbar3(Item item) // Add the selected item to slot 3
    {
        if (hotbarItem3 != null)
        {
            hotbarItem3.hideObject();
        }
        hotbarItem3 = item;
        canvas.GetComponent<Hotbar>().setItemHotbar(3, item);
    }

    public void toggleInventory() // Toggle the display of the inventory
    {
        if (isShown)
        {
            isShown = false;
            Cursor.visible = false;
        }
        else
        {
            isShown = true;
            Cursor.visible = true;
            Grid.GetComponent<InventoryItemController>().makeList(allItems);
        }
    }

    public void refreshList() // Refresh display of list
    {
        Grid.GetComponent<InventoryItemController>().makeList(allItems);
    }

    public int isInHotbar(Item item) // Check if item is in hotbar
    {
        if (item == hotbarItem1)
        {
            return 1;
        }
        else if (item == hotbarItem2)
        {
            return 2;
        }
        else if (item == hotbarItem3)
        {
            return 3;
        }
        return 0;
    }

    public GameObject getHandPos() // Return Hand Position
    {
        return handPosition;
    }

 // Check if the player has picked up all three ingredients and has them in thier inventory
    public bool checkIngredients()
    {
        Debug.Log("Checking inv");
        //has 0 in inventory.
        allThree =  0;

        //loop through inv
      foreach(Item i in allItems)
      {
          //2 being id of first ingrdient
          if(i.getID() == 2){
              //player has this in their inv
              allThree += 1;
          }
          //3 being id of second ingrdient
          if(i.getID() == 3){
              //player has this in their inv
              allThree += 1;
            }
           //4 being id of third ingrdient
          if(i.getID() == 4){
              //player has this in their inv
              allThree += 1;
            }
      }
      if(allThree == 3)
      {
        return true; 
      }

    return false; 
       
    }





}
