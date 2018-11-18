using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    GameObject menuUI, startUI;

    [SerializeField]
    string Column1 = "Settings";

    [SerializeField]
    Text leftHanded, rightHanded;

    [SerializeField]
    Sprite muteSprite, unmuteSprite;

    bool muted = false;

    [SerializeField]
    Animator anim;

    [SerializeField]
    string Column2 = "Level Selector";

    [SerializeField]
    Text nextLevelName;

    [SerializeField]
    Image faceSprite;

    [SerializeField]
    string Column3 = "Endless Mode";

    [SerializeField]
    Text endlessHighScore;

    Sprite currentLevelSprite;

    [SerializeField]
    string Column4 = "Start UI";

    [SerializeField]
    bool shouldPlayanim = false;

    [SerializeField]
    Color yellow;

    [SerializeField]
    RectTransform baby, levelNameText;

    [SerializeField]
    Text levelNumber;

    [SerializeField]
    Image face; 

    [SerializeField]
    Sprite[] levelSprites;

    [SerializeField]
    string[] levelName;

    void Start ()
    {
        Time.timeScale = 1;

        if (GameManager.Instance.isLeftHanded)
        {
            anim.Play("Slide Left");
            leftHanded.color = Color.black;
            rightHanded.color = Color.white;
        }
        else
        {
            anim.Play("Slide Right");
            leftHanded.color = Color.white;
            rightHanded.color = Color.black;
        }

        endlessHighScore.text = PlayerPrefs.GetInt("maxRallies").ToString();
    }
	

	void Update ()
    {
		if(shouldPlayanim)
        {
            if (baby.localPosition.x < 0)
            {
                baby.localPosition += new Vector3(43, 0, 0);
            }               
            if(levelNameText.localPosition.x > 17)
            {
                levelNameText.localPosition -= new Vector3(43, 0, 0);
            }
        }

        if (levelNameText.localPosition.x < 18 && baby.localPosition.x > 0 && !shouldPlayanim)
        {
            baby.localPosition += new Vector3(43, 0, 0);
            levelNameText.localPosition -= new Vector3(43, 0, 0);
        }
    }


    public void OnTouchHand()
    {
        GameManager.Instance.UpdateSettings();

        if(GameManager.Instance.isLeftHanded)
        {
            anim.Play("Slide Left");
            StartCoroutine(ActivateButton(0.5f));
            leftHanded.color = Color.black;
            rightHanded.color = Color.white;
        }
        else
        {
            anim.Play("Slide Right");
            StartCoroutine(ActivateButton(0.5f));
            leftHanded.color = Color.white;
            rightHanded.color = Color.black;
        }
    }

    IEnumerator ActivateButton(float waitTime)
    {
        leftHanded.GetComponentInParent<Button>().interactable = false;
        yield return new WaitForSeconds(waitTime);
        leftHanded.GetComponentInParent<Button>().interactable = true;
    }

    public void OnTouchAudio(Button button)
    {
        muted = !muted;

        if(muted)
        {
            AudioListener.volume = 0;
            button.image.overrideSprite = unmuteSprite;
        }
        else
        {
            AudioListener.volume = 1;
            button.image.overrideSprite = muteSprite;
        }
    }

    public void OnTouchPlay()
    {
        menuUI.SetActive(false);
        Camera.main.backgroundColor = yellow;
        startUI.SetActive(true);
        shouldPlayanim = true;
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(2.5f);

        shouldPlayanim = false;

        yield return new WaitForSeconds(2f);

        LevelLoad(PlayerPrefs.GetInt("currentLevel" , 1));
    }

    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }
}
