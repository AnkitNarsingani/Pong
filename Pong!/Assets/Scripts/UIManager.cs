using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject inGameUI, pauseUI, gameOverUI;


    private bool isPaused = false;

    [HideInInspector]
    public int playerScore = 0, aiScore = 0;

    float timer;


    [SerializeField]
    Text AIText, playerText;

    bool isGreyScale = false;

    bool isLookingForInput = false;

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

    void Start()
    {
        UpdateScore();
    }

    void Update()
    {
        if (isLookingForInput && Input.touchCount > 0 && timer > 1f)
        {
            ChangeProfile();
            timer = 0f;
        }
        else if (isLookingForInput)
        {
            timer += Time.deltaTime;
        }
    }

    public void ChangeProfile()
    {
        isGreyScale = !isGreyScale;
        if (isGreyScale)
        {
            Camera.main.GetComponent<Grayscale>().enabled = true;
            gameOverUI.SetActive(true);
            isLookingForInput = true;
        }
        else
        {
            Camera.main.GetComponent<Grayscale>().enabled = false;
            gameOverUI.SetActive(false);
            isLookingForInput = false;
            GameLose();
        }
    }

    public void GameWin(string winner)
    {
        if (winner.Equals("AI"))
        {
            aiScore++;
            UpdateScore();
            if (aiScore >= winScore)
            {
                PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
                Debug.Log("AI Wins");
            }
            else
            {
                ChangeProfile();
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
                ChangeProfile();
            }
        }

    }

    public void GameWinEndless()
    {
        aiScore++;
        UpdateScore();
    }

    void UpdateScore()
    {
        AIText.text = aiScore.ToString();
        if (playerText != null)
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

        if (isPaused)
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

    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }
}
