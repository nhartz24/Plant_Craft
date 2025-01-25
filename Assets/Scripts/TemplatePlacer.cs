using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]

public class TemplatePlacer : MonoBehaviour {

    [SerializeField]
    private GameObject finalObject; // needs to be filled in the editor!

    private LayerMask placeableTileLayer; 
    // (note that the placeable tilemap on this layer needs to have a tilemapcollider)
    
    private LayerMask objectLayer;  

    private SoundManager soundManager; //filled by script, connects to the master in all scen

    public SupplySlot slot; 

    private Vector2 mousePos;
    private GameObject gameObjectHit;

    private GameObject newFinalObject;

    private Damage damage;

    void Start()
    {
        if(slot == null) { Debug.Log("SupplySlot was not properly assigned to this template, likely in the Spawner.");}
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1,1,1,0.5f);
        
        // set layers        
        // NameToLayer gives the layer number, not the layerMask, so needs to be converted
        int ptLayer = LayerMask.NameToLayer("PlaceableTile");
        int ptLayerMask = (1 << ptLayer);
        //Debug.Log("PlaceableTileLayer: "+ptLayerMask); // if 8, should be 256
        placeableTileLayer = ptLayerMask;

        int objLayer = LayerMask.NameToLayer("Plants");
        int objLayerMask = (1 << objLayer);
        //Debug.Log("objectlayer: "+objLayerMask); // if 9, should be 512
        objectLayer = objLayerMask;

