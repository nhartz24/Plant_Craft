using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]

public class Health : MonoBehaviour
{
    public float health, maxHealth = 10f;

    public DamagePopup damagePopup;

    private PlantDict plantDict;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyMovement movement;

    private GameObject poisonEffect = null;

    private HealthBar healthBar = null;

    public bool poison = false;

    private void Start(){
        plantDict = GameObject.FindObjectOfType<PlantDict>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        health = maxHealth;

        if(gameObject.name == "magic_flower") {
            healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.setMaxHealth((int)maxHealth);
        } else
        {
            movement = GetComponent<EnemyMovement>();
        }
    }


    public void TakeDamage(float damageAmount){
        health -= damageAmount;
        if(gameObject.name != "magic_flower"){
            StartCoroutine(DamageColor());
        }

        if(healthBar != null) {
            healthBar.setHealth((int)health);
        }
        Debug.Log(gameObject.name);

        float xPos = gameObject.transform.position.x;
        float yPos = gameObject.transform.position.y;

        damagePopup.Create(new Vector3(xPos, yPos, 0) , damageAmount);


        Debug.Log(gameObject.name+": "+health);

        if(health <= 0 && gameObject.name != "magic_flower"){
            StartCoroutine(EnemyDies());
        }

    }

    IEnumerator DamageColor(){
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 225);

        yield return new WaitForSeconds(0.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

    }

    IEnumerator EnemyDies() {
        animator.SetBool("isDying", true);
        movement.Stop();
        Debug.Log("waiting second...");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("second up!");
        Destroy(gameObject);
    }

    public void Poisonous(float attackSpeed, float damageAmount, float powerLvl){
        poison = true;
        poisonEffect = Instantiate(plantDict.poisonCloud, transform);
        Vector3 poisonLoc = new Vector3(transform.position.x, transform.position.y+0.35f, 0);
        poisonEffect.transform.position = poisonLoc;
        StartCoroutine(Poison(attackSpeed, damageAmount, powerLvl));
    }

    IEnumerator Poison(float attackSpeed, float damageAmount, float powerLvl){
        int maxTimesAttack = (int)powerLvl;
        
        for(int i = 0; i <= maxTimesAttack; i++){
            health -= damageAmount;

            float xPos = gameObject.transform.position.x;
            float yPos = gameObject.transform.position.y;

            damagePopup.Create(new Vector3(xPos, yPos, 0) , damageAmount);

            if(health <= 0){
                Destroy(gameObject);
            }
            if (i<maxTimesAttack) {
                yield return new WaitForSeconds((1/attackSpeed) *3); 
            }
        }

        poison = false;
        Destroy(poisonEffect);
    }

    public void setHealth(int num) {
        health = (float)num;
        if(gameObject.name == "magic_flower") {
            healthBar.setHealth(num);
        }
    }
}