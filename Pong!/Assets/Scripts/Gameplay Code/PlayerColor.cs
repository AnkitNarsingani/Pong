using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer srPlayer, srPaddleLeft, srPaddleRight;

    [HideInInspector]
    public int count = 0;

    [HideInInspector]
    public Color currentColor;

    private void Start()
    {
        if (GameManager.Instance.isLeftHanded)
        {
            srPaddleLeft.gameObject.SetActive(true);
            srPaddleRight.gameObject.SetActive(false);
        }
        else
        {
            srPaddleLeft.gameObject.SetActive(false);
            srPaddleRight.gameObject.SetActive(true);
        }

        OnTouchDown(Vector3.one);
    }

    public void OnTouchDown(Vector3 point)
    {
        if (UIManager.Instance.endless && Input.touchCount > 0)
            UIManager.Instance.LevelLoad("Main Menu");

        if (Time.timeScale != 0)
        {
            count++;
            switch (count)
            {
                case 1:
                    srPlayer.color = GameManager.Instance.firstColor;
                    srPaddleLeft.color = GameManager.Instance.firstColor;
                    srPaddleRight.color = GameManager.Instance.firstColor;
                    break;
                case 2:
                    srPlayer.color = GameManager.Instance.secondColor;
                    srPaddleLeft.color = GameManager.Instance.secondColor;
                    srPaddleRight.color = GameManager.Instance.secondColor;
                    break;
                case 3:
                    count = 0;
                    srPlayer.color = GameManager.Instance.thirdColor;
                    srPaddleLeft.color = GameManager.Instance.thirdColor;
                    srPaddleRight.color = GameManager.Instance.thirdColor;
                    break;
            }
        }
    }
}
