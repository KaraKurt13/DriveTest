using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceManager : MonoBehaviour
{
    public static IronSourceManager Instance { get; private set; }

    [SerializeField]
    private string _appKey = "208e31995";

    private void Start()
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

        IronSource.Agent.init(_appKey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.validateIntegration();
    }

    private int _doubleRewardAmount;

    public void DoubleReward(int amount)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            _doubleRewardAmount = amount;
            IronSource.Agent.showRewardedVideo("DoubleReward");
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnDoubleRewardVideoComplete;
        }
        else
        {
            Debug.Log("Ad is not available.");
        }
    }

    public void OnDoubleRewardVideoComplete(IronSourcePlacement placement, IronSourceAdInfo info)
    {
        SaveSystem.PlayerData.Cash += _doubleRewardAmount;
        SaveSystem.SavePlayerData();
        _doubleRewardAmount = 0;
        IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnDoubleRewardVideoComplete;
    }
}
