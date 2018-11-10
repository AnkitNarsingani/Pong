using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject inGameUI, pauseUI, gameOverUI;


    private bool isPaused = false;

    [HideInInspector]
    public int playerScore = 0, aiScore = 0;


    [SerializeField]
    Text AIText, playerText;


    [SerializeField]
    int winScore = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScore();
    }

    public void GameWin(string winner)
    {
        if (winner.Equals("AI"))
        {
            aiScore++;
            UpdateScore();
            if (aiScore >= winScore)
            {
                Debug.Log("AI Wins");
            }
            else
            {
                GameLose();
            }
        }
        else if (winner.Equals("Player"))
        {
            playerScore++;
            UpdateScore();
            if (playerScore >= winScore)
            {
                Debug.Log("Player Wins");
            }
            else
            {
                GameLose();
            }
        }

    }

    void UpdateScore()
    {
        AIText.text = aiScore.ToString();
        playerText.text = playerScore.ToString();
    }

    public void GameLose()
    {
        GameManager.Instance.rally = 0;
        GameManager.Instance.Reference();
    }

    public void OnTouchPause()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }
}
