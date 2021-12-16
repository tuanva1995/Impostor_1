//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionPopup : SimplePopup
{
    public MissionItem[] lsMissionItem;
    public Transform ContentItems;
    public Sprite rewardOn, rewardOff;
    public MissionData[] lsMissionData;
   
    protected override void Start()
    {
        //PopupManager.Instance.mission = this;
        base.Start();
        //lsMissionData = DataController.Instance.lsMission;
        demReward = 0;
        for (int i = 0; i < lsMissionData.Length; i++)
        {

            SetUpItem(lsMissionItem[i], i);

        }
    }
    int demReward = 0;
    public void SetUpItem(MissionItem data,int index)
    {
        if (lsMissionData[index].isComplete)
        {
            data.gameObject.SetActive(false);
            return;
        }
        if (DataController.Instance.playerData.lsMissionRewarded[index])
        {
            data.button.GetComponent<Image>().sprite = rewardOn;
            data.isReward = true;
            demReward++;
            //UIMainSceneController.Instance.iconStick.SetActive(true);
        }
        else
        {
            data.button.GetComponent<Image>().sprite = rewardOff;
            data.isReward = false;
        }
        int a = index;
        data.button.onPress.RemoveAllListeners();
        data.button.onPress.AddListener(() => {
            if (DataController.Instance.playerData.lsMissionRewarded[a])
            {
                Debug.Log("aaa");
                int coin = lsMissionData[a].lsReward[DataController.Instance.playerData.lsMissionLevel[a]];

                //if (IronSourceManager.Instance.isVideoAvail)
                //{
                //    FreeExChangePopup.Instance.ShowUp();
                //    FreeExChangePopup.Instance.SetOnButtonYes(() => {
                //        IronSourceManager.Instance.ShowRewardedAds(() => {
                //            coin = coin * 2;
                //            MessagePopup.Instance.ShowMessageItem("You get ", -1, coin);
                //            MessagePopup.Instance.SetOnButtonOK(() => {

                //                //MessagePopup.Instance.Hide();
                //            });
                //        }, "Mission");
                //        FreeExChangePopup.Instance.Hide();
                //    });
                //    FreeExChangePopup.Instance.SetOnButtonNo(() => {
                //        MessagePopup.Instance.ShowMessageItem("You get ", -1, coin);
                //        MessagePopup.Instance.SetOnButtonOK(() => {

                //            //MessagePopup.Instance.Hide();
                //        });
                //        FreeExChangePopup.Instance.Hide();
                //    });
                //}
                //else
                //{
                //    MessagePopup.Instance.ShowMessageItem("You get ", -1, coin);
                //    MessagePopup.Instance.SetOnButtonOK(() => {

                //        //MessagePopup.Instance.Hide();
                //    });
                //}
                DataController.Instance.playerData.lsMissionLevel[a]++;
                data.isReward = DataController.Instance.playerData.lsMissionRewarded[a] = false;
                demReward--;
                Debug.Log(demReward);
                DataController.Instance.UpdateGold(coin);
                
                DataController.Instance.UpdateMisson(index, 0);
                //lsMissionData[index].UpdateProcess(0);
                SetUpItem(data, index);

            }
            else
            {
                data.button.GetComponent<Image>().sprite = rewardOff;
                //data.isReward = false;
            }


        });
        data.SetUp(lsMissionData[index].name, DataController.Instance.playerData.lsMissionProcess[index], lsMissionData[index].lsTarget[DataController.Instance.playerData.lsMissionLevel[index]],
            lsMissionData[index].lsReward[DataController.Instance.playerData.lsMissionLevel[index]], lsMissionData[index].iconSpr);
    }
    public override void ShowPopup(AnimationPopupType type = AnimationPopupType.OnTopDown)
    {
        demReward = 0;
        for (int i = 0; i < lsMissionData.Length; i++)
        {
           
            SetUpItem(lsMissionItem[i], i);

        }
        base.ShowPopup(type);
    }
    bool isGetReward = false;
    public void OnButtonGetDailyMission()
    {
       
    }

}
