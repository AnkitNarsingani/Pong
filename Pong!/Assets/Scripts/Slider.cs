using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
    [HideInInspector]
    public float minSwipeDistY = 75;

    [HideInInspector]
    public float minSwipeDistX = 75;

    private Vector2 startPos;

    [HideInInspector]
    public float swipeValue;

    [HideInInspector]
    public int index = 2;

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
        if (Input.touchCount > 0)
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
                            if (index != 0)
                            {
                                index--;
                                menuDots.ChangeDots(index);
                                canMove = true;
                            }
                        }
                        else if (swipeValue < 0)//left swipe
                        {
                            if (index != 3)
                            {
                                index++;
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