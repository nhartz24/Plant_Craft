using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// should be on the hitbox beneath the plant object

public class Damage : MonoBehaviour
{

    public GameObject enemy;

    private string attackTrait;
    public float attackSpeed; // should be higher number = more speed, so 3 is 3x as fast as 1
    public float damageAmount;
    private float localPowerLvl;

    private Plant plant;

    private PlantDict dict;

    private BoxCollider2D box;

    private bool isEnabled = true;

    public string enemyTag = "Enemy";

    private string plantName;
    public Transform target;
    public float range;
    public float attackCountdown = 1f;
    private bool contains = true;

    private GameObject upgradeGlow;

    // stinky/poison
    private GameObject stinkCloud = null;
    private GameObject poisonCloud = null;
    private float cloudTimer = 0f;
    public float cloudLingerTime = 3f;

    // carnivorous
    private Animator animator;
    private NutrientProduction nutrientProduction;

    private bool magic = true;


    // public Collider2D[] colls;

    void Start()
    {
        try {
            dict = GameObject.FindObjectOfType<PlantDict>().GetComponent<PlantDict>();
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Could not find the PlantDictObj and access its PlantDict component.");
        }

        Debug.Log("damage searching for plant: "+transform.parent.name);
        plant = dict.FindWithObjName(transform.parent.name);

        attackTrait = plant.attackTrait;
        localPowerLvl = plant.powerLvl;

        box = gameObject.GetComponent<BoxCollider2D>();

        if (box.isTrigger == false){
            Debug.Log("IsTrigger was not enabled for this plant's collider - enabling.");
            box.isTrigger = true;
        }

        upgradeGlow = dict.upgradeGlow;
        
        updatePlant();

        InvokeRepeating("UpdateTarget", 0f, 0.1f);

        if(plant.attackTrait == "Carnivorous") {
            GameObject plantObj = transform.parent.gameObject;
            if (plantObj.GetComponent<NutrientProduction>() == null) {
                plantObj.AddComponent<NutrientProduction>();
            }
            nutrientProduction = plantObj.GetComponent<NutrientProduction>();
            nutrientProduction.automated = false;
            animator = transform.parent.GetComponent<Animator>();

        } else if (plant.attackTrait == "Thorny") {
            animator = transform.parent.GetComponent<Animator>();

        } else if (plant.attackTrait == "Stinky") {
            stinkCloud = Instantiate(dict.stinkCloud, transform.parent);
            //float cloudSize = (box.size.x/2f) + 0.5f;
            stinkCloud.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            stinkCloud.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, 0);
            stinkCloud.SetActive(false);

        } else if (plant.attackTrait == "Poisonous") {
            poisonCloud = Instantiate(dict.poisonCloud, transform.parent);
            poisonCloud.SetActive(false);
            Vector3 poisonLoc = new Vector3(transform.position.x, transform.position.y+0.75f, 0);
            poisonCloud.transform.position = poisonLoc;
        }

    }


    void UpdateTarget (){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }


        if (nearestEnemy != null && shortestDistance <= range /1.25){
            target = nearestEnemy.transform;
            enemy = nearestEnemy;
        }
        else{
            target = null;
        }
    }

    void Update(){

        attackCountdown -= Time.deltaTime;
        if (poisonCloud != null || stinkCloud != null) {
            cloudTimer -= Time.deltaTime;
        }

        if (cloudTimer > 0f && poisonCloud != null && !poisonCloud.activeSelf) {
            poisonCloud.SetActive(true);
        }
        else if (cloudTimer > 0f && stinkCloud != null && !stinkCloud.activeSelf) {
            stinkCloud.SetActive(true);
        }
        
        else if(cloudTimer <= 0f && poisonCloud != null) {
            poisonCloud.SetActive(false);
        } 
        else if (cloudTimer <= 0f && stinkCloud != null) {
            stinkCloud.SetActive(false);
        }


        if (target == null){
            return;
        }
        else if (attackCountdown <= 0f){
            Attack();        
            
            if (contains == true){
                attackCountdown = (1/attackSpeed)*3;
                if (attackTrait != "Carnivorous"){ 
                    contains = false;
                }
            }        
        }
            

        attackCountdown -= Time.deltaTime;
    }


    IEnumerator carnivorousAttack() {
        try {
            animator.SetTrigger("isAttacking");
        } catch (NullReferenceException) {
            Debug.Log("Could not run animation on this carnivorous plant.");
        }
        yield return new WaitForSeconds(0.5f);
        if(enemy != null){
            damage(enemy.transform, damageAmount);
        }
    }


    void Attack()
    {
        if (attackTrait == "Poisonous")
        {

            contains = false;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(range, range), 1f);

            foreach (Collider2D collider in colliders)
            {
                if (enemy != null)
                {
                    if (collider.tag == "Enemy")
                    {
                        contains = true;

                        if (collider.gameObject.GetComponent<Health>().poison != true)
                        {
                            cloudTimer = cloudLingerTime;
                            collider.gameObject.GetComponent<Health>().Poisonous(attackSpeed, damageAmount, plant.powerLvl);
                        }
                    }
                }
            }
        }

        else if (attackTrait == "Carnivorous")
        {
            if (enemy != null){
                StartCoroutine(carnivorousAttack());
                nutrientProduction.Produce();
            }
            
        }
        else if (attackTrait == "Thorny")
        {
            contains = false;


            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(range, range), 0f);

            foreach (Collider2D collider in colliders)
            {
                if (enemy != null)
                {
                    if (collider.tag == "Enemy")
                    {
                        contains = true;
                        animator.SetTrigger("isAttacking");
                        damage(collider.transform, damageAmount);
                    }
                }
            }

        }
        else if (attackTrait == "Stinky") {
            contains = false;


            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(range, range), 0f);

            foreach (Collider2D collider in colliders)
            {
                if (enemy != null)
                {
                    if (collider.tag == "Enemy")
                    {
                        contains = true;
                        cloudTimer = cloudLingerTime;
                        damage(collider.transform, damageAmount);
                    }
                }
            }
        }
        else if (attackTrait == "Magic")
        {
            StartCoroutine(MagicAttack());

            
        }
    }

    IEnumerator MagicAttack(){

        if(magic == true){
            magic = false;

        List<Collider2D> enemies = new List<Collider2D>();

        contains = false;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(range, range), 0f);

            foreach (Collider2D collider in colliders)
            {
                    if (collider.tag == "Enemy")
                    {
                        enemies.Add(collider);  
                    }
            }

                for (int i = 0; i < enemies.Count; i++){

                    for (int j = i; j > 0; j--){

                        float uno = enemies[j].transform.position.x;
                        float dos = enemies[j - 1].transform.position.x;
                        

                        
                        if (uno > dos){
                            Collider2D largest = enemies[j];

                            enemies[j] = enemies[j-1];
                            enemies[j-1] = largest;

                        }
                    }

                }
                        foreach(Collider2D enemy in enemies){
                            if (enemy != null){
                                damage(enemy.transform, 100);
                                yield return new WaitForSeconds(0.3f);
                            }
                        }

                        // animator.SetTrigger("isAttacking");
                        contains = true;     
        }                   

    }

    void damage(Transform newEnemy, float damage){
        newEnemy.gameObject.GetComponent<Health>().TakeDamage(damage);
    }

    public void Disable(){
        damageAmount = 0f;
        attackSpeed = 0f;
        if (transform.parent.name == "magic_flower"){
                    box.size = Vector3.zero;
        }
        isEnabled = false;
        gameObject.GetComponent<Damage>().enabled = false;
    }


    public void updatePlant(){
        //Debug.Log("updating plant!");
        
        // size, damage, speed
        (Vector3, float, float) damageStats = PlantAttack.Calculate(plant, localPowerLvl);
        
        box.size = damageStats.Item1; 
        damageAmount = damageStats.Item2;
        attackSpeed = damageStats.Item3;

        range = box.size.x;
    }

    public void upgradePlant(){
        localPowerLvl = localPowerLvl + 1;
        Debug.Log("Plant's power level is now: "+localPowerLvl);
        if(plant.plantName != "Magic Flower") {
            StartCoroutine(UpgradeVisuals());
        }
        updatePlant();
    }

    IEnumerator UpgradeVisuals() {
        GameObject glow = Instantiate(upgradeGlow);
        Vector3 glowPos = new Vector3(transform.position.x, transform.position.y+0.1f, 0);
        glow.transform.position = glowPos;
        yield return new WaitForSeconds(1);
        Destroy(glow);
    }
}



