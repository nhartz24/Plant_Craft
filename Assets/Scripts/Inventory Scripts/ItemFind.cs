using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// pics up item and passes info to IventoryManager
// attached to physical item in game


public class ItemFind : MonoBehaviour
{

    [SerializeField] ItemClass item;        //scriptable object 
    public GameObject HideyBoi;             // player
    public GameObject weapon;               //actual in game object

    private InventoryManager reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trig){
        if (trig.gameObject == HideyBoi){
            Debug.Log("trig");
            reference.Add(item, weapon);                        //calls add in Invetory Manager sctipt
            weapon.transform.parent = HideyBoi.transform;       //makes the player a parent of the object
                                                                // so that it stays with the player
            weapon.transform.localPosition = new Vector3(.09f,-.03f,0); // where the item is located in relation 
            // weapon.GetComponent<SpriteRenderer>().enabled = false;      to the player when pciked up
        }
    }


    
}
