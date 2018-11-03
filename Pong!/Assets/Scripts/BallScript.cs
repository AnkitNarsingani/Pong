using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector2 force;

    SpriteRenderer sr;

    float timer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        GameManager.Instance.Reference();
        UpdateColor();

        force.x = Random.Range(5, -6);
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            rb.AddForce(force);
            GameManager.Instance.goingUp = true;
            GameManager.Instance.AI.GetComponent<AIScript>().StartColorChange();
        }
        else
        {
            rb.AddForce(-force);
            GameManager.Instance.goingUp = false;
        }
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
        GameManager.Instance.AI.GetComponent<AIScript>().currentColor = thisBallColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer >= 0.5f)
        {
            if (sr.color != collision.gameObject.GetComponent<SpriteRenderer>().color)
            {
                GameManager.Instance.GameWin("AI");
            }
            else
            {
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
                GameManager.Instance.GameWin("Player");
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
                GameManager.Instance.GameWin("Player");
            else if (collision.gameObject.name.Equals("Bottom Wall"))
                GameManager.Instance.GameWin("AI");
        }
    }

    Vector2 GetBallForce(float xForce)
    {
        if(GameManager.Instance.rally <= 4)
        {
            return new Vector2(xForce * 2.5f, 5.5f);
        }
        else if(GameManager.Instance.rally <= 8)
        {
            return new Vector2(xForce * 3, 7);
        }
        else
        {
            return new Vector2(xForce * 3.5f, 8.5f);
        }
    }
}
