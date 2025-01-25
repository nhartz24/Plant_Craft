using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FieldOfVision : MonoBehaviour
{

    private BoxCollider2D hitbox;
    private float hitboxX;
    private float hitboxY;
    // Start is called before the first frame update
    void Start()
    {

        
        // Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        // Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);
        
    }

    public void Show(){
        gameObject.SetActive(true);

        hitbox = gameObject.transform.parent.transform.GetComponentInChildren<Damage>().gameObject.GetComponent<BoxCollider2D>();
        hitboxX = hitbox.size.x;
        hitboxY = hitbox.size.y;

        gameObject.transform.localScale = new Vector3(hitboxX, hitboxY, 1);

    }


    public void DontShow(){
        gameObject.SetActive(false);
    }
}
