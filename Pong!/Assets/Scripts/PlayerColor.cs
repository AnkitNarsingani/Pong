using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;

    private int count = 0;

    [HideInInspector]
    public Color currentColor;

    public void OnTouchDown(Vector3 point)
    {
        count++;
        switch (count)
        {
            case 1:
                sr.color = GameManager.Instance.red;
                break;
            case 2:
                sr.color = GameManager.Instance.green;
                break;
            case 3:
                count = 0;
                sr.color = GameManager.Instance.blue;
                break;
        }
    }
}
