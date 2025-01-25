using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMove : MonoBehaviour
{
    public GameObject Jude;

    public GameObject Min;

    public GameObject Will;

    public GameObject Luca;

    public GameObject Noah;

    void Update()
    {

        Jude.transform.position = new Vector3(Jude.transform.position.x,Jude.transform.position.y+2f, 0);

        Min.transform.position = new Vector3(Min.transform.position.x,Min.transform.position.y+2f, 0);

        Will.transform.position = new Vector3(Will.transform.position.x,Will.transform.position.y+2f, 0);

        Luca.transform.position = new Vector3(Luca.transform.position.x,Luca.transform.position.y+2f, 0);

        Noah.transform.position = new Vector3(Noah.transform.position.x,Noah.transform.position.y+2f, 0);



        
    }
}
