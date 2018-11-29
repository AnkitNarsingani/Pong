using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    AudioSource theme;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start ()
    {
        theme = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		
	}

    public void OnTouchVolumeDown()
    {
        while(theme.volume >= 0.2f)
        {
            theme.volume -= 0.01f;
        }
    }


}
