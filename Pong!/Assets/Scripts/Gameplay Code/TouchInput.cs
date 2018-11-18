using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    public LayerMask touchInputMask;
    public LayerMask uIMask;
    LayerMask resultMask;
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchOld;
    private RaycastHit hit;

    void Start()
    {
        resultMask = touchInputMask + uIMask;
    }

    void Update ()
    {
        
        if (Input.touchCount > 0)
        {

            touchOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, 10000, resultMask))
                {
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Stationary)
                    {

                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchMoved", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                }
            }

            foreach (GameObject g in touchOld)
            {
                if (!touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }
}
