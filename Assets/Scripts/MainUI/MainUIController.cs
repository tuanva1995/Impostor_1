  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
//using FalconSDK.Advertising;

public class MainUIController : MonoBehaviour
{
    public PlayerDataSO data;
    // Start is called before the first frame update
    public List<Transform> lsCharacter;
    public List<GameObject> lsCharSlide;
    public Transform charContent;
    public GameObject nextButton, preButton;
    private int charIndex = 0;
    [Header("Popup")]
    public FlagPopup flagPopup;
    public DailyGiftPopup dailyPopup;
    public FreeGiftPopup freeGiftPopup;
    public SettingPopup settingPopup;
    public UpgradeController upgrade;
    public MissionPopup missionPopup;
    //public FreeExChangePopup freeExChangePopup;
    public GameObject luckyWheel;
    public Text txtCoin,txtTime;
    public Text before, current, after;
    public GameObject btnTestGold;
    public GameObject panelTestLevel;
    bool isTestUI = false;
    void Awake()
    {
        btnTestGold.SetActive(isTestUI);
       // MusicManager.Instance.PlayBGMusic();
        txtCoin.text = DataController.Instance.playerData.Gold.ToString();
        DataController.Instance.UpdateMisson(1, DataController.Instance.playerData.Gold);
        DataController.Instance.UpdateMisson(2, UserData.NextLevel);
        SetUpLevel();
       // IronSourceManager.Instance.DesTroyBaner();
        luckyWheel = DataController.Instance.luckyWheel;
        if (PlayerPrefs.GetInt("IsFirstShowDaily") == 1 && DataController.Instance.isShowDaily)
        {
            OnDailyPopupShow();
             
        }
        charIndex = DataController.Instance.playerData.playerCurrentIndex-1;
        if (DataController.Instance.playerData.playerCurrentIndex>0)
            preButton.SetActive(true);
        if (DataController.Instance.playerData.playerCurrentIndex == lsCharacter.Count - 1)
            nextButton.SetActive(false);
        SlideCharacter(false, DataController.Instance.playerData.playerCurrentIndex);
    }
    bool isSlide = false;
    const int LEVEL_MAX = 50;
    private void Update()
    {
        if (DataController.Instance.playerData.timeCountDown > 0)
            txtTime.text = ConvertToTime((int)DataController.Instance.playerData.timeCountDown);
        else
            txtTime.text = "Get prize !";
    }
    string ConvertToTime(int time)
    {
        System.TimeSpan ts = System.TimeSpan.FromSeconds(time);
        string temp = ts.ToString();//time / 60 + ":" + time % 60;
        return temp;
    }
    public void OnButtonClock()
    {
        if (DataController.Instance.playerData.isRewardGiftOnClock)
        {
            //if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
            //{
            //    MessagePopup.Instance.ShowUp("Ads is not ready ! ", false, true);
            //    MessagePopup.Instance.SetOnButtonOK(() => { MessagePopup.Instance.Hide(); });
            //    return;
            //}
            //else
            //{
            //    IronSourceManager.Instance.ShowRewardedAds(() => {
            //        MessagePopup.Instance.ShowMessageItem("You get", -1, 100);
            //        MessagePopup.Instance.SetOnButtonOK(() =>
            //        {
            //            DataController.Instance.UpdateGold(100);
            //            DataController.Instance.playerData.timeCountDown = DataController.Instance.TIME_MAX;
            //            DataController.Instance.playerData.isRewardGiftOnClock = false;
            //            //MessagePopup.Instance.Hide();
            //        });
            //        FreeExChangePopup.Instance.Hide();
            //    },"Bonus Coin");
            //}
            
            
        }
    }
    void SetUpLevel()
    {
        if (UserData.NextLevel == 1)
        {
            before.text = "";
            current.text = UserData.NextLevel.ToString();
            after.text = (UserData.NextLevel + 1).ToString();
        }
        else if(UserData.NextLevel == LEVEL_MAX)
        {
            after.text = "";
            current.text = UserData.NextLevel.ToString();
            before.text = (UserData.NextLevel - 1).ToString();
        }
        else
        {
            before.text = (UserData.NextLevel - 1).ToString();
            current.text = UserData.NextLevel.ToString();
            after.text = (UserData.NextLevel + 1).ToString();
        }

    }
    void SlideCharacter(bool isLeft,int index=1)
    {
        
        //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        //isSlide = true;
        
        //if (isLeft)
        //{
        //    charContent.DOLocalMoveX(charContent.localPosition.x + 864.4f*index, 0.5f).OnComplete(()=> {
        //        isSlide = false;
        //    });
        //    charIndex--;
        //}
        //else
        //{
        //    charContent.DOLocalMoveX(charContent.localPosition.x - 864.4f * index, .5f).OnComplete(() => {
        //        isSlide = false;
        //    });
        //    charIndex++;
        //}
        //lsCharacter[charIndex].eulerAngles = new Vector3(0, 180, 0);
        //for (int i = 0; i < lsCharSlide.Count; i++)
        //{
        //    if (i == charIndex)
        //    {
        //        lsCharSlide[i].SetActive(true);
        //    }
        //    else
        //    {
        //        lsCharSlide[i].SetActive(false);
        //    }
            
        //}
        
        //upgrade.SetUp(charIndex);
        //Debug.Log("charIndex : " + charIndex + "- " + isLeft);
    }
    public void OnButtonNext()
    {
        preButton.SetActive(true);
        if (charIndex < lsCharacter.Count - 1&&!isSlide)
        {
            SlideCharacter(false);
        }
         if (charIndex == lsCharacter.Count - 1)
        {
            nextButton.SetActive(false);
        }
    }
    public void OnButtonPre()
    {

        nextButton.SetActive(true);
        if (charIndex >0 && !isSlide)
        {
            SlideCharacter(true);
        }

        if (charIndex == 0)
        {
            preButton.SetActive(false);
        }
    }
    public void OnFlagPopupShow()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        flagPopup.ShowPopup();
    }
    public void OnDailyPopupShow()
    {
       // MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        ///DailyGiftPopup.Instance.ShowPopup();
    }
    public void OnFreeGiftPopupShow()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        freeGiftPopup.ShowPopup();
    }
    public void OnSettingPopupShow()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        settingPopup.ShowPopup();
    }
    public void OnMissionPopupShow()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        missionPopup.ShowPopup();
    }
    public void OnFreeExPopupShow()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        FreeExChangePopup.Instance.ShowPopup();
    }
    public void OnLuckyPopupShow(bool isShow)
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        luckyWheel.SetActive(isShow);
    }
   
    public void OnButtonPlay()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (isTestUI)
        {
            panelTestLevel.SetActive(true);
            return;
        }
        Initiate.Fade("GamePlay", Color.black, 3.0f);
        //SceneManager.LoadScene("Map1");
    }

    public void btnGoldTest_Onclick()
    {
        DataController.Instance.playerData.Gold += 5000;
        txtCoin.text = DataController.Instance.playerData.Gold.ToString();

    }

   
}
