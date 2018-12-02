using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    public static AdManager Instance { get; private set; }

    string level;
    int index = 0;

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

    private void Start()
    {
        if(Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize("2943168", true);
            Debug.Log("Initialized");
        }
    }
    void ShowBannerAds()
    {
        if (Advertisement.IsReady("banner"))
        {
            Advertisement.Show("banner");
        }
    }

    void ShowBannerAds(string levelName)
    {
        level = levelName;

        if (Advertisement.IsReady("banner"))
        {
            Advertisement.Show("banner", new ShowOptions() { resultCallback = HandleResult });
        }
    }

    public void ShowVideoAds()
    {
        if (Advertisement.IsReady("video") && index < 5)
        {
            index++;
            Advertisement.Show("video");
        }
        else
            ShowRewardedVideoAds();
    }

    public void ShowVideoAds(string levelName)
    {
        level = levelName;

        if (Advertisement.IsReady("video") && index < 5)
        {
            index++;
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleResult });
        }
        else
            ShowRewardedVideoAds(levelName);
    }

    public void ShowRewardedVideoAds()
    {
        if (Advertisement.IsReady("rewardedvideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
    }

    public void ShowRewardedVideoAds(string levelname)
    {
        if (Advertisement.IsReady("rewardedvideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleResult });
        }
    }

    private void HandleResult(ShowResult showResult)
    {
        if(Advertisement.isInitialized)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.Log("Failed");
                    if (UIManager.Instance != null)
                        UIManager.Instance.LevelLoad(level);
                    else
                        MenuManager.Instance.LevelLoad(level);
                    break;
                case ShowResult.Finished:
                    Debug.Log("Finished");
                    if (UIManager.Instance != null)
                        UIManager.Instance.LevelLoad(level);
                    else
                        MenuManager.Instance.LevelLoad(level);
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Skipped");
                    if (UIManager.Instance != null)
                        UIManager.Instance.LevelLoad(level);
                    else
                        MenuManager.Instance.LevelLoad(level);
                    break;
            }
        }
        else
        {
            if (UIManager.Instance != null)
                UIManager.Instance.LevelLoad(level);
            else
                MenuManager.Instance.LevelLoad(level);
        }
    }
}
