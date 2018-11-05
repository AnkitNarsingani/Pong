using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIScript : MonoBehaviour
{

    [HideInInspector]
    public GameObject ball;

    public float moveSpeed = 5f;
    float colorWaitTime = 0.5f;

    SpriteRenderer sr;

    [HideInInspector]
    public Color currentColor;
    private int count = 0;

    private bool canMove = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AIMove();
    }

    void AIMove()
    {
        if (ball != null)
        {
            if (GameManager.Instance.goingUp && canMove)
            {
                //Going Left
                if (transform.localPosition.x > ball.transform.localPosition.x && transform.localPosition.x > -1.68f)
                    transform.localPosition += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
                    
                //Going Right
                if (transform.localPosition.x < ball.transform.localPosition.x && transform.localPosition.x < 1.95f)
                    transform.localPosition += new Vector3(moveSpeed * Time.deltaTime, 0, 0);     
            }
            else if (!GameManager.Instance.goingUp)
            {
                canMove = false; 
            }
        }
        else
        {
            Debug.Log("Cannot find ball --> AI");
        }
    }

    public void StartColorChange()
    {
        StartCoroutine(Changecolor());
    }

    IEnumerator Changecolor()
    {
        count++;
        switch (count)
        {
            case 1:
                sr.color = GameManager.Instance.red;
                break;
            case 2:
                sr.color = GameManager.Instance.green;
                break;
            case 3:
                count = 0;
                sr.color = GameManager.Instance.blue;
                break;
        }

        if (GameManager.Instance.rally > SceneManager.GetActiveScene().buildIndex + 6)
            moveSpeed = 3.5f;

        if (sr.color == currentColor && GameManager.Instance.goingUp)
            yield return new WaitForEndOfFrame();
        else
            yield return new WaitForSeconds(colorWaitTime);
            
        if (sr.color != currentColor && GameManager.Instance.goingUp)
            StartCoroutine(Changecolor());
        else
            canMove = true;            
    }
}
