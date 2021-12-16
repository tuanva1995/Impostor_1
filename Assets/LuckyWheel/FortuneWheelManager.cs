using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FortuneWheelManager : MonoBehaviour
{
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public Button AdsButton;
    public GameObject Circle; 			// Rotatable Object with rewards
    public Text CoinsDeltaText; 		// Pop-up text with wasted or rewarded coins amount
    public Text CurrentCoinsText; 		// Pop-up text with wasted or rewarded coins amount
    public int TurnCost = 300;			// How much coins user waste when turn whe wheel
    //public int CurrentCoinsAmount = 1000;	// Started coins amount. In your project it can be set up from CoinsManager or from PlayerPrefs and so on
    public double PreviousCoinsAmount;		// For wasted coins animation

    private void Awake ()
    {
        
        
    }
    private void OnEnable()
    {
        CheckShowButton();
        PreviousCoinsAmount = DataController.Instance.GetGold();
        CurrentCoinsText.text = DataController.Instance.GetGold().ToString();
    }
    bool isAdsTurn;
    public void TurnWheel ()
    {
    	// Player has enough money to turn the wheel
        if (DataController.Instance.GetGold() >= TurnCost || isAdsTurn) {
    	    _currentLerpRotationTime = 0f;
    	
    	    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
    	    _sectorsAngles = new float[] { 45, 90, 135, 180, 225, 270, 315, 360 };
    	
    	    int fullCircles = 5;
    	    float randomFinalAngle = _sectorsAngles [UnityEngine.Random.Range (0, _sectorsAngles.Length)];
    	
    	    // Here we set up how many circles our wheel should rotate before stop
    	    _finalAngle = -(fullCircles * 360 + randomFinalAngle);
    	    _isStarted = true;
    	
    	    PreviousCoinsAmount = DataController.Instance.GetGold();

            // Decrease money for the turn
            if (!isAdsTurn)
            {
                DataController.Instance.UpdateGold(-TurnCost);
            }

            // Show wasted coins
            CoinsDeltaText.text = "-" + TurnCost;
    	    CoinsDeltaText.gameObject.SetActive (false);
    	
    	    // Animate coins
    	    StartCoroutine (HideCoinsDelta ());
    	    StartCoroutine (UpdateCoinsAmount ());
            CheckShowButton();
            isAdsTurn = false;
        }
    }

    public void AdsButton_Onclick()
    {

        //if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    MessagePopup.Instance.ShowUp("Ads is not ready ! ", false, true);
        //    MessagePopup.Instance.SetOnButtonOK(() => { MessagePopup.Instance.Hide(); });
        //    return;
        //}
        
        //    //GoogleAdsController.instance.ShowVideoAds();
        //    //GoogleAdsController.instance.evt = () => {
        //    //    isAdsTurn = true;
        //    //    TurnWheel();
        //    //};
        //    IronSourceManager.Instance.ShowRewardedAds(()=> {
        //    isAdsTurn = true;
        //    TurnWheel();
        //}, "Wheel");
        //AdsButton.gameObject.SetActive(false);
    }

    void CheckShowButton()
    {
        //if(DataController.Instance.GetGold() >= TurnCost)
        //{
        //    TurnButton.gameObject.SetActive(true);
        //    TurnButton.interactable = true;
        //    AdsButton.gameObject.SetActive(false);
        //}
        //else
        //{
        //    if (IronSourceManager.Instance.isVideoAvail )
        //    {
        //        TurnButton.gameObject.SetActive(false);
        //        AdsButton.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        TurnButton.gameObject.SetActive(true);
        //        TurnButton.interactable = false;
        //        AdsButton.gameObject.SetActive(false);
        //    }
            
        //}
    }

    private void GiveAwardByAngle ()
    {
        Debug.Log("_startAngle: " + _startAngle);
    	// Here you can set up rewards for every sector of wheel
    	switch ((int)_startAngle) {
    	case 0:
    	    RewardCoins (100);
    	    break;
    	case -315:
            RewardItems(0,1);
    	    break;
    	case -270:
    	    RewardCoins (200);
    	    break;
    	case -225:
                RewardItems(1, 2);
                break;
    	case -180:
    	    RewardCoins (300);
    	    break;
    	case -135:
                RewardItems(2, 1);
                break;
    	case -90:
    	    RewardCoins (500);
    	    break;
    	case -45:
                RewardItems(3, 2);
                break;
    	default:
    	    RewardCoins (300);
    	    break;
        }
    }

    void Update ()
    {
        // Make turn button non interactable if user has not enough money for the turn
    	if (_isStarted || DataController.Instance.GetGold() < TurnCost) {
    	    TurnButton.interactable = false;
    	    TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
    	} else {
    	    TurnButton.interactable = true;
    	    TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
    	}

    	if (!_isStarted)
    	    return;

    	float maxLerpRotationTime = 4f;
    
    	// increment timer once per frame
    	_currentLerpRotationTime += Time.deltaTime;
    	if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.localEulerAngles.z == _finalAngle) {
    	    _currentLerpRotationTime = maxLerpRotationTime;
    	    _isStarted = false;
    	    _startAngle = _finalAngle % 360;
    
    	    GiveAwardByAngle ();
    	    StartCoroutine(HideCoinsDelta ());
            CheckShowButton();

        }
    
    	// Calculate current position using linear interpolation
    	float t = _currentLerpRotationTime / maxLerpRotationTime;
    
    	// This formulae allows to speed up at start and speed down at the end of rotation.
    	// Try to change this values to customize the speed
    	t = t * t * t * (t * (6f * t - 15f) + 10f);
    
    	float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
    	Circle.transform.localEulerAngles = new Vector3 (0, 0, angle);
    }

    private void RewardCoins (int awardCoins)
    {
        rewardIndex = 0;
        //if (IronSourceManager.Instance.isVideoAvail)
        //{
        //    ShowPopupAds(() =>
        //    {
        //        setRewardCoins(awardCoins);
        //    }, () =>
        //    {
        //        setRewardCoins(awardCoins*2);
        //    });
        //}
        //else
        //{
        //    setRewardCoins(awardCoins);
        //}
        
    }
    void setRewardCoins(int awardCoins)
    {
        MessagePopup.Instance.ShowMessageItem("You get ", -1, awardCoins);
        MessagePopup.Instance.SetOnButtonOK(() => {
            DataController.Instance.UpdateGold(awardCoins);
            CoinsDeltaText.text = "+" + awardCoins;
            CoinsDeltaText.gameObject.SetActive(false);
            StartCoroutine(UpdateCoinsAmount());
            DontDestroy.Instance.StarEffectCoin(Circle.transform.position, CurrentCoinsText.transform.position);
        });
        
    }
    private void RewardItems(int indexItem, int numItem)
    {
        rewardIndex = indexItem+1 ;
        //if (IronSourceManager.Instance.isVideoAvail)
        //{
        //    ShowPopupAds(() =>
        //    {
        //        setRewardItem(indexItem, numItem);
        //    }, () =>
        //    {
        //        setRewardItem(indexItem, numItem * 2);
        //    });
        //}
        //else
        //{
        //    setRewardItem(indexItem, numItem);
        //}
    }
    void setRewardItem(int indexItem, int numItem)
    {
        MessagePopup.Instance.ShowMessageItem("You get", indexItem , numItem);
        MessagePopup.Instance.SetOnButtonOK(() =>
        {
            CoinsDeltaText.text = "+" + numItem;
            CoinsDeltaText.gameObject.SetActive(false);
            DataController.Instance.UpdateItems(indexItem, numItem);
        });
        
    }

    private IEnumerator HideCoinsDelta()
    {
        yield return new WaitForSeconds (1f);
	CoinsDeltaText.gameObject.SetActive (false);
    }

    private IEnumerator UpdateCoinsAmount ()
    {
    	// Animation for increasing and decreasing of coins amount
    	const float seconds = 0.5f;
    	float elapsedTime = 0;
    
    	while (elapsedTime < seconds) {
    	    CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp ((float)PreviousCoinsAmount, (float)DataController.Instance.GetGold(), (elapsedTime / seconds))).ToString ();
    	    elapsedTime += Time.deltaTime;
    
    	    yield return new WaitForEndOfFrame ();
        }
    
    	PreviousCoinsAmount = DataController.Instance.GetGold();
    	CurrentCoinsText.text = DataController.Instance.GetGold().ToString ();
    }
    int rewardIndex;
    void ShowPopupAds(System.Action evtNo, System.Action evtYes)
    {
        FreeExChangePopup.Instance.ShowUp();
        FreeExChangePopup.Instance.SetUp(rewardIndex);
        //FreeExChangePopup.Instance.SetOnButtonYes(() => {

        //    //IronSourceManager.Instance.ShowRewardedAds(() => {
        //    //    evtYes();
        //    //}, "Wheel");

        //    FreeExChangePopup.Instance.Hide();

        //});
        FreeExChangePopup.Instance.SetOnButtonNo(() => {

            evtNo();

        });
        //MessagePopup.Instance.ShowUp("Do you want to double the reward?", true, false);
        //MessagePopup.Instance.SetOnButtonNo(() =>
        //{
        //    evtNo();
        //});

        //MessagePopup.Instance.SetOnButtonYes(()=> {
        //    IronSourceManager.Instance.ShowRewardedAds(()=> {
        //        evtYes();
        //    });
        //});
    }
    public void OnClose()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GamePlay")
        {
            LevelManager.ChangeToNextLevel();
           // IronSourceManager.Instance.ShowInterstitialAd("Next Level", DataController.Instance.playerData.levelCurrent);
        }
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }
}
