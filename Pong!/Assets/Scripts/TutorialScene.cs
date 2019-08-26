using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScene : MonoBehaviour
{

    bool movePlayerFinger, goingLeft, tapPlayerFinger, displayScore, shrinking;

    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject thumbRight, thumbLeft, moveText, colorText, scoreText,fivePoint;

    [SerializeField]
    Text playerText, aiText;

    Color initialColor;

    void Start()
    {
        movePlayerFinger = true;
        goingLeft = true;
        shrinking = false;
        player.GetComponentInParent<BasicMovement>().enabled = false;
        player.GetComponentInParent<PlayerColor>().enabled = false;
    }


    void Update()
    {
        if (movePlayerFinger)
        {
            MovePlayer();
        }
        else if (tapPlayerFinger)
        {
            ChangeColor();
        }
        else if (displayScore)
        {
            DisplayScore();
        }
    }

    private void DisplayScore()
    {
        if (playerText.rectTransform.localScale.x < 1.1f && !shrinking)
        {
            playerText.rectTransform.localScale = Vector3.Lerp(playerText.rectTransform.localScale, new Vector3(1.2f, 1.2f, 1), Time.fixedDeltaTime);
            aiText.rectTransform.localScale = Vector3.Lerp(aiText.rectTransform.localScale, new Vector3(1.2f, 1.2f, 1), Time.deltaTime);
        }
        else
            shrinking = true;
        if (playerText.rectTransform.localScale.x > 1.05f && shrinking)
        {
            playerText.rectTransform.localScale = Vector3.Lerp(playerText.rectTransform.localScale, new Vector3(1, 1, 1), Time.fixedDeltaTime);
            aiText.rectTransform.localScale = Vector3.Lerp(aiText.rectTransform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
        }
        else
            shrinking = false;
    }

    private void ChangeColor()
    {
        if (player.GetComponent<SpriteRenderer>().color != initialColor)
        {
            colorText.SetActive(false);
            thumbLeft.SetActive(false);
            tapPlayerFinger = false;
            displayScore = true;
            StartCoroutine(DisplayScoreText());
        }
    }

    private void MovePlayer()
    {

        if (Input.touchCount > 0 && Mathf.Abs(Input.touches[0].position.y - player.position.y) < 100)
        {
            Destroy(thumbRight);
            movePlayerFinger = false;
            tapPlayerFinger = true;
            player.GetComponentInParent<BasicMovement>().enabled = true;
            player.GetComponentInParent<PlayerColor>().enabled = true;
            moveText.SetActive(false);
            colorText.SetActive(true);
            thumbLeft.SetActive(true);
            initialColor = player.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            if (player.position.x > -1.95f && goingLeft)
                player.position += new Vector3(-2.5f * Time.deltaTime, 0, 0);
            else
                goingLeft = false;
            if (player.position.x < 1.95f && !goingLeft)
                player.position -= new Vector3(-2.5f * Time.deltaTime, 0, 0);
            else
                goingLeft = true;
        }
    }

    IEnumerator DisplayScoreText()
    {
        yield return new WaitForSeconds(0.5f);
        scoreText.SetActive(true);
        yield return new WaitForSeconds(4);
        scoreText.SetActive(false);
        displayScore = false;
        aiText.rectTransform.localScale = new Vector3(1, 1, 1);
        playerText.rectTransform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(WinCondition());
    }

    IEnumerator WinCondition()
    {
        fivePoint.SetActive(true);
        yield return new WaitForSeconds(4);
        fivePoint.SetActive(false);
        PlayerPrefs.SetInt("tutorial", 1);
        UIManager.Instance.LevelLoad("Level 17");
    }
}
