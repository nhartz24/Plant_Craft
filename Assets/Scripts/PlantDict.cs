using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDict: MonoBehaviour
{

   // dictionary to hold all scriptable objects
   // NOTE: keys are NOT stored as just the scriptable object's plantName. 
   // (plantName is the "display name", with correct capitalization and spaces.)
   // Key is the plantName, but all lowercase and with underscores for the spaces. 
   // Prefab/object names should have spaces or underscores. Otherwise, they will not be tracked accurately.
   public Dictionary<string, Plant> SO_Plants = new Dictionary<string, Plant>();  

   public List<Plant> Plants = new List<Plant>();

   public List<string> keys = new List<string>();

   public GameObject stinkCloud; // these two are assigned in the editor!
   public GameObject poisonCloud;

   public GameObject upgradeGlow;

   public List<string> newNames = new List<string>();

   public List<Sprite> pics = new List<Sprite>(); // contains all possible sprites

   public List<GameObject> prefabs = new List<GameObject>(); // contains all possible prefabs

   [TextArea(5, 10)]
   public List<string> descriptions = new List<string>();

   // all the scriptable objects that were premade, aka "purebred" plants
   public List<Plant> allPremadePlants; // assigned in the editor


   public List<string> sceneNames = new List<string>();

   // strings need to match the Plant.plantName
   public string[] startingPlants = {"Lily of the Valley", "Onion", "Pea Plant"};


   void Start()
   {
      // add starting plants

      foreach (string name in startingPlants)
      {
         foreach (Plant plant in allPremadePlants)
         {
            if (name == plant.plantName) {
               Add(plant);
               Debug.Log("Added starting plant "+plant.plantName);
            }
         }
      }

   }

   public void Add(Plant plant) {
      string key = ConvertAnyNameToKey(plant.plantName);
      AddWithKey(key, plant);
   }



   public void AddWithKey(string key, Plant plant) {

      if (!SO_Plants.ContainsKey(key)){
         SO_Plants.Add(key, plant);
         Plants.Add(plant);
         }
      
   }

   // Converts any version of a plant name into its corresponding key, to look up in the dictionary.
   // Called "any name" because it could be the in-game object's name or the plant.plantName.
   public string ConvertAnyNameToKey(string plantName) {
      //Debug.Log(plantName);
      string key = plantName.Replace("(Clone)", "");  
      key = key.ToLower();
      key = key.Replace(" ", "_");
      return key;
   }

   // searches the dictionary for a plant using its object name, which can have spaces, uppercase, or (Clone)
   public Plant FindWithObjName(string objName) {
      string plantName = ConvertAnyNameToKey(objName);
      Plant plant = null;

      try {
         plant = SO_Plants[plantName];
      } catch (KeyNotFoundException ex) {
         Debug.Log("ERROR: "+ex);
         Debug.Log("Make sure the prefab/object is named in accordance with the scriptable object. The searched key was "+plantName+" and the object was named "+objName+
         ". It should match the plantName in the scriptable object, with spaces or underscores between words.");
      }

      return plant;
   }

}
