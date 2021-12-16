//using UnityEngine;
//using System.Collections;
//using GoogleMobileAds.Api;
//using UnityEngine.Advertisements;
//using System;
//public class GoogleAdsController : MonoBehaviour
//{
//#if UNITY_IPHONE
//	 string idAdMobInterstitial = "";
//	 string idAdMobBanner = "";
//	 string testDeviceId = "";
//#endif
 
//    string idAdMobVideo = "ca-app-pub-3940256099942544/5224354917";
//    string idAdMobInterstitial = "ca-app-pub-3940256099942544/1033173712";
//    string idAdMobBanner = "ca-app-pub-3940256099942544/6300978111";
//    string idAdMobBannerSquare = "ca-app-pub-3940256099942544/6300978111";

//    string testDeviceId_01 = "1C15CB8848218BDD08B01CCE7AE7AF90";
//    string testDeviceId_02 = "D9DE8F204E4E57737B89A7CD1402DBE8";


//    public static GoogleAdsController instance = null;
//    public InterstitialAd interstitial;
//    BannerView bannerView;
//    BannerView bannerViewSquare;

//    public RewardBasedVideoAd adsVideo;

//    float totalTimeLoadAds = 120;
//    int numFailVideo;
//    int numFailInters;
//    public bool isloadBanner = false;
//    bool testAds;
//    void Awake()
//    {
//         testAds = true;
//        if (!testAds)
//        {
//             idAdMobVideo = "ca-app-pub-8792509527786447/5450571720";
//             idAdMobInterstitial = "ca-app-pub-8792509527786447/4137490052";
//            //idAdMobBanner = " ca-app-pub-3117611650864637/2905186207";
//            //idAdMobVideo = "ca-app-pub-3940256099942544/5224354917";
//            idAdMobBanner = "ca-app-pub-8792509527786447/1702898400";
//            idAdMobBannerSquare = "ca-app-pub-8792509527786447/8076735069";
//        }
//        //Check if instance already exists
//        if (instance == null)
//        {
//            //if not, set instance to this
//            instance = this;
//        }
//        //If instance already exists and it's not this:
//        //else if (instance != this)
//        //    //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
//        //    Destroy(gameObject);

//        // //Sets this to not be destroyed when reloading scene
//        // DontDestroyOnLoad(gameObject);
//    }

//    // Use this for initialization
//    void Start()
//    {
        

//#if UNITY_ANDROID
//        string appId = "ca-app-pub-8792509527786447~7413555938";
//        if(!testAds){
//            appId = "ca-app-pub-3117611650864637~1975247915";
//        }
//#elif UNITY_IPHONE
//            //string appId = "ca-app-pub-3940256099942544~1458002511";
//#else
//            string appId = "unexpected_platform";
//#endif

//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(appId);
//        LoadFullAdmob();
//        if (isloadBanner)
//        {
//            // LoadBannerAdmod(true);
//        }
//        //LoadBannerAdmod();
//        LoadVideoAds();
//        LoadFullAdmob();
//        //StartCoroutine(CoShowFullAdmod());
//        //LoadBanerSquare();
//    }

//    public void LoadFullAdmob()
//    {
//        // Clean up interstitial ad before creating a new one.
//        if (this.interstitial != null)
//        {
//            if (interstitial.IsLoaded())
//            {
//                return;
//            }
//            this.interstitial.Destroy();
//        }
//        this.interstitial = new InterstitialAd(idAdMobInterstitial);
//        //this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
//        //this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
//        //this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
//        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
//        //this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

//        // Create an empty ad request.
//        //AdRequest request = new AdRequest.Builder().Build();
//        AdRequest request = new AdRequest.Builder()
//            .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
//            .AddTestDevice(testDeviceId_01)  // My test device.
//            .AddTestDevice(testDeviceId_02)
//            .Build();
//        // Load the interstitial with the request.
//        interstitial.LoadAd(request);
//    }

//    public IEnumerator CoShowFullAdmod()
//    {
//        yield return new WaitForSeconds(1);
//        LoadFullAdmob();
//        yield return new WaitForSeconds(2);
//        if (interstitial.IsLoaded())
//        {
//            interstitial.Show();
            
//        }
//        else
//        {
//            LoadFullAdmob();
//        }
//    }
//    public void ShowFullAdmod()
//    {
//        Debug.Log("LoadFullAds  ----"+ interstitial.IsLoaded());
//        if (interstitial.IsLoaded())
//        {
//            interstitial.Show();
//            LoadFullAdmob();
//        }
//        else
//            LoadFullAdmob();
//    }
//    public void LoadBanerSquare()
//    {
//        //return;
//        Debug.Log("LoadBanerSquare  ----");
//        // Clean up banner ad before creating a new one.
//        if (this.bannerViewSquare != null)
//        {
//            this.bannerViewSquare.Destroy();
//        }
       
//        this.bannerViewSquare = new BannerView(idAdMobBannerSquare, AdSize.MediumRectangle, AdPosition.Top);
       
//        // Register for ad events.
//        this.bannerViewSquare.OnAdLoaded += this.HandleAdLoaded;
//        this.bannerViewSquare.OnAdFailedToLoad += this.HandleAdFailedToLoad;
//        this.bannerViewSquare.OnAdOpening += this.HandleAdOpened;
//        this.bannerViewSquare.OnAdClosed += this.HandleAdClosed;
//        this.bannerViewSquare.OnAdLeavingApplication += this.HandleAdLeftApplication;

