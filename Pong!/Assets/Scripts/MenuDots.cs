using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDots : MonoBehaviour
{
    [SerializeField]
    Image[] cards;

    [SerializeField]
    Sprite highlighted, notHighlighted;

    [SerializeField]
    int totalDots;

    public void ChangeDots(int n)
    {
        for (int i = 0; i < totalDots; i++)
        {
            if (i == n)
                cards[i].overrideSprite = highlighted;
            else
                cards[i].overrideSprite = notHighlighted;
        }
    }
}
