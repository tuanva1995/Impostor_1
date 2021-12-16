//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActiveItemButton : MonoBehaviour
{
    public ItemKind itemKind;
    public Image slide;
    public Text txtItemAmount;
    public GameObject watchVideo;

    protected ItemData item;
    protected float timeActive,itemTime;
    protected bool isActivating;

    protected void Start()
    {
        item = GameController.Instance.GetItemData(itemKind);
        SetItemStatus();
    }

    protected void LateUpdate()
    {
        if(isActivating && item.itemTime != 0)
        {
            slide.fillAmount -= Time.deltaTime / itemTime;
            timeActive += Time.deltaTime;
        }
        if(isActivating && slide.fillAmount == 0)
        {
            slide.fillAmount = 1;
            isActivating = false;
        }
    }

    public void ActiveItem()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        //ActiveActivity();
        //if (DataController.Instance.GetItem((int)item.kindItem) > 0)
        //{
        //    DataController.Instance.UpdateItems((int)item.kindItem, -1);
        //    ActiveActivity();
        //}
        //else
        //{

        //    //GoogleAdsController.instance.ShowVideoAds();
        //    //GoogleAdsController.instance.evt = () =>
        //    //{
        //    //    ActiveActivity();
        //    //};
        //    if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
        //    {
        //        MessagePopup.Instance.ShowUp("Ads is not ready ! ", false, true);
        //        MessagePopup.Instance.SetOnButtonOK(() => { MessagePopup.Instance.Hide(); });
               
        //    }
        //    else
        //        IronSourceManager.Instance.ShowRewardedAds(() => {
        //        ActiveActivity();
        //    }, "Button Item",DataController.Instance.playerData.levelCurrent
        //   );
        //}
    }

    protected virtual void ActiveActivity()
    {
        if (isActivating)
        {
            slide.fillAmount = 1f;
            itemTime += item.itemTime - timeActive;
        }
        else
        {
            itemTime = item.itemTime;
            isActivating = true;
        }
        GameController.Instance.SetItemData(item);
        EventDisPatcher.Instance.PostEvent(GetEvent());

        SetItemStatus();
    }
    protected void SetItemStatus()
    {
        if (DataController.Instance.GetItem((int)item.kindItem) > 0)
            txtItemAmount.text = DataController.Instance.GetItem((int)item.kindItem).ToString();
        else
        {
            txtItemAmount.text = "";
            watchVideo.SetActive(true);
        }
    }
    private EventID GetEvent()
    {
        switch(itemKind)
        {
            case ItemKind.Heal: return EventID.OnUseHeal; 
            case ItemKind.Shield: return EventID.OnUseShield; 
            case ItemKind.Strength: return EventID.OnUseStrength; 
            default: return EventID.None;
        }
    }
}
