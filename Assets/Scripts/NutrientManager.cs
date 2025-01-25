using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NutrientManager : MonoBehaviour
{
    public int startingValue = 100;
    public int nutrients;

    public bool canBuy;

    public DamagePopup nutrientPopupPrefab; // popup prefab actually stored here; this way you can copy to nutrientProduction component at runtime

    private TMP_Text textBox;
    private SupplyManager supplyManager;
    
    // Start is called before the first frame update
    void Start()
    {
        nutrients = startingValue;
        canBuy = true;

        supplyManager = GetComponentInParent<SupplyManager>();

        textBox = GetComponent<TMP_Text>();
        textBox.text = nutrients.ToString();

        if(SceneManager.GetActiveScene().name != "TutorialLevel"){
            InvokeRepeating("UpdateNutrients", 0f, 4f);
        }

        
    }

    public void UpdateNutrientsTutorial(){

        InvokeRepeating("UpdateNutrients", 0f, 4f);

    }

    void UpdateNutrients(){

        Add(1);

    }

    // Update is called once per frame
    void Update()
    {
        if(nutrients <= 0){
            nutrients = 0;
            canBuy = false; 
        } else {
            canBuy = true;
        }
    }

    public void Subtract(int num){
        nutrients -= num;
        textBox.text = nutrients.ToString();
        Debug.Log("Subtracted; nutrients is now "+nutrients);
    }

    public void Add(int num) {
        nutrients += num;
        textBox.text = nutrients.ToString();

        supplyManager.UpdateAvailableSupply();

        Debug.Log("Added; nutrients is now "+nutrients);
    }
}
