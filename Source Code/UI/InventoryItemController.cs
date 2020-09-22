using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour // by JB2051
{
    public GameObject grid;
    public GameObject ExampleInventoryItem;
    public Text text;
    public Text hotbarNumberText;
    public GameObject playItem;
    public GameObject viewItem;
    private List<GameObject> currentList;
    public GameObject inventory;
    private Item selectedItem;
    public GameObject inventoryScript;
    public Item selectedItemActive;
    public GameObject InformationPanel;
    public Text quantityText;
    
    

    void Start()
    {
        currentList = new List<GameObject>();
        inventory.SetActive(false);
        playItem.SetActive(false);
        viewItem.SetActive(false);
        selectedItem = null;
        
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (inventoryScript.GetComponent<Inventory>().isInHotbar(selectedItemActive) == 0)
            {
                inventoryScript.GetComponent<Inventory>().addItemHotbar1(selectedItemActive);
                inventoryScript.GetComponent<Inventory>().refreshList();
                selectedItem = selectedItemActive;
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (inventoryScript.GetComponent<Inventory>().isInHotbar(selectedItemActive) == 0)
            {
                inventoryScript.GetComponent<Inventory>().addItemHotbar2(selectedItemActive);
                inventoryScript.GetComponent<Inventory>().refreshList();
            }
        }
        if (Input.GetKeyDown("3"))
        {
            if (inventoryScript.GetComponent<Inventory>().isInHotbar(selectedItemActive) == 0)
            {
                inventoryScript.GetComponent<Inventory>().addItemHotbar3(selectedItemActive);
                inventoryScript.GetComponent<Inventory>().refreshList();
            }
        }
        if (selectedItem != null)
        {
            if (selectedItem.isPlayable())
            {
                playItem.SetActive(true);
                viewItem.SetActive(false);
                playItem.GetComponentInChildren<VideoStreamerScript>().GiveURL(selectedItem.getURL());
                playItem.GetComponentInChildren<VideoStreamerScript>().PlayClip();
                selectedItemActive = selectedItem;
                selectedItem = null;
            }
            else
            {
                viewItem.SetActive(true);
                playItem.SetActive(false);
                viewItem.GetComponentInChildren<Text>().text = selectedItem.getAcompanyingText();
                selectedItemActive = selectedItem;
                selectedItem = null;
            }
            InformationPanel.transform.GetChild(0).GetComponent<Image>().sprite = selectedItemActive.getImage();
            InformationPanel.transform.GetChild(1).GetComponent<Text>().text = selectedItemActive.getName();
            InformationPanel.transform.GetChild(2).GetComponent<Text>().text = selectedItemActive.getDescription();
        }
        if(selectedItemActive == null)
        {
            InformationPanel.SetActive(false);
        }
        else
        {
            InformationPanel.SetActive(true);
        }
    }

    private GameObject createItem(Item item)
    {
        GameObject TempObject;
        text.text = item.getName();
        quantityText.text = "x"+item.getQuantity();
        if(inventoryScript.GetComponent<Inventory>().isInHotbar(item) != 0)
        {
            hotbarNumberText.text = inventoryScript.GetComponent<Inventory>().isInHotbar(item).ToString();
        }
        else
        {
            hotbarNumberText.text = "";
        }
        TempObject = Instantiate(ExampleInventoryItem) as GameObject;
        TempObject.transform.parent = grid.transform;
        TempObject.transform.localScale = new Vector3(1f,1f,1f);
        TempObject.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(item));
        TempObject.SetActive(true);
        return TempObject;
    }

    public void makeList(List<Item> items)
    {
        deleteList();
        //loop through all items
        for (int i = 0; i < items.Count; ++i)
        {
            Debug.Log(items[i].getName());
            //add item to list 
            currentList.Add(createItem(items[i]));
        }
    }

    public void deleteList()
    {
        try
        {
            //loop through current items 
            for (int i = 0; i < currentList.Count; ++i)
            {
                Destroy(currentList[i]);
            }

            currentList.Clear();
            playItem.GetComponentInChildren<VideoStreamerScript>().StopPlay();
            selectedItemActive = null;
            playItem.SetActive(false);
            viewItem.SetActive(false);
        }
        catch { }
    }

    void ButtonClicked(Item item)
    {
        this.selectedItem = item;
    }


   

}
