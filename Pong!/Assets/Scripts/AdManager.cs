using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    public static AdManager Instance { get; private set; }

    string level;

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
        if (Advertisement.isSupported && !Advertisement.isInitialized && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Initialize("2943168", false);
            Debug.Log("Initialized");
        }
    }
    void ShowBannerAds()
    {
        if (Advertisement.IsReady("banner") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("banner");
        }
    }

    void ShowBannerAds(string levelName)
    {
        level = levelName;

        if (Advertisement.IsReady("banner") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("banner", new ShowOptions() { resultCallback = HandleResult });
        }
        else if (Advertisement.IsReady("displaypicture") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("displaypicture", new ShowOptions() { resultCallback = HandleResult });
        }
        else
        {
            if (UIManager.Instance != null)
                UIManager.Instance.LevelLoad(levelName);
            else
                MenuManager.Instance.LevelLoad(levelName);
        }
    }

    public void ShowVideoAds()
    {
        if (Advertisement.IsReady("video") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("video");
        }
        else
            ShowRewardedVideoAds();
    }

    public void ShowVideoAds(string levelName)
    {
        level = levelName;

        if (Advertisement.IsReady("video") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleResult });
        }
        else
            ShowRewardedVideoAds(levelName);
    }

    public void ShowRewardedVideoAds()
    {
        if (Advertisement.IsReady("rewardedVideo") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("rewardedVideo");
        }
        else if (Advertisement.IsReady("displaypicture") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("displaypicture", new ShowOptions() { resultCallback = HandleResult });
        }
    }

    public void ShowRewardedVideoAds(string levelName)
    {
        level = levelName;
        if (Advertisement.IsReady("rewardedVideo") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleResult });
        }
        else if (Advertisement.IsReady("displaypicture") && !GameManager.Instance.hasPurchased)
        {
            Advertisement.Show("displaypicture", new ShowOptions() { resultCallback = HandleResult });
        }
        else
        {
            if (UIManager.Instance != null)
                UIManager.Instance.LevelLoad(levelName);
            else
                MenuManager.Instance.LevelLoad(levelName);
        }
    }

    private void HandleResult(ShowResult showResult)
    {
        if (Advertisement.isInitialized && !GameManager.Instance.hasPurchased)
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
