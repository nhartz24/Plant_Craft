using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controls the inventory and what appears and does not appear
//Attached to slot panel
//you want to add a button to the slot panel and onClick it calls InvetoryManager.Remove

public class SlotController : MonoBehaviour
{
    private ItemClass item;                 // scriptable object
    private GameObject weapon;              // physcial representation in game
    public GameObject inventory;            // inventory UI in game 

    [SerializeField] Image image;           //part of UI, changes to the pic assciated
                                            //with whatver object is picked up 

    // public GameObject slot;
    private InventoryManager reference;
    public bool isFull = false;
    // Start is called before the first frame update
    void Start()
    {
        reference = FindObjectOfType<InventoryManager>();   //this finds the first object in the heirarchy
                                                           //with the inventory manager script attached,
                                                           //will not work if there is more than one 
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void setItem(ItemClass newItem, GameObject newWeapon)
        {
            //if the inventory is already full and we are picking up an item 
            //we drop the item that is in the invetory
            if(isFull){

                removeItem();
            }

            Debug.Log("setItem");

                inventory.SetActive(true);          //makes inventory visible if it is hidden
                image.sprite = newItem.itemIcon;    //changes picture to show up in inventory
                image.enabled = true;               //makes the layer that holds the pic visible

                weapon = newWeapon;     //the physical and scriptable object associated with what  
                                        //the inventory is holding change 

                item = newItem;

                isFull = true;      // inventory is now full and can not pick uo anythign 
                                    // else without dropping somehting first 
        }


    public void removeItem()
        {
            Debug.Log("");
            if(isFull){
                // inventory.SetActive(false);
                weapon.transform.parent = null;     //removes the item as a child of the player so it
                                                    // will drop to the floor and not reamin attached

                image.enabled = false;             //the ui element displying the image is turned off

                isFull = false;                    // the invenotry can now accept more items
            }
        }


}
