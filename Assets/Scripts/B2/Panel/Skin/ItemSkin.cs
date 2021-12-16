using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSkin : MonoBehaviour
{
    public int idSkin;
    DataSkinModel model;
    [SerializeField] GameObject buttonRemove, buttonAds, buttonBuy, buttonUse, buttonSpin, butonShop;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] TextMeshProUGUI textCountAds;
    [SerializeField] Image imageSkin;

   
    public void btnRemove_Onclick()
    {
        DataManager.Instance.dataSkin.idCurrentSkin = 0;
        UpdateItem();
    }
    public void btnAds_Onclick()
    {
        IronSourceAdsController.Instance.ShowVideoAds(AdsComplete, UpdateItem);
        
    }
    void AdsComplete()
    {
        model.CountViewAds += 1;
        if (model.CountViewAds >= model.amountAds)
        {
            BuyCompleteSkin();
        }
        else
        {
            UpdateItem();
        }
    }
    public void btnBuy_Onclick()
    {
        if(model.price <= UserData.Coin)
        {
            BuyCompleteSkin();
            UserData.Coin -= model.price;
            MainCanvas.Instance.UpdateCoinUI(UserData.Coin + model.price, UserData.Coin);
        }
        else
        {
            Messenger.Instance.SetMess("You don't have enough coins!");
            //Debug.Log("You don't have enough coins!");
        }
    }
    void BuyCompleteSkin()
    {
        model.State = StateSkinItem.UNLOCKED;
        btnUse_Onclick();
    }
    public void btnUse_Onclick()
    {
        DataManager.Instance.dataSkin.idCurrentSkin = model.id;
        SelectSkinController.Instance.SetScrollSkin();
    }
    public void btnOpenSpin_Onclick()
    {
        SelectSkinController.Instance.btnClose_Onclick();
        MainCanvas.Instance.btnSpin_Onclick();
    }
    public void btnOpenShop_Onclick()
    {
        SelectSkinController.Instance.btnClose_Onclick();
        MainCanvas.Instance.btnShop_Onclick();
    }
    public void SetItem(DataSkinModel _model)
    {
        model = _model;
        idSkin = model.id;
        textPrice.text = model.price.ToString();
        imageSkin.sprite = DataManager.Instance.dataSkin.datas[idSkin].spriteSkin;
        UpdateItem();
    }
    void UpdateItem()
    {
        textCountAds.text = model.CountViewAds.ToString() + "/" + model.amountAds.ToString();
        buttonAds.SetActive(false);
        buttonBuy.SetActive(false);
        butonShop.SetActive(false);
        buttonSpin.SetActive(false);
        buttonRemove.SetActive(false);
        buttonUse.SetActive(false);
        
        switch (model.State)
        {
            case StateSkinItem.LOCK:
                if (model.getSpin)
                {
                    buttonSpin.SetActive(true);
                }else if (model.getShop)
                {
                    butonShop.SetActive(true);
                }
                else
                {
                    buttonAds.SetActive(true);
                    buttonBuy.SetActive(true);
                }
                break;
            case StateSkinItem.UNLOCKED:
                if (DataManager.Instance.dataSkin.idCurrentSkin == model.id)
                {
                    buttonRemove.SetActive(true);
                }
                else
                {
                    buttonUse.SetActive(true);
                }
                break;
            
        }
    }
}