public static class PlantAttack {
    
    public static (Vector3, float, float) Calculate(Plant plant, float powerLvl) {
        
        // find boxsize, damageAmount, attackSpeed based on powerlevel and trait
        Vector3 boxSize = Vector3.zero;
        float damageAmount = 0;
        float attackSpeed = 0;
        float size = 0;

        //Debug.Log(plant.attackTrait);

        switch (plant.attackTrait)
        {

            case "Stinky":
                // box size
                size = 2.2f + (powerLvl * 1.15f);
                boxSize = new Vector3(size,size,0);

                // damage amount
                damageAmount = powerLvl;

                // attack speed
                attackSpeed = 1.5f + powerLvl; //defaults 1.5
                break;

            case "Thorny":
                // box size
                size = 2.25f + (powerLvl/1.75f);
                boxSize = new Vector3(size,size,0);

                // damage amount
                damageAmount = 1f + powerLvl * 1.5f;

                // attack speed
                attackSpeed = 1f + powerLvl;
                break;

            case "Poisonous":
                // box size
                size = 1.75f + (powerLvl/2f);
                boxSize = new Vector3(size,size,0);

                // damage amount
                damageAmount = powerLvl *2;

                // attack speed
                attackSpeed = 1.5f + powerLvl; // basically irrelevant bc poison only attacks non-poisoned plants (once)
                break;

            case "Carnivorous":
                // box size
                size = 1.65f + (powerLvl/2f);
                boxSize = new Vector3(size,size,0);

                // damage amount
                damageAmount = powerLvl + 100;

                // attack speed
                attackSpeed = powerLvl / 2.5f;
                break;
            // case "Stinky":
            //     // box size
            //     size = 3f + (powerLvl * 1.5f); //defaults * 3f
            //     boxSize = new Vector3(size,size,0);

            //     // damage amount
            //     damageAmount = powerLvl * 1.5F;

            //     // attack speed
            //     attackSpeed = 1f + (powerLvl * 0.1f); //defaults 1.5
            //     break;

            // case "Thorny":
            //     // box size
            //     size = 2f + powerLvl;
            //     boxSize = new Vector3(size,size,0);

            //     // damage amount
            //     damageAmount = 2f + (powerLvl * 2f);

            //     // attack speed
            //     attackSpeed = 1f + powerLvl;
            //     break;

            // case "Poisonous":
            //     // box size
            //     size = 2f + powerLvl;
            //     boxSize = new Vector3(size,size,0);

            //     // damage amount
            //     damageAmount = 1f + (powerLvl * 2f);

            //     // attack speed
            //     attackSpeed = 3f + powerLvl; // basically irrelevant bc poison only attacks non-poisoned plants (once)
            //     break;

            // case "Carnivorous":
            //     // box size
            //     size = 2f + (powerLvl * 2f);
            //     boxSize = new Vector3(size,size,0);

            //     // damage amount
            //     damageAmount = 5f + (powerLvl * 5f);

            //     // attack speed
            //     attackSpeed = powerLvl;
            //     break;

            case "Magic":
                // box size
                size = (powerLvl-1) * 30f;
                boxSize = new Vector3(size,size,0);

                // damage amount
                damageAmount = (powerLvl-1) * 30f;

                // attack speed
                attackSpeed = (powerLvl-1) * 1f;
                break;

            case "None": // keep no damage, 0 box size, no speed
                break;

            default:
                Debug.Log("ERROR: AttackTrait on this plant is not a valid string. Damage modifiers will not be applied, and all are set to 0.");
                break;
        }

        Debug.Log("PLANT ATTACK: This plant, "+plant.plantName+", with attack trait "+plant.attackTrait+", has the power level "+powerLvl+", and so its box size is "+boxSize+", its damage is "+damageAmount+", and its speed is "+attackSpeed+".");

        return (boxSize,damageAmount,attackSpeed);

    }
    
}

