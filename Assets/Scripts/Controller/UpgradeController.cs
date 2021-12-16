//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeController : MonoBehaviour
{
    public DataPlayer playerData;
    public Text playerName;
    public Image dameImg, hpImg;
    public GameObject upgradePanel, unlockPanel;
    public int LEVEL_MAX = 20;
    public UIButtonSimple btnUpgrade,btnBuy, btnTry;
    public ParticleSystem upgradeEffect, buyEffect;
    public Text UpgradeTxt,buyTxt;
    // Start is called before the first frame update
    void Awake()
    {
        //DataController.Instance.playerData.Gold = 3500;
        playerData = DataController.Instance.baseData;
        //SetUp(0);
    }
    public void SetUp(int index)
    {
        if(DataController.Instance.playerData.lsUnlock[index]== (int)StatePlayerItem.LOCK)
        {
            upgradePanel.SetActive(false);
            unlockPanel.SetActive(true);
            btnBuy.onPress.RemoveAllListeners();
            btnBuy.onPress.AddListener(() => {
                OnButtonBuy(index);
            });
            btnTry.onPress.RemoveAllListeners();
            btnTry.onPress.AddListener(() => {
                OnButtonTry(index);
            });
        }
        else
        {
            DataController.Instance.playerData.playerCurrentIndex = index;
            DataController.Instance.playerData.playerDameCurrentIndex = playerData.datas[index].dame*
                ((DataController.Instance.playerData.LsPlayerDame[index]+ DataController.Instance.playerData.LsPlayerEXP[index])*0.05f+1)
                ;
            DataController.Instance.playerData.playerHPCurrentIndex = (0.05f*playerData.datas[index].Hp* DataController.Instance.playerData.LsPlayerEXP[index])+ playerData.datas[index].Hp ;
            upgradePanel.SetActive(true);
            unlockPanel.SetActive(false);
            dameImg.fillAmount = DataController.Instance.playerData.LsPlayerDame[index] / 20;
            hpImg.fillAmount = DataController.Instance.playerData.LsPlayerEXP[index] / 20;
        }
        playerName.text = playerData.datas[index].name;
        btnUpgrade.onPress.RemoveAllListeners();
        btnUpgrade.onPress.AddListener(() => {
            OnButtonUpgrade( index);
        });
        buyTxt.text = playerData.datas[index].Price.ToString(); ;
        if (DataController.Instance.playerData.LsPlayerDame[index] == 20&& DataController.Instance.playerData.LsPlayerEXP[index] == 20)
        {
            btnUpgrade.gameObject.SetActive(false);
           
        }
        else
        {
            btnUpgrade.gameObject.SetActive(true);
            UpgradeTxt.text = (500 + 20 * (DataController.Instance.playerData.LsPlayerDame[index] + DataController.Instance.playerData.LsPlayerEXP[index]-2)).ToString();
        }
        DataController.Instance.SaveData();
    }
    public void OnButtonUpgrade(int index)
    {
        //SoundController.Instance.PlaySingle(FXSound.Instance.button1);
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (DataController.Instance.playerData.Gold < int.Parse(UpgradeTxt.text))
        {
            Debug.Log("Mess");
            MessagePopup.Instance.ShowUp("Not enough coin !", false, true);
            return;
        }
        else
        {
            DataController.Instance.UpdateGold(-int.Parse(UpgradeTxt.text));
        }
        if (DataController.Instance.playerData.LsPlayerDame[index] < 20&& DataController.Instance.playerData.LsPlayerEXP[index] < 20)
        {
            MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxGetItem);
            int rd = Random.Range(0, 2);
            if (rd == 0)
            {
                DataController.Instance.playerData.LsPlayerDame[index]++;

            }
            else
            {
                DataController.Instance.playerData.LsPlayerEXP[index]++;
            }
            upgradeEffect.Play();
        }
        else if (DataController.Instance.playerData.LsPlayerDame[index] < 20)
        {
            DataController.Instance.playerData.LsPlayerDame[index]++;
            upgradeEffect.Play();
        }
        else if(DataController.Instance.playerData.LsPlayerEXP[index]<20)
        {
            DataController.Instance.playerData.LsPlayerEXP[index]++;
            upgradeEffect.Play();
        }
        
        
        SetUp(index);
        DataController.Instance.SaveData();
    }
    public void OnButtonBuy(int index)
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (DataController.Instance.playerData.Gold < int.Parse(buyTxt.text))
        {
            MessagePopup.Instance.ShowUp("Not enough coin !",false,true);
            return;
        }
        else
        {
            DataController.Instance.UpdateGold(-int.Parse(buyTxt.text));
        }
        buyEffect.Play();
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxEatItem);
        DataController.Instance.playerData.lsUnlock[index] = (int)StatePlayerItem.UNLOCKED;
        DataController.Instance.playerData.LsPlayerDame[index] = 1;
        DataController.Instance.playerData.LsPlayerEXP[index] = 1;
        SetUp(index);
        DataController.Instance.SaveData();
       
    }
    public void OnButtonTry(int index)
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        //if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    MessagePopup.Instance.ShowUp("Ads is not ready ! ", false, true);
        //    MessagePopup.Instance.SetOnButtonOK(() => { MessagePopup.Instance.Hide(); });
        //    return;
        //}
        //else
        //{
        //    IronSourceManager.Instance.ShowRewardedAds(() => {
        //        DataController.Instance.testSkinID = index;
        //        Initiate.Fade("TestSkinScene", Color.black, 3.0f);
        //    }, "Try Character");
        //}
            
        //if (GoogleAdsController.instance.adsVideo.IsLoaded())
        //{
        //    GoogleAdsController.instance.ShowVideoAds();
        //    GoogleAdsController.instance.evt = () => {
        //        DataController.Instance.testSkinID = index;
        //        Initiate.Fade("TestSkinScene", Color.black, 3.0f);
        //    };
           
        //}
        
    }
}
