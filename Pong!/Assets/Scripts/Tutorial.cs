using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    bool colorDisplay = true, goingLeft = true, canTime = true;

    float timer = 0f;

    [SerializeField]
    string seet1 = "Change Color";

    [SerializeField]
    Image fingerColor, player, paddle;

    [SerializeField]
    Text t1, t2, tapText;

    [SerializeField]
    Color blue;

    [SerializeField]
    string set2 = "Movement";

    [SerializeField]
    Image fingerMovement;

    [SerializeField]
    Text t3, t4;

    Color red;
    
    void Start()
    {
        red = player.color;
    }

    
    void Update()
    {
        if(canTime)
            timer += Time.deltaTime;

        if(colorDisplay && timer > 2)
        {
            paddle.color = blue;
            player.color = blue;
            tapText.gameObject.SetActive(false);
            timer = 0;
        }
        else if(!colorDisplay)
        {
            paddle.color = red;
            player.color = red;
            if(player.rectTransform.localPosition.x > -665f && goingLeft)
            {
                player.rectTransform.localPosition += new Vector3(-5f, 0, 0);
            }
            else
            {
                goingLeft = false;
            }
            if (player.rectTransform.localPosition.x < -310f && !goingLeft)
            {
                player.rectTransform.localPosition -= new Vector3(-5f, 0, 0);
            }
            else
                goingLeft = true;
                
        }
    }
    public void StartCoroutines()
    {
        StartCoroutine(ColorAnimation());
    }
    public IEnumerator ColorAnimation()
    {
        ChangeDisplay();
        yield return new WaitForSeconds(4);
        ChangeDisplay();
        yield return new WaitForSeconds(4);
        StartCoroutine(ColorAnimation());
    }

    void ChangeDisplay()
    {
        colorDisplay = !colorDisplay;

        if(colorDisplay)
        {
            fingerMovement.gameObject.SetActive(false);
            t3.gameObject.SetActive(false);
            t4.gameObject.SetActive(false);
            fingerColor.gameObject.SetActive(true);
            t1.gameObject.SetActive(true);
            t2.gameObject.SetActive(true); gameObject.SetActive(true);
            tapText.gameObject.SetActive(true);
            canTime = true;
        }
        else
        {
            fingerColor.gameObject.SetActive(false);
            t1.gameObject.SetActive(false);
            t2.gameObject.SetActive(false);
            tapText.gameObject.SetActive(false);
            fingerMovement.gameObject.SetActive(true);
            t3.gameObject.SetActive(true);
            t4.gameObject.SetActive(true);
            canTime = false;
            timer = 0;
        }
    }
}
