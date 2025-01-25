using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//The inventory just takes the input and determines what 
//weapon is being picked up and what scriptible object that goes with and 
//passses that info to other scripts that will actullay do stuff with that info
//attached to empty inventory object

public class InventoryManager : MonoBehaviour
{

    // public GameObject HideyBoi;
    [SerializeField] private SlotController slot; //these are all variables that point to different scripts so 
    // public DeathButtonAppear button;              //so we can call different methods in those classes
    // public Animator animator;
    // public GameObject weapon;

    private InventoryManager reference;


    public GameObject item;


    public void Start()
    {
        reference = FindObjectOfType<InventoryManager>();  //this finds teh first object in the heirarchy
                                                           //with the inventory manager script attached,
                                                           //will not work if there is more than one 

    }

    public void Add(ItemClass item, GameObject weapon)
    {
        Debug.Log("Add");
        slot.setItem(item, weapon);             //calls setItem in SlotController script and passes
        // button.isTrue();                       the scriptable object as well as the actual visual
        // animator.SetBool("gotKnife", true);    item in the game 
    }

    public void Remove()
    {
        slot.removeItem();          // calls removeItem in SlotController script
        // button.isFalse();
        //this.GetComponent<ItemUse>();
        //this.GetComponent<ItemUse>().showWeapon();
    }

}