        //judes stuff i hope it is not messy it is only nine line and i am literally one years old
        try
        {
            GameObject.Find("SoundManagerMain").GetComponent<SoundManager>().Play("plant_select");

        }
        catch (NullReferenceException ex)
        {
            Debug.Log("ERROR: " + ex);
            Debug.Log("Main Menu Sound Manager not found. Not a problem if not running from main menu");
        }
    }

    void Update ()
    {        
        // checking for missing connections in editor
        //i might be wrong but could these be moved outside of update?
        if(finalObject == null) { 
            Debug.Log("This TemplateScript object has not been assigned a final object to turn into!");
        }
        if(placeableTileLayer.value == 0) { 
            Debug.Log("This TemplateScript object has not been assigned a tile layer!");
        }
        if(objectLayer.value == 0) { 
            Debug.Log("This TemplateScript object has not been assigned an object layer!");
        }


        // if mouse is outside of the window, don't do anything
        Vector3 mouse = Input.mousePosition; // in relation to screen, not world
        if(mouse.x < 0 || mouse.y < 0 || mouse.x > Screen.width || mouse.y > Screen.height) {
            return;
        }


        // check to place objects
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // update position for tile matching
        //Sprite plantSprite = GetComponent<SpriteRenderer>().sprite;
        
        Sprite plantSprite = finalObject.GetComponent<SpriteRenderer>().sprite;
        
        //Debug.Log("plantSprite: "+plantSprite);

        float plantHeight = plantSprite.bounds.size.y;
        

        if(plantHeight > 1) {
            transform.position = new Vector2((Mathf.Floor(mousePos.x)+0.5f), (Mathf.Floor(mousePos.y)+1f)); // matches with tiles
        }
        else {
            transform.position = new Vector2((Mathf.Floor(mousePos.x)+0.5f), (Mathf.Floor(mousePos.y)+0.5f)); // added 0.5 to rounded down val so the center is at (0.5, 0.5), matches with tiles
        }

        if (Input.GetMouseButtonDown(0)) // mouse left click
        {
            //Debug.Log("clicked");

            //soundManager.Play("plant_select");
            Vector3 raycastPoint;
            if(plantHeight > 1) {
                raycastPoint = new Vector3 (transform.position.x, transform.position.y-0.5f, 0f);
            } else {
                raycastPoint = transform.position;
            }
            RaycastHit2D rayHitTile = Physics2D.Raycast(raycastPoint, Vector2.zero, Mathf.Infinity, placeableTileLayer);

            if (rayHitTile.collider != null) // over a placeable tile
            {
                gameObjectHit = rayHitTile.transform.gameObject;
                RaycastHit2D rayHitSameObj = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, objectLayer);
                if (rayHitSameObj.collider == null) // AND nothing already placed here
                {
                    Debug.Log("placing");

                    //judes stuff i hope it is not messy it is only one line and i am literally one years old
                    try
                    {
                        GameObject.Find("SoundManagerMain").GetComponent<SoundManager>().Play("plant_plant");
                        
                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.Log("ERROR: " + ex);
                        Debug.Log("Main Menu Sound Manager not found. Not a problem if not running from main menu");
                    }
                    

                    newFinalObject = Instantiate(finalObject, transform.position, Quaternion.identity);
                    newFinalObject.GetComponentInChildren<Damage>().gameObject.layer = LayerMask.NameToLayer("Hitbox");
                    newFinalObject.layer = LayerMask.NameToLayer("Plants");
                    //slot.isPlaced();
                    Destroy(gameObject);


                    string name = newFinalObject.name.ToLower();
                    name = name.Replace(" ", "_");
                    name = name.Replace("(clone)","");

                    string biome = GameObject.Find("Master/PlantDictObj").GetComponent<PlantDict>().SO_Plants[name].biomeTrait;

                    if((TutorialManager.tutorialStage == 2 || TutorialManager.tutorialStage == 3) && SceneManager.GetActiveScene().name == "TutorialLevel") 
                    //for the tutorial level, shows dialogue depending on name. 2 is the first plant placed, 3 any after
                    {
                        Debug.Log("THE PLANTS NAME IS " + name);
/*                        if (TutorialManager.tutorialStage == 3)
                        {

                            Debug.Log("PLACING TUTORIAL FINISHED??");
                            FindObjectOfType<TutorialManager>().startTutorialLevel();
                        }*/
                        
                        if(name == "pea_plant")
                        {
                            FindObjectOfType<TutorialManager>().peaPlantTutorial();
                        }
                        if(name == "onion")
                        {
                            FindObjectOfType<TutorialManager>().onionTutorial();
                        }
                        if(name == "lily_of_the_valley")
                        {
                            FindObjectOfType<TutorialManager>().lilyTutorial();
                        }
                            
                    }

                    if(gameObjectHit.tag != biome){
                        if(SceneManager.GetActiveScene().name == "TutorialLevel") {
                            GameObject tutorialManager = GameObject.Find("TutorialManager");
                            tutorialManager.GetComponent<TutorialManager>().Warning();
                        }

                        newFinalObject.GetComponent<PlantController>().disableNutrients();
                        damage = newFinalObject.transform.GetComponentInChildren<Damage>();
                        damage.Disable();

                        //string tag = gameObjectHit.tag;
                        //tag is biome it is trying to place in (DEPREDCATED
                        if (biome == "Desert") //so if the plant is made for the desert but not being placed in the desert
                        {
                            GameObject.Find("DesertPlantWarning").GetComponent<DialogueTrigger>().StartDialogue();
                        }
                        else if (biome == "Swamp")
                        {
                            GameObject.Find("SwampPlantWarning").GetComponent<DialogueTrigger>().StartDialogue();
                        }
                        else if (biome == "Forest")
                        {
                            GameObject.Find("ForestPlantWarning").GetComponent<DialogueTrigger>().StartDialogue();
                        }
                        else {
                            DialogueTrigger trigger = GameObject.Find("WarningDialogue").GetComponent<DialogueTrigger>(); //HAS TO BE CALLED WARNING DIALOGUE DONT CHANGE NAMES DONT PASS GOOOOO
                            trigger.StartDialogue();
                        }

                        // tutorialManager.GetComponent<TutorialManager>().Warning();

                        
                        //Need pause Game
                        //Stop from clicking while dialogue is active

                    }
                }
            }
            
        }
	}
}