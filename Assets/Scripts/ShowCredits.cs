using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits: MonoBehaviour
{

    public GameObject creditsScreen;
    // Start is called before the first frame update
    public void ShowScreen()
    {
        Instantiate(creditsScreen);
    }

    public void CloseScreen()
    {
        Destroy(creditsScreen);
    }

}
