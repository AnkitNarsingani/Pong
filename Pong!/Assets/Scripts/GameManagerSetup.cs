using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSetup : MonoBehaviour
{
    [SerializeField]
    Color[] levelColor;

    void ColorSetup()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        int i = (3 * currentLevel) - 1;
        GameManager.Instance.thirdColor = levelColor[i--];
        GameManager.Instance.secondColor = levelColor[i--];
        GameManager.Instance.firstColor = levelColor[i];
    }
}
