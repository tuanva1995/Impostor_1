//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyGiftPopup : SimplePopup
{
    public static DailyGiftPopup Instance;
    public DailyItem[] lsDailyItem;
    public Sprite[] lsIconSpr;
    public Image Claim;
    //public FreeExChangePopup freeExChangePopup;
    private void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {

        base.Start();
    }
    public override void ShowPopup(AnimationPopupType type = AnimationPopupType.OnTopDown)
    {
        DataController.Instance.isShowDaily = false;
        SetUp();
        base.ShowPopup(type);
    }
    public void SetUp()
    {
        if (DataController.Instance.playerData.isDailyReward)
        {
            Claim.gameObject.SetActive(true);
            
        }
        else
        {
            Claim.gameObject.SetActive(false);
        }
        for (int i = 0; i < lsDailyItem.Length; i++)
        {
            lsDailyItem[i].dayTxt.text = "Day " + (i + 1);
            lsDailyItem[i].icon.sprite = lsIconSpr[i];
            lsDailyItem[i].coinRecvTxt.text = DataController.Instance.baseData.ListGiftInDailyGift[i].ToString();
            if (i < DataController.Instance.playerData.CurrentDailyGiftRevc)
            {
                lsDailyItem[i].stick.SetActive(true);
                lsDailyItem[i].cover.SetActive(true);
            }
            else
            {
                lsDailyItem[i].stick.SetActive(false);
                lsDailyItem[i].cover.SetActive(false);
            }
            if (i == lsDailyItem.Length-1)
                return;
            if (DataController.Instance.playerData.CurrentDailyGiftRevc == i)
            {
                lsDailyItem[i].holder.SetActive(true);
            }
            else
            {
                lsDailyItem[i].holder.SetActive(false);
            }
        }
    }
    double coinRecv;
    public void OnButtonClaim()
    {
        if (DataController.Instance.playerData.isDailyReward)
        {
            Claim.gameObject.SetActive(true);
            return;
        }
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);

        switch (DataController.Instance.playerData.CurrentDailyGiftRevc)
        {
            case 0:
                coinRecv = 200;
                break;
            case 1:
                coinRecv = 200;
                break;
            case 2:
                coinRecv = 200;
                break;
            case 3:
                coinRecv = 200;
                break;
            case 4:
                coinRecv = 200;
                break;
            case 5:
                coinRecv = 200;
                break;
            case 6:
                coinRecv = 200;
                break;
        }
        //if (IronSourceManager.Instance.isVideoAvail)
        //{
        //    FreeExChangePopup.Instance.ShowUp();
        //    FreeExChangePopup.Instance.SetUp(0);
        //    FreeExChangePopup.Instance.SetOnButtonYes(() => {

        //        IronSourceManager.Instance.ShowRewardedAds(() => {
        //            MessagePopup.Instance.ShowMessageItem("You get ", -1, DataController.Instance.baseData.ListGiftInDailyGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //            MessagePopup.Instance.SetOnButtonOK(() => {
        //                if (DataController.Instance.playerData.CurrentDailyGiftRevc < 6)
        //                {
        //                    DataController.Instance.UpdateGold(DataController.Instance.baseData.ListGiftInFreeGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //                }
        //                else
        //                {
        //                    DataController.Instance.UpdateItems(0, DataController.Instance.baseData.ListGiftInDailyGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //                }
        //                //MessagePopup.Instance.Hide();
        //            });
        //        }, "Daily");

        //        FreeExChangePopup.Instance.Hide();

        //    });
        //    FreeExChangePopup.Instance.SetOnButtonNo(() => {
        //        MessagePopup.Instance.ShowMessageItem("You get ", -1, DataController.Instance.baseData.ListGiftInDailyGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //        MessagePopup.Instance.SetOnButtonOK(() => {
        //            if (DataController.Instance.playerData.CurrentDailyGiftRevc < 6)
        //            {
        //                DataController.Instance.UpdateGold(DataController.Instance.baseData.ListGiftInFreeGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //            }
        //            else
        //            {
        //                DataController.Instance.UpdateItems(0, DataController.Instance.baseData.ListGiftInDailyGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //            }
        //            //MessagePopup.Instance.Hide();
        //        });
        //        FreeExChangePopup.Instance.Hide();
        //    });
        //}
        //else
        //{

        //    //GoogleAdsController.instance.LoadVideoAds();
        //    MessagePopup.Instance.ShowMessageItem("You get ", -1, DataController.Instance.baseData.ListGiftInDailyGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //    MessagePopup.Instance.SetOnButtonOK(() => {
        //        if (DataController.Instance.playerData.CurrentDailyGiftRevc < 6)
        //        {
        //            DataController.Instance.UpdateGold(DataController.Instance.baseData.ListGiftInFreeGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //        }
        //        else
        //        {
        //            DataController.Instance.UpdateItems(0, DataController.Instance.baseData.ListGiftInDailyGift[DataController.Instance.playerData.CurrentDailyGiftRevc]);
        //        }
        //        //MessagePopup.Instance.Hide();
        //    });
        //}
        if (DataController.Instance.playerData.CurrentDailyGiftRevc < 6)
            DataController.Instance.playerData.CurrentDailyGiftRevc++;
        else
            DataController.Instance.playerData.CurrentDailyGiftRevc=0;
        DataController.Instance.playerData.isDailyReward = true;
        DataController.Instance.SaveData();
        SetUp();
        //Claim.gameObject.SetActive(true);
    }
}
