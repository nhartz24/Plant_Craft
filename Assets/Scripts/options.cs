using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class options : MonoBehaviour
{
    public Transform voiceOnButton;
    public Transform voiceOffButton;
    static public bool voiceOver = false;

    // Start is called before the first frame update
    public void voiceOverOff()
    {
        voiceOver = false;
        voiceOnButton.gameObject.SetActive(false);

        voiceOffButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void voiceOverOn()
    {
        voiceOver = true;
        voiceOffButton.gameObject.SetActive(false);

        voiceOnButton.gameObject.SetActive(true);
    }
}
