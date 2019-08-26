using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFitVertical : MonoBehaviour
{
    public SpriteRenderer rink;

    // Use this for initialization
    void Start()
    {
        float spriteLength = rink.bounds.size.x;
        float screenLength = Screen.width;
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0)));
        //GetComponent<Camera>().
        //Camera.main.GetComponent<CameraFollow>().leftCameraBound = -(len / 2);
        //Camera.main.GetComponent<CameraFollow>().rightCameraBound = (len / 2);
        //Camera.main.orthographicSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;
    }
}