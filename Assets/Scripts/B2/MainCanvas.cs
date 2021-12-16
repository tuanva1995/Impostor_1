using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCanvas : MonoBehaviour
{
    public Transform transCoin;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] GameObject panelSelectSkin;
    [SerializeField] GameObject panelSetting;
    [SerializeField] GameObject panelShop;
    [SerializeField] GameObject panelSpin;
    public static MainCanvas Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        StartCoroutine(Commons.CurrencyChange(textCoin, 0, UserData.Coin, 0.5f, false));
        SoundController.Instance.PlayBGMusic(SoundController.Instance.musicMenu);
        SetSkinAndHatPlayer();
        textLevel.text = UserData.NextLevel.ToString();
    }
    public void SetSkinAndHatPlayer()
    {

        player.GetComponent<PlayerSkin>().SetUpSkin();
    }


    public void UpdateCoinUI(int oldCoin, int newCoin)
    {
        StartCoroutine(Commons.CurrencyChange(textCoin, oldCoin, newCoin, 0.5f, false));
    }
    public void btnSelectSkin_Onclick()
    {
        panelSelectSkin.SetActive(true);
        //SoundController.Instance.PlaySfxButtonClick();
    }
    public void btnSetting_Onclick()
    {
        panelSetting.SetActive(true);
        SoundController.Instance.PlaySfxButtonClick();
    }
    public void btnSpin_Onclick()
    {
        if (panelSpin == null) return;
        panelSpin.SetActive(true);
        SoundController.Instance.PlaySfxButtonClick();
    }
    public void btnShop_Onclick()
    {
        if (panelShop == null) return;
        panelShop.SetActive(true);
        SoundController.Instance.PlaySfxButtonClick();
    }
    public void btnRemoveAds_Onclick()
    {
        SoundController.Instance.PlaySfxButtonClick();

    }
    public void btnGift_Onclick()
    {
        SoundController.Instance.PlaySfxButtonClick();
    }
    public void OnButtonPlay()
    {
        
        Initiate.Fade("GamePlay", Color.blue, 1f);
        SoundController.Instance.PlaySfxButtonClick();
        //SceneManager.LoadScene("Map1");
    }
}
