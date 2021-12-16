//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActiveYouDieGUI : MonoBehaviour
{
    [SerializeField] private float timeWaitDie;
    [SerializeField] private Image timeSlide;
    [SerializeField] private Text txtDeadTime;
    [SerializeField] private GameObject noThank;
    private void Update()
    {
        if (timeWaitDie != 0)
        {
            timeSlide.fillAmount -= Time.deltaTime / timeWaitDie;
            int timeDead = Mathf.Clamp((int)(timeWaitDie * timeSlide.fillAmount) + 1, 0, (int)timeWaitDie);
            txtDeadTime.text = timeDead.ToString();
        }
        if (timeSlide.fillAmount == 0)
        {
            txtDeadTime.text = 0.ToString();
            GUIManager.HideGUI(GUIName.YouDieDialog);
            GUIManager.ShowGUI(GUIName.DefeatDialog);
        }
    }
    private void OnEnable()
    {
        Invoke("SetNoThank", 2);
    }
    void SetNoThank()
    {
        noThank.SetActive(true);
    }
    public void SoundHeart()
    {
        SoundController.Instance.PlaySfx(SoundController.Instance.heart);
    }
    public void Revival()
    {

        //timeSlide.fillAmount = 1f;
        //GUIManager.HideGUI(GUIName.YouDieDialog);
        //GameController.Instance.isFinish = false;
        //var Player = CustomUtils.GameObjectUtils.FindObjectsOfType<PlayerControl>();
        //Player.gameObject.SetActive(true);
        //Player.Revival();
        SoundController.Instance.PlaySfxButtonClick();
        if (!IronSourceAdsController.Instance.IsVideoRewardAdsReady() || Application.internetReachability == NetworkReachability.NotReachable)
        {
            //MessagePopup.Instance.ShowUp("Ads is not ready ! ", false, true);
            // MessagePopup.Instance.SetOnButtonOK(() => { MessagePopup.Instance.Hide(); });
            NoThank();
            return;
        }
        else
        {
            timeSlide.fillAmount = 1f;
            GUIManager.HideGUI(GUIName.YouDieDialog);
            GameController.Instance.isFinish = false;
            var Player = CustomUtils.GameObjectUtils.FindObjectsOfType<PlayerControl>();
            Player.gameObject.SetActive(true);
            Player.Revival();
            IronSourceAdsController.Instance.ShowVideoAds(() =>
            {

            }, null);
        }


    }
    public void NoThank()
    {
        SoundController.Instance.PlaySfxButtonClick();
        GUIManager.HideGUI(GUIName.YouDieDialog);
        GUIManager.ShowGUI(GUIName.DefeatDialog);
    }
}
