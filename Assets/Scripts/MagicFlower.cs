using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFlower : MonoBehaviour
{
    public bool hasAttacked;
    private Health health;
    private Damage damage;

    void Start()
    {
        hasAttacked = false;
        health = GetComponent<Health>();
        damage = GetComponentInChildren<Damage>();
    }

    public void nearDeathAttack(){
        health.setHealth((int)(health.maxHealth/3));
        damage.upgradePlant();
        StartCoroutine(waitForAttack());
    }

    IEnumerator waitForAttack() {
        yield return new WaitForSeconds(2);
        hasAttacked = true;
        damage.Disable();
        health.setHealth((int)(health.maxHealth/3));
    }
}
