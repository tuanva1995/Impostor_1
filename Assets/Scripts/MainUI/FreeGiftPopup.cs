//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGiftPopup : SimplePopup
{
    //public FreeExChangePopup freeExChangePopup;
    public FreeGiftItem[] lsItem;
    protected override void Start()
    {
        base.Start();
        Setup();
    }
    public void Setup()
    {
        for(int i = 0; i < lsItem.Length; i++)
        {
            lsItem[i].numberOfitem.text = DataController.Instance.baseData.ListGiftInFreeGift[i].ToString();
            if (i == DataController.Instance.playerData.FreeGiftIndex)
            {
                lsItem[i].cover.SetActive(false);
                lsItem[i].stick.SetActive(false);
            }
            else if (i < DataController.Instance.playerData.FreeGiftIndex)
            {
                lsItem[i].cover.SetActive(true);
                lsItem[i].stick.SetActive(true);
            }
            else
            {
                lsItem[i].cover.SetActive(true);
                lsItem[i].stick.SetActive(false);
            }
        }
        
    }
    public void OnButtonClaim(int index)
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        //if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    MessagePopup.Instance.ShowUp("Ads is not ready ! ",false,true);
        //    MessagePopup.Instance.SetOnButtonOK(() => { MessagePopup.Instance.Hide(); });
        //    return;
        //}
        //IronSourceManager.Instance.ShowRewardedAds(() => {

        //    FreeExChangePopup.Instance.ShowUp();
        //    FreeExChangePopup.Instance.SetUp(index);
        //    FreeExChangePopup.Instance.SetOnButtonYes(() =>
        //    {
        //        IronSourceManager.Instance.ShowRewardedAds(() => {
        //            MessagePopup.Instance.ShowMessageItem("You get", index - 1, 2 * DataController.Instance.baseData.ListGiftInFreeGift[index]);
        //            MessagePopup.Instance.SetOnButtonOK(() =>
        //            {
        //                if (index == 0)
        //                {
        //                    DataController.Instance.UpdateGold(2 * DataController.Instance.baseData.ListGiftInFreeGift[index]);
        //                }
        //                else
        //                {
        //                    DataController.Instance.UpdateItems(index - 1, 2 * DataController.Instance.baseData.ListGiftInFreeGift[index]);
        //                }
        //                //MessagePopup.Instance.Hide();
        //            });
        //            FreeExChangePopup.Instance.Hide();
        //        },"Free Gift");
               
        //    });
        //    FreeExChangePopup.Instance.SetOnButtonNo(() =>
        //    {
        //        //IronSourceManager.Instance.ShowRewardedAds(() => {
        //            MessagePopup.Instance.ShowMessageItem("You get", index - 1, DataController.Instance.baseData.ListGiftInFreeGift[index]);
        //            MessagePopup.Instance.SetOnButtonOK(() =>
        //            {
        //                if (index == 0)
        //                {
        //                    DataController.Instance.UpdateGold(DataController.Instance.baseData.ListGiftInFreeGift[index]);
        //                }
        //                else
        //                {
        //                    DataController.Instance.UpdateItems(index - 1, DataController.Instance.baseData.ListGiftInFreeGift[index]);
        //                }
        //               // MessagePopup.Instance.Hide();
        //            });
        //        FreeExChangePopup.Instance.Hide();
        //        });
                
        //   // });
        //    DataController.Instance.playerData.FreeGiftIndex++;
        //    Setup();
        //});
       
    }

}
