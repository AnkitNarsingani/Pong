using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
    [HideInInspector]
    public float minSwipeDistY = 100;

    [HideInInspector]
    public float minSwipeDistX = 100;

    private Vector2 startPos;

    [HideInInspector]
    public float swipeValue;

    [SerializeField]
    public int index = 2;

    [SerializeField]
    int minValue = 0, maxValue = 3;

    [SerializeField]
    MenuDots menuDots;

    public float[] postions;

    RectTransform rt;

    bool canMove = false;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rt.localPosition = new Vector3(Mathf.Lerp(rt.localPosition.x, postions[index], Time.deltaTime * 10), rt.localPosition.y, 0);
            if (Mathf.Abs(rt.localPosition.x - postions[index]) < 1)
                canMove = false;
        }
    }
    void Update()
    {
        if (Input.touchCount > 0 && Time.timeSinceLevelLoad > 0.5f)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:

                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                    if (swipeDistHorizontal > minSwipeDistX)
                    {
                        swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        if (swipeValue > 0)//right swipe
                        {
                            if (index != minValue)
                            {
                                index--;
                                if (menuDots != null)
                                    menuDots.ChangeDots(index);
                                canMove = true;
                            }
                        }
                        else if (swipeValue < 0)//left swipe
                        {
                            if (index != maxValue)
                            {
                                index++;
                                if (menuDots != null)
                                    menuDots.ChangeDots(index);
                                canMove = true;
                            }
                        }
                    }
                    break;
            }  
        }
    }
}