//        // Create an empty ad request.
//        AdRequest requestBanner = new AdRequest.Builder()
//            //.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
//            .AddTestDevice(testDeviceId_01)  // My test device.
//            .AddTestDevice(testDeviceId_02)
//            .Build();
//        // Load the banner with the request.
//        bannerViewSquare.LoadAd(requestBanner);
//    }
//    public void LoadBannerAdmod(bool isLoadHeader = false)
//    {
//        Debug.Log("Load banner ----");
//        // Clean up banner ad before creating a new one.
//        if (this.bannerView != null)
//        {
//            this.bannerView.Destroy();
//        }
//        // Create a 320x50 banner at the top of the screen.
//        if (isLoadHeader)
//        {
//            this.bannerView = new BannerView(idAdMobBanner, AdSize.Banner, AdPosition.Top);
//        }
//        else
//        {
//            this.bannerView = new BannerView(idAdMobBanner, AdSize.Banner, AdPosition.Bottom);
//        }
//        // if (this.bannerView == null)
//        // {
//        //     this.bannerView = new BannerView(idAdMobBanner, AdSize.SmartBanner, AdPosition.Bottom);
//        // }

//        // Register for ad events.
//        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
//        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
//        this.bannerView.OnAdOpening += this.HandleAdOpened;
//        this.bannerView.OnAdClosed += this.HandleAdClosed;
//        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

//        // Create an empty ad request.
//        AdRequest requestBanner = new AdRequest.Builder()
//            //.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
//            .AddTestDevice(testDeviceId_01)  // My test device.
//            .AddTestDevice(testDeviceId_02)
//            .Build();
//        // Load the banner with the request.
//        bannerView.LoadAd(requestBanner);
//    }
//    public void ShowBanner()
//    {
//       // LoadBannerAdmod();
//        this.bannerViewSquare.Show();
//    }
//    public void HideBanner()
//    {
//        if (this.bannerView != null)
//        {
//            this.bannerView.Hide();
//            this.bannerView.Destroy();
            
//        }
//        //LoadBanerSquare();
//        //LoadBannerAdmod(true);
//    }
//    public void HideBannerSquare()
//    {
//        if (this.bannerViewSquare != null)
//        {
//            this.bannerViewSquare.Hide();
//            this.bannerViewSquare.Destroy();
            
//        }
//        //LoadBanerSquare();
//        //LoadBannerAdmod(true);
//    }

//    public void LoadVideoAds()
//    {
//        if (adsVideo == null)
//        {
//            adsVideo = RewardBasedVideoAd.Instance;
//        }
//        else
//        {
//            if (adsVideo.IsLoaded())
//            {
//                return;
//            }
//        }
//        adsVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
//        adsVideo.OnAdClosed += HandleRewardedAdClosed;
//        adsVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
//        adsVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
//        // Create an empty ad request.
//        //AdRequest request = new AdRequest.Builder().Build();
//        AdRequest request = new AdRequest.Builder()
//        .AddTestDevice(testDeviceId_01)
//        .AddTestDevice(testDeviceId_02)
//        .Build();
//        // Load the rewarded video ad with the request.
//        this.adsVideo.LoadAd(request, idAdMobVideo);
//    }

//    public void ShowVideoAds()
//    {
//        if (adsVideo.IsLoaded())
//        {
//            adsVideo.Show();
//            LoadVideoAds();
//        }
//        else
//        {
//            LoadVideoAds();
//        }
//    }
//    public System.Action evt; // sự kiện khi xem xong video quảng cáo video.
//    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
//    {
//        evt();
//        evt = null;
//        this.LoadVideoAds();

//        Debug.Log("is play video ads ");
//        string type = args.Type;
//        double amount = args.Amount;
//        MonoBehaviour.print(
//            "HandleRewardBasedVideoRewarded event received for"
//                        + amount.ToString() + " " + type);
//    }
//    public void HandleRewardedAdClosed(object sender, EventArgs args)
//    {
//        this.LoadVideoAds();
//        MonoBehaviour.print("HandleRewardedAdClosed event received");
//    }
//    // public void HandleOnAdLoaded(object sender, EventArgs args) // load thanh cong inters
//    // {
//    //     numFailInters = 0;
//    //     MonoBehaviour.print("HandleAdLoaded event received");
//    // }
//    // public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) // load failed inters
//    // {
//    //     numFailInters +=1;
//    // }
//    // public void HandleOnAdClosed(object sender, EventArgs args) // ham goi khi tat quang cao full man
//    // {
//    //     LoadFullAdmob();
//    //     StaticData.timeLoadAds = Commons.ConvertToTimestamp(DateTime.Now);
//    //     MonoBehaviour.print("HandleAdClosed event received");
//    // }
//    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    { // load failed video

//    }
//    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args) // load thanh cong video
//    {

//    }


//    // Event Ads Load Inters
//    public void HandleInterstitialLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleInterstitialLoaded event received");
//    }

//    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print(
//            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
//    }

//    public void HandleInterstitialOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleInterstitialOpened event received");
//    }
//    public void HandleInterstitialClosed(object sender, EventArgs args)
//    {
//        LoadFullAdmob();
//        MonoBehaviour.print("HandleAdClosed event received");
//        MonoBehaviour.print("HandleInterstitialClosed event received");
//    }

//    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
//    }
//    #region Banner callback handlers

//    public void HandleAdLoaded(object sender, EventArgs args)
//    {
//        //ShowBanner();
//        MonoBehaviour.print("HandleAdLoaded event received");
//    }

//    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
//    }

//    public void HandleAdOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdOpened event received");
//    }

//    public void HandleAdClosed(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdClosed event received");
//    }

//    public void HandleAdLeftApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdLeftApplication event received");
//    }

//    #endregion


//}