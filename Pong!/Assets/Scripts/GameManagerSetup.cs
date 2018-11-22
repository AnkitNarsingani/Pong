using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSetup : MonoBehaviour
{
    [SerializeField]
    Color[] levelColor;

    int currentLevel;

    [SerializeField]
    GameObject ballNormal, ballEndless;

    private void Start()
    {
        currentLevel = MenuManager.Instance.currentLevelNo;
        ColorSetup();
    }

    public void ColorSetup()
    { 
        int i = (3 * currentLevel) - 1;
        GameManager.Instance.firstColor = levelColor[i--];
        GameManager.Instance.secondColor = levelColor[i--];
        GameManager.Instance.thirdColor = levelColor[i];
    }

    public void OnTouchPlay()
    {
        GameManager.Instance.circle = ballNormal;
    }

    public void OnTouchEndless()
    {
        GameManager.Instance.circle = ballEndless;
    }   
}
