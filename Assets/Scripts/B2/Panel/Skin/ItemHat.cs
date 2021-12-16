using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemHat : MonoBehaviour
{
    public int idHat;
    DataHatModel model;
    [SerializeField] GameObject buttonRemove, buttonAds, buttonBuy, buttonUse, buttonSpin, butonShop;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] TextMeshProUGUI textCountAds;
    [SerializeField] Image imageSkin;


    public void btnRemove_Onclick()
    {
        DataManager.Instance.dataHat.idCurrentHat = 0;
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
            BuyComplete();
        }
        else
        {
            UpdateItem();
        }
    }
    public void btnBuy_Onclick()
    {
        if (model.price <= UserData.Coin)
        {
            BuyComplete();
            UserData.Coin -= model.price;
        }
        else
        {
            Messenger.Instance.SetMess("You don't have enough coins!");
            //Debug.Log("You don't have enough coins!");
        }
    }
    void BuyComplete()
    {
        model.State = StateHatItem.UNLOCKED;
        btnUse_Onclick();
    }
    public void btnUse_Onclick()
    {
        DataManager.Instance.dataHat.idCurrentHat = model.id;
        SelectSkinController.Instance.SetScrollHat();
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
    public void SetItem(DataHatModel _model)
    {
        model = _model;
        idHat = model.id;
        textPrice.text = model.price.ToString();
        imageSkin.sprite = DataManager.Instance.dataHat.datas[idHat].sprite;
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
            case StateHatItem.LOCK:
                if (model.getSpin)
                {
                    buttonSpin.SetActive(true);
                }
                else if (model.getShop)
                {
                    butonShop.SetActive(true);
                }
                else
                {
                    buttonAds.SetActive(true);
                    buttonBuy.SetActive(true);
                }
                break;
            case StateHatItem.UNLOCKED:
                if (DataManager.Instance.dataHat.idCurrentHat == model.id)
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
