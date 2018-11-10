using UnityEngine;

public class Slider : MonoBehaviour
{

    public float minSwipeDistY;

    public float minSwipeDistX;

    private Vector2 startPos;

    [HideInInspector]
    public float swipeValue;

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
                            //animator.Play("Slider Left");
                            //leftDot.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
                            //rightDot.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                            //Invoke("ArrowRight", 0.35f);
                        }
                        else if (swipeValue < 0)//left swipe
                        {
                            //animator.Play("Slider Right");
                            //leftDot.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
                            //rightDot.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
                            //Invoke("ArrowLeft", 0.35f);
                        }
                    }
                    break;
            }
        }
    }
}