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

    Gradient firstGradient, secondGradient, thirdGradient;

    TrailRenderer tr;

    AudioSource audio;

    [SerializeField]
    AudioClip boing1, boing2;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }
    void Start()
    {
        GameManager.Instance.goingUp = true;

        firstGradient = UIManager.Instance.firstGradient;
        secondGradient = UIManager.Instance.secondGradient;
        thirdGradient = UIManager.Instance.thirdGradient;

        UpdateColor();

        force.x = Random.Range(5, -6);
        GameManager.Instance.AI.GetComponent<AIScript>().StartColorChange();
        StartCoroutine(StartRally());

        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void UpdateColor()
    {
        Color thisBallColor = GameManager.Instance.GenerateRandomColor();
        sr.color = thisBallColor;
        tr.colorGradient = UpdateGradient(thisBallColor);
        GameManager.Instance.player.GetComponentInParent<PlayerColor>().currentColor = thisBallColor;
        GameManager.Instance.AI.GetComponent<AIScript>().currentColor = thisBallColor;
    }

    Gradient UpdateGradient(Color c)
    {
        if (c == GameManager.Instance.firstColor)
            return firstGradient;
        else if (c == GameManager.Instance.secondColor)
            return secondGradient;
        else
            return thirdGradient;
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
                int temp = Random.Range(1, 3);
                if (temp == 1)
                {
                    audio.clip = boing1;
                    audio.Play();
                }
                else
                {
                    audio.clip = boing2;
                    audio.Play();
                }
                collision.gameObject.GetComponentInChildren<Animator>().Play("Player Hit");
                float Xvelocity = transform.position.x - collision.transform.position.x;
                rb.velocity = GetBallForce(Xvelocity);
                GameManager.Instance.rally++;
                GameManager.Instance.goingUp = true;
                UpdateColor();
                GameManager.Instance.AI.GetComponent<AIScript>().StartColorChange();
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
                int temp = Random.Range(1, 3);
                if (temp == 1)
                {
                    audio.clip = boing1;
                    audio.Play();
                }
                else
                {
                    audio.clip = boing2;
                    audio.Play();
                }
                float Xvelocity = transform.position.x - collision.transform.position.x;
                collision.gameObject.GetComponentInChildren<Animator>().Play("Hit");
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
