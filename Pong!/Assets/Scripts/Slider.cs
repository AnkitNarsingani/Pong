using UnityEngine;

public class Slider : MonoBehaviour
{
    [HideInInspector]
    public float minSwipeDistY = 100;

    [HideInInspector]
    public float minSwipeDistX = 100;

    private Vector2 startPos;

    [HideInInspector]
    public float swipeValue;

    int index = 0;

    bool rightSwipe = false, leftSwipe = false;

    private RectTransform rectTransform;

    float minMoveBounds;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Input.touchCount > 0 && !rightSwipe && !leftSwipe)
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
                            SetBounds(false);
                            rightSwipe = true;
                        }
                        else if (swipeValue < 0)//left swipe
                        {
                            SetBounds(true);
                            leftSwipe = true;
                        }
                    }
                    break;
            }
        }

        if(rightSwipe)
        {
            if (index >= -2 && index <= 1 && rectTransform.localPosition.x < minMoveBounds)
                rectTransform.localPosition += new Vector3(1000 * Time.deltaTime, 0, 0);
            else
                rightSwipe = false;
        }
        else if(leftSwipe)
        {
            if (index >= -2 && index <= 1 && rectTransform.localPosition.x > minMoveBounds)
                rectTransform.localPosition -= new Vector3(1000 * Time.deltaTime, 0, 0);
            else
                leftSwipe = false;
        }
    }

    void SetBounds(bool increment)
    {
        if (increment)
            index++;
        else
            index--;

        switch(index)
        {
            case 2:
                index = 1;
                break;
            case 1:
                minMoveBounds = -3260f;
                break;
            case 0:
                minMoveBounds = -1887f;
                break;
            case -1:
                minMoveBounds = -513f;
                break;
            case -2:
                minMoveBounds = 850f;
                break;
            case -3:
                index = -2;
                break;
        }
    }
}