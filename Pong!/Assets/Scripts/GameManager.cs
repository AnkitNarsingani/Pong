using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [HideInInspector]
    public int rally = 0;

    [HideInInspector]
    public GameObject player, AI;

    [SerializeField]
    GameObject circle;

    GameObject ball;

    [HideInInspector]
    public bool goingUp;

    [SerializeField]
    public Color red, green, blue;
    
    [HideInInspector]
    public int playerScore = 0, aiScore = 0;

    [SerializeField]
    Text AIText, playerText;

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
        UpdateScore();
        Reference();
    }

    void Update()
    {
        
    }

    void Reference()
    {
        player = GameObject.FindWithTag("Player");
        AI = GameObject.FindWithTag("AI");
        ball = Instantiate(circle, transform.position, Quaternion.identity) as GameObject;
        AI.GetComponent<AIScript>().ball = ball;
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
        rally = 0;
        Reference();
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
}
