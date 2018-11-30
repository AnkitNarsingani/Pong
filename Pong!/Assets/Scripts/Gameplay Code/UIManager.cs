using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    public Gradient firstGradient, secondGradient, thirdGradient;

    [SerializeField]
    GameObject inGameUI, pauseUI, gameLosePointUI, gameWinPointUI, gameOverUI, gameWinUI;


    private bool isPaused = false;

    [HideInInspector]
    public int playerScore = 0, aiScore = 0;

    float timer;

    Camera main;

    [SerializeField]
    Text AIText, playerText;

    bool isGreyScale = false;

    bool displayText = false;

    bool isLookingForInput = false;

    bool endless = false;

    [SerializeField]
    int winScore = 5;

    [SerializeField]
    Color loseColorCamera, loseColorPlayer;

    [SerializeField]
    GameObject paddleLeft, paddleRight;

    [SerializeField]
    PlayerColor playerColorScript;

    Color defaultColorCamera, defaultColorText;

    [SerializeField]
    LayerMask UI;

    [SerializeField]
    Color GameLoseBackgroundColor, GameWinBackgroundColor;
 

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
        main = Camera.main;
        defaultColorCamera = main.backgroundColor;
        defaultColorText = AIText.color;
    }

    void Update()
    {
        if (isLookingForInput && Input.touchCount > 0 && timer > 1f)
        {
            ChangeProfileLost();
            timer = 0f;
        }
        else if (isLookingForInput)
        {
            timer += Time.deltaTime;
        }

        if(endless)
        {
            main.backgroundColor = Color.Lerp(main.backgroundColor, GameLoseBackgroundColor, Time.deltaTime);
            var tempcolor = gameOverUI.GetComponent<Text>().color;
            tempcolor.a = Mathf.Lerp(tempcolor.a, 1, Time.deltaTime * 1.2f);
            gameOverUI.GetComponent<Text>().color = tempcolor;
        }
            
    }

    void ChangeProfileLost()
    {
        isGreyScale = !isGreyScale;
        if (isGreyScale)
        {
            main.GetComponent<Grayscale>().enabled = true;
            main.backgroundColor = loseColorCamera;
            GameManager.Instance.player.GetComponent<SpriteRenderer>().color = loseColorPlayer;
            GameManager.Instance.AI.GetComponent<SpriteRenderer>().color = loseColorPlayer;
            paddleLeft.GetComponent<SpriteRenderer>().color = loseColorPlayer;
            paddleRight.GetComponent<SpriteRenderer>().color = loseColorPlayer;
            AIText.color = loseColorPlayer;
            playerText.color = loseColorPlayer;
            gameLosePointUI.SetActive(true);
            isLookingForInput = true;
        }
        else
        {
            main.GetComponent<Grayscale>().enabled = false;
            main.backgroundColor = defaultColorCamera;
            GameManager.Instance.player.GetComponent<SpriteRenderer>().color = GameManager.Instance.firstColor;
            GameManager.Instance.AI.GetComponent<SpriteRenderer>().color = GameManager.Instance.firstColor;
            paddleLeft.GetComponent<SpriteRenderer>().color = GameManager.Instance.firstColor;
            paddleRight.GetComponent<SpriteRenderer>().color = GameManager.Instance.firstColor;
            AIText.color = defaultColorText;
            playerText.color = defaultColorText;
            playerColorScript.count = 1;
            gameLosePointUI.SetActive(false);
            isLookingForInput = false;
            GameReset();
        }
    }

    IEnumerator ChangeProfileWin()
    {

        gameWinPointUI.SetActive(true);

        yield return new WaitForSeconds(2);

        gameWinPointUI.SetActive(false);
        GameReset();

    }

    public void GameWin(string winner)
    {
        if (winner.Equals("AI"))
        {
            aiScore++;
            UpdateScore();
            if (aiScore >= winScore)
            {
                GameLost(true);
            }
            else
            {
                ChangeProfileLost();
            }
        }
        else if (winner.Equals("Player"))
        {
            playerScore++;
            UpdateScore();
            if (playerScore >= winScore)
            {
                if (PlayerPrefs.GetInt("currentLevel", 17) > 1)
                {
                    PlayerPrefs.SetInt("currentLevel", (PlayerPrefs.GetInt("currentLevel", 17) - 1));
                }
                GameLost(false);
            }
            else
            {
                StartCoroutine(ChangeProfileWin());
            }
        }

    }

    void GameLost(bool playerLost)
    {
        main.cullingMask = UI;
        inGameUI.SetActive(false);
        if (playerLost)
        {
            main.backgroundColor = GameLoseBackgroundColor;
            GameManager.Instance.rally = 0;
            gameOverUI.SetActive(true);
        }
        else
        {
            main.backgroundColor = GameWinBackgroundColor;
            gameWinUI.SetActive(true);
        }
    }

    public void GameWinEndless()
    {
        aiScore++;
        UpdateScore();
    }

    public void GameLoseEndless()
    {
        main.cullingMask = UI;
        endless = true;
        gameOverUI.SetActive(true);
    }

    void UpdateScore()
    {
        AIText.text = aiScore.ToString();
        if (playerText != null)
            playerText.text = playerScore.ToString();
    }

    public void GameReset()
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
    public void OnTouchGo()
    {
        GameManager.Instance.shouldLoadNextScene = true;
        GameManager.Instance.rally = 0;
        LevelLoad("Main Menu");
    }

    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LevelLoad(string level)
    {
        SceneManager.LoadScene(level);
    }
}
