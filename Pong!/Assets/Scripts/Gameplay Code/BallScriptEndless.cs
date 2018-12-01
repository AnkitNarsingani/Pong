using UnityEngine;
using System.Collections;

public class BallScriptEndless : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector2 force;

    SpriteRenderer sr;

    float timer = 0f;

    [SerializeField]
    Sprite ballBoundaries, ballNoBoundaries;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        GameManager.Instance.goingUp = true;
        UpdateColor();

        force.x = Random.Range(5, -6);
        StartCoroutine(StartRally());
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void UpdateColor()
    {
        Color thisBallColor = GameManager.Instance.GenerateRandomColor();
        sr.color = thisBallColor;
        GameManager.Instance.player.GetComponentInParent<PlayerColor>().currentColor = thisBallColor;
    }

    IEnumerator StartRally()
    {
        yield return new WaitForSeconds(2f);
        rb.AddForce(force * 1.2f);
        GameManager.Instance.goingUp = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer >= 0.5f)
        {
            if (sr.color != collision.gameObject.GetComponent<SpriteRenderer>().color)
            {
                Destroy(gameObject);
                if (PlayerPrefs.GetInt("maxRallies", 0) < UIManager.Instance.aiScore)
                    PlayerPrefs.SetInt("maxRallies", UIManager.Instance.aiScore);
                UIManager.Instance.GameLoseEndless();
            }
            else
            {
                collision.gameObject.GetComponentInChildren<Animator>().Play("Player Hit");
                float Xvelocity = transform.position.x - collision.transform.position.x;
                rb.velocity = GetBallForce(Xvelocity);
                GameManager.Instance.goingUp = true;
                GameManager.Instance.rally++;
                timer = 0;
            }
        }
        else if (collision.gameObject.CompareTag("AI") && timer >= 0.5f)
        {
            float Xvelocity = transform.position.x - collision.transform.position.x;
            rb.velocity = -GetBallForce(-Xvelocity);
            UIManager.Instance.GameWinEndless();
            GameManager.Instance.goingUp = false;
            GameManager.Instance.rally++;
            UpdateColor();
            timer = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            if (PlayerPrefs.GetInt("maxRallies", 0) < UIManager.Instance.aiScore)
                PlayerPrefs.SetInt("maxRallies", UIManager.Instance.aiScore);
            UIManager.Instance.GameLoseEndless();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sr.color == collision.gameObject.GetComponent<SpriteRenderer>().color)
            sr.sprite = ballBoundaries;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (sr.color == collision.gameObject.GetComponent<SpriteRenderer>().color)
            sr.sprite = ballBoundaries;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.sprite = ballNoBoundaries;
    }

    Vector2 GetBallForce(float xForce)
    {
        if (GameManager.Instance.goingUp)
        {
            if (GameManager.Instance.rally <= 5)
            {
                return new Vector2(xForce * 1.1f, 5.5f);
            }
            else if (GameManager.Instance.rally <= 10)
            {
                return new Vector2(xForce * 1.1f, 7f);
            }
            else
            {
                return new Vector2(xForce * 1.1f, 8f);
            }
        }
        else
        {
            if (GameManager.Instance.rally <= 5)
            {
                return new Vector2(xForce * 3.5f, 5.5f);
            }
            else if (GameManager.Instance.rally <= 10)
            {
                return new Vector2(xForce * 3f, 7f);
            }
            else
            {
                return new Vector2(xForce * 2.5f, 8f);
            }
        }
    }
}