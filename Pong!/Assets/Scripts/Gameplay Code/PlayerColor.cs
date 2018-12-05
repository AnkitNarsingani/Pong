using UnityEngine;
using UnityEngine.UI;

public class PlayerColor : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer srPlayer, tempLeft , tempRight;

    [SerializeField]
    Image srPaddleLeft, srPaddleRight;

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

            tempLeft.gameObject.SetActive(true);
            tempRight.gameObject.SetActive(false);
        }
        else
        {
            srPaddleLeft.gameObject.SetActive(false);
            srPaddleRight.gameObject.SetActive(true);

            tempLeft.gameObject.SetActive(false);
            tempRight.gameObject.SetActive(true);
        }

        OnTouchDown(Vector3.one);
    }

    public void OnTouchDown(Vector3 point)
    {
        if (UIManager.Instance.endless && Input.touchCount > 0)
            AdManager.Instance.ShowVideoAds("Main Menu");

        if (Time.timeScale != 0)
        {
            count++;
            switch (count)
            {
                case 1:
                    srPlayer.color = GameManager.Instance.firstColor;
                    srPaddleLeft.color = GameManager.Instance.firstColor;
                    srPaddleRight.color = GameManager.Instance.firstColor;
                    tempLeft.color = GameManager.Instance.firstColor;
                    tempRight.color = GameManager.Instance.firstColor;
                    break;
                case 2:
                    srPlayer.color = GameManager.Instance.secondColor;
                    srPaddleLeft.color = GameManager.Instance.secondColor;
                    srPaddleRight.color = GameManager.Instance.secondColor;
                    tempLeft.color = GameManager.Instance.secondColor;
                    tempRight.color = GameManager.Instance.secondColor;
                    break;
                case 3:
                    count = 0;
                    srPlayer.color = GameManager.Instance.thirdColor;
                    srPaddleLeft.color = GameManager.Instance.thirdColor;
                    srPaddleRight.color = GameManager.Instance.thirdColor;
                    tempLeft.color = GameManager.Instance.secondColor;
                    tempRight.color = GameManager.Instance.secondColor;
                    break;
            }
        }
    }
}
