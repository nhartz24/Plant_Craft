using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    RectTransform contentRect;

    // tutorial is currently 580, not 450
    public static int[] heights = {450, 580, 880, 880, 1400, 1800};

    // Start is called before the first frame update
    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        
        int contentHeight = getHeight();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

        Debug.Log("SceneSwitch: "+SceneSwitch.gameLevel+", and so height is "+contentHeight);

    }

    public static int getHeight() {
        int index = SceneSwitch.gameLevel;
        if(index>5) {
            index = 5;
        }

        return heights[index];
    }
}
