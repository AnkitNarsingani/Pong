using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public int rally = 0;

    [HideInInspector]
    public GameObject player, AI;

    [SerializeField]
    GameObject ball;

    [HideInInspector]
    public bool goingUp;

    [SerializeField]
    public Color red, green, blue;

    int playerScore = 0;
    int aiScore = 0;

    [SerializeField]
    int winScore = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Reference()
    {
        player = GameObject.FindWithTag("Player");
        AI = GameObject.FindWithTag("AI");
        ball = GameObject.FindWithTag("Ball");
        AI.GetComponent<AIScript>().ball = this.ball;
    }

    public Color GenerateRandomColor()
    {
        int token = Random.Range(1, 4);
        switch (token)
        {
            case 1:
                return red;
            case 2:
                return green;
            case 3:
                return blue;
            default:
                return red;
        }
    }

    public void GameLose()
    {
        Time.timeScale = 0;
        rally = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameWin(string winner)
    {
        if (winner.Equals("AI"))
        {
            aiScore++;
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
}
