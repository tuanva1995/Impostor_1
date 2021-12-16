using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IronSourceAdsController : MonoBehaviour
{
    public static IronSourceAdsController Instance;
    private string appKey;
    private System.Action onUserEarnedReward, onAdClosed;
    bool isLoadBanerOke;
    float countTimeShowFullAds;
    //public delegate void OnInterComplete();
    //public OnInterComplete InterComplete;
    System.Action InterComplete;
    private void Awake()
    {
        Instance = this;
    }
    public bool IsInterstitialAdsReady()
    {
        //return false;
         return IronSource.Agent.isInterstitialReady();
    }
    public bool IsVideoRewardAdsReady()
    {
       // return false;
         return IronSource.Agent.isRewardedVideoAvailable();
    }
    public void LoadInterstitial()
    {
         IronSource.Agent.loadInterstitial();
    }
    public void LoadRewardedVideo()
    {
        throw new NotImplementedException();
    }
    public void ShowInterstitial(System.Action onComplete = null)
    {
        //if (countTimeShowFullAds > 0)
        //{
        //    return;
        //}
        if (UserData.RemoveAdsState == true ) return;
        //         Debug.Log("unity-script: ShowInterstitialButtonClicked");
        if (IronSource.Agent.isInterstitialReady())
        {
            //countTimeShowFullAds = StaticData.dataConfig.adsConfig.delayTimeFullAds;
            InterComplete = onComplete;
            IronSource.Agent.showInterstitial();
            //UserData.countShowAdsInter += 1;
            //UserData.IsShowAds = true;
        }
        else
        {
            IronSource.Agent.loadInterstitial();
        }
    }
    public void ShowVideoAds(Action onUserEarnedReward, Action onAdClosed)
    {
        this.onUserEarnedReward = onUserEarnedReward;
        this.onAdClosed = onAdClosed;
        IronSource.Agent.showRewardedVideo();
    }
    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
    }
    //Invoked when the RewardedVideo ad view has clicked.
    void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
        // Debug.Log("Close ads: " + System.DateTime.UtcNow.ToLongTimeString());
    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        // isVideoLoaded = available;
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        // Debug.Log("Reward player: " + System.DateTime.UtcNow.ToLongTimeString());
        if (onUserEarnedReward != null)
            onUserEarnedReward.Invoke();
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    void RewardedVideoAdStartedEvent()
    {
    }
    //Invoked when the video ad finishes playing. 
    void RewardedVideoAdEndedEvent()
    {
    }
    //Invoked when the initialization process has failed.
    //@param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
    }
    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
    }
    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
     void InterstitialAdShowFailedEvent(IronSourceError error)
     {
     }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
    }
    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
        InterComplete?.Invoke();
        InterComplete = null;

        IronSource.Agent.loadInterstitial();
    }
    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
    }
    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
    }
    public void LoadBanner()
    {
        if (UserData.RemoveAdsState) return;
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
        HideBanner();
    }
    public void HideBanner()
    {
        IronSource.Agent.hideBanner();
    }
    void BannerAdLoadedEvent()
    {
        HideBanner();
        isLoadBanerOke = true;
        //GameCanvas.Instance.SetUiBaner(true);
        Debug.Log("unity-script: I got BannerAdLoadedEvent");
        // GameCanvas.Instance.botton.GetComponent<RectTransform>().localPosition = new Vector2(GameCanvas.Instance.botton.GetComponent<RectTransform>().localPosition.x, 185 );
    }
    void BannerAdLoadFailedEvent(IronSourceError error) // load không thành công baner
    {
        isLoadBanerOke = false;
        Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }
    void BannerAdClickedEvent()
    {
        Debug.Log("unity-script: I got BannerAdClickedEvent");
    }

    void BannerAdScreenPresentedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
    }

    void BannerAdScreenDismissedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
    }

    void BannerAdLeftApplicationEvent()
    {
        Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
    }

    #region ImpressionSuccess callback handler

    void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionSuccessEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionSuccessEvent allData: " + impressionData.allData);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        string appKey = "";
#elif UNITY_IPHONE
        string appKey = "";
#else
        string appKey = "unexpected_platform";
#endif
        //For Rewarded Video
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
        //For Interstitial
        IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
        //For Offerwall
        IronSource.Agent.init(appKey, IronSourceAdUnits.OFFERWALL);
        //For Banners
        IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER);
        IronSource.Agent.init(appKey);
        #region video reward
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        #endregion
        #region interstitial
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        #endregion

        // Add Banner Events
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        IronSource.Agent.validateIntegration();
        IronSource.Agent.setAdaptersDebug(true);

        //Add ImpressionSuccess Event
        IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;

        //Load Ads
        IronSource.Agent.loadInterstitial();

        //LoadBanner();
    }
    void OnDestroy()
    {
        #region video reward
        IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent -= RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent -= RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent -= RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
        #endregion
        #region interstitial
        IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent -= InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent -= InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent -= InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent -= InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent -= InterstitialAdClosedEvent;
        #endregion
    }
    void OnApplicationPause(bool isPaused)
    {
        //IronSource.Agent.onApplicationPause(isPaused);
    }
    private float timeStamp = 0;
#if !UNITY_EDITOR
    private void Update()
    {
        if (Time.time - timeStamp > 15)
        {
            timeStamp = Time.time;
            if (!IsInterstitialAdsReady())
                LoadInterstitial();
        }
    }
#endif
}
