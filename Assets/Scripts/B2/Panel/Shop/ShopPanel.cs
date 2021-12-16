using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] GameObject popup, panelStaterPack, panelNoAds;

    private void OnEnable()
    {
        SetPanel();
        Commons.ShowPopup(popup.transform);
    }

    void SetPanel()
    {

    }

    public void btnClose_Onclick()
    {
        Commons.HidePopup(popup.transform, this.gameObject);
    }
    public void btnStaterPack_Onclick()
    {

    }
    public void btnNoAds_Onclick()
    {

    }
}
