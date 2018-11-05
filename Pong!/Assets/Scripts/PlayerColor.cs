using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer srPlayer, srPaddle;

    private int count = 0;

    [HideInInspector]
    public Color currentColor;

    public void OnTouchDown(Vector3 point)
    {
        count++;
        switch (count)
        {
            case 1:
                srPlayer.color = GameManager.Instance.red;
                srPaddle.color = GameManager.Instance.red;
                break;
            case 2:
                srPlayer.color = GameManager.Instance.green;
                srPaddle.color = GameManager.Instance.green;
                break;
            case 3:
                count = 0;
                srPlayer.color = GameManager.Instance.blue;
                srPaddle.color = GameManager.Instance.blue;
                break;
        }
    }
}
