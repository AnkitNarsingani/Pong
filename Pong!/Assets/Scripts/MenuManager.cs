using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

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
    Image noFaceSprite;

    [SerializeField]
    string Column3 = "Endless Mode";

    [SerializeField]
    Text endlessHighScore;

    [SerializeField]
    Sprite[] levelSprites;

    Sprite currentLevelSprite;

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

    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level);
    }
}
