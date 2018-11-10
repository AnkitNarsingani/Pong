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
    GameObject circle;

    GameObject ball;

    [HideInInspector]
    public bool goingUp;

    [SerializeField]
    public Color firstColor, secondColor, thirdColor;

    [HideInInspector]
    public bool isLeftHanded = true;

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
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void GetPlayerData()
    {
        PlayerPrefs.SetInt("RightHanded", 1);
    }

    public void Reference()
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
                return firstColor;
            case 2:
                return secondColor;
            case 3:
                return thirdColor;
            default:
                return firstColor;
        }
    }
}
