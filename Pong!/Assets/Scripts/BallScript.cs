using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
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
        GameManager.Instance.AI.GetComponent<AIScript>().StartColorChange();
        StartCoroutine(StartRally());
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void StartRallyy()
    {
        if (GameManager.Instance.goingUp)
        {
            rb.AddForce(force);
            GameManager.Instance.goingUp = true;
            GameManager.Instance.AI.GetComponent<AIScript>().StartColorChange();
        }
        else if (!GameManager.Instance.goingUp)
        {
            rb.AddForce(-force);
            GameManager.Instance.goingUp = false;
        }
    }

    private void UpdateColor()
    {
        Color thisBallColor = GameManager.Instance.GenerateRandomColor();
        sr.color = thisBallColor;
        GameManager.Instance.player.GetComponentInParent<PlayerColor>().currentColor = thisBallColor;
        GameManager.Instance.AI.GetComponent<AIScript>().currentColor = thisBallColor;
    }

    IEnumerator StartRally()
    {
        yield return new WaitForSeconds(2f);
        rb.AddForce(force);
        GameManager.Instance.goingUp = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer >= 0.5f)
        {
            if (sr.color != collision.gameObject.GetComponent<SpriteRenderer>().color)
            {
                Destroy(gameObject);
                UIManager.Instance.GameWin("AI");
            }
            else
            {
                collision.gameObject.GetComponentInChildren<Animator>().Play("Player Hit");
                float Xvelocity = transform.position.x - collision.transform.position.x;
                rb.velocity = GetBallForce(Xvelocity);
                GameManager.Instance.rally++;
                GameManager.Instance.goingUp = true;
                GameManager.Instance.AI.GetComponent<AIScript>().StartColorChange();
                UpdateColor();
                timer = 0;
            }
        }
        else if (collision.gameObject.CompareTag("AI") && timer >= 0.5f)
        {
            if (sr.color != collision.gameObject.GetComponent<SpriteRenderer>().color)
            {
                Destroy(gameObject);
                UIManager.Instance.GameWin("Player");
            }
            else
            {
                float Xvelocity = transform.position.x - collision.transform.position.x;
                rb.velocity = -GetBallForce(-Xvelocity);
                GameManager.Instance.rally++;
                GameManager.Instance.goingUp = false;
                UpdateColor();
                timer = 0;
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            if (collision.gameObject.name.Equals("Top Wall"))
                UIManager.Instance.GameWin("Player");
            else if (collision.gameObject.name.Equals("Bottom Wall"))
                UIManager.Instance.GameWin("AI");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        sr.sprite = ballBoundaries;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.sprite = ballNoBoundaries;
    }

    Vector2 GetBallForce(float xForce)
    {
        if (GameManager.Instance.rally <= 4)
        {
            return new Vector2(xForce * 3.5f, 5.5f);
        }
        else if (GameManager.Instance.rally <= 8)
        {
            return new Vector2(xForce * 3, 7);
        }
        else
        {
            return new Vector2(xForce * 3.5f, 8.5f);
        }
    }
}
