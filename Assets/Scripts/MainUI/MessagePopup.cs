using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessagePopup : SimplePopup
{

    public Text txtCoin;
    public Image IconImg;
    public static MessagePopup Instance;
    public Sprite[] lsItem;
    public Image IconX2Video;
    protected override void Start()
    {
        Instance = this;
        base.Start();
    }
    public override void ShowUp(string content = "", bool isYesNo = true, bool isStatus = false, AnimationPopupType type = AnimationPopupType.OnFade)
    {
        txtCoin.text = "";
        IconImg.gameObject.SetActive(false);
        //IconX2Video.gameObject.SetActive(false);
        base.ShowUp(content, isYesNo, isStatus, type);
    }
    public void ShowMessageItem(string content,int idItem,int countOfItem)
    {

        ShowUp("", false, true);
        IconImg.gameObject.SetActive(true);
        IconImg.sprite = lsItem[idItem + 1];
        txtCoin.text = content+" "+countOfItem;
    }
    public void ShowX2Item(string content, int idItem)
    {

        ShowUp("", false, false);
        //IconX2Video.gameObject.SetActive(true);
        //IconX2Video.sprite = lsItem[idItem + 1];
        
    }
}
