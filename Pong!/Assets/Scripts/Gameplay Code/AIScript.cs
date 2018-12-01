using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIScript : MonoBehaviour
{

    [HideInInspector]
    public GameObject ball;

    public float moveSpeed;
    float colorWaitTime = 0.5f;

    SpriteRenderer sr;

    [HideInInspector]
    public Color currentColor;
    private int count = 0;

    private bool canMove = false;

    float decreasedSpeed, increasedcolorWaitTime;

    int rallytoWait;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GameManager.Instance.Reference();
        decreasedSpeed = moveSpeed - 1;
        increasedcolorWaitTime = colorWaitTime + 0.15f;
        rallytoWait = GetRallyWait();
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
                if (transform.localPosition.x > ball.transform.localPosition.x && transform.localPosition.x > -1.95f)
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
                sr.color = GameManager.Instance.firstColor;
                break;
            case 2:
                sr.color = GameManager.Instance.secondColor;
                break;
            case 3:
                count = 0;
                sr.color = GameManager.Instance.thirdColor;
                break;
        }

        if (GameManager.Instance.rally > (rallytoWait + 4))
        {
            moveSpeed = decreasedSpeed;
            colorWaitTime = increasedcolorWaitTime;
        }

        if (sr.color == currentColor && GameManager.Instance.goingUp)
            yield return new WaitForEndOfFrame();
        else
            yield return new WaitForSeconds(colorWaitTime);
            
        if (sr.color != currentColor && GameManager.Instance.goingUp)
            StartCoroutine(Changecolor());
        else
            canMove = true;            
    }

    int GetRallyWait()
    {
        switch (PlayerPrefs.GetInt("currentLevel", 17))
        {
            case 17:
                return 1;
            case 16:
                return 2;
            case 15:
                return 3;
            case 14:
                return 4;
            case 13:
                return 5;
            case 12:
                return 6;
            case 11:
                return 7;
            case 10:
                return 8;
            case 9:
                return 9;
            case 8:
                return 10;
            case 7:
                return 11;
            case 6:
                return 12;
            case 5:
                return 13;
            case 4:
                return 14;
            case 3:
                return 15;
            case 2:
                return 16;
            case 1:
                return 17;
            default:
                return 2;
        }
    }
}
