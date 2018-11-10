using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    string Column1;

    [SerializeField]
    Text leftHanded, rightHanded;

    [SerializeField]
    Sprite muteSprite, unmuteSprite;

    bool muted = false;

	void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}


    public void OnTouchHand(Animator anim)
    {
        GameManager.Instance.isLeftHanded = !GameManager.Instance.isLeftHanded;

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
