using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
//using FalconSDK.Advertising;

public class ActiveVictoryGUI : MonoBehaviour
{
    [SerializeField] private GameConfigSO gameConfig;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private GameObject GetX2Popup, noThank;
    [SerializeField] private Text txtGetGold, txtGetExp;
    private GameObject Player;
    private CharacterParams playerParams;
    public Transform coinTrans,coin100,coin300;
    private void OnEnable()
    {
        SoundController.Instance.PlaySfx(SoundController.Instance.win);
        playerParams = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterParams>();
        //if(playerParams.GetComponent<WeaponAttack>().rdWeapon<3)
        //    playerParams.GetComponent<WeaponAttack>().lsWeapons[playerParams.GetComponent<WeaponAttack>().rdWeapon].SetActive(false);
        FindObjectOfType<InitLevelData>().SetCameraOff();
        if (SceneManager.GetActiveScene().name == "TestSkinScene")
        {
            //txtGetGold.text = "+" + 0;
            //txtGetExp.text = "+" + 0;
        }
        else
        {
            //txtGetGold.text = "+" + playerParams.gold;
            //txtGetExp.text = "+" + gameConfig.victoryBonusExp;
        }
        playerParams.GetComponent<PlayerControl>().win1Effect.Play();
        playerParams.GetComponent<PlayerControl>().win2Effect.Play();
        //ClonePlayer();
        //if (DataController.Instance.playerData.levelCurrent == 1&&DataController.Instance.isShowDaily)
        //{
        //    Invoke("ShowDaily", 0.5f);
        //}
        // LogManager.LogLevel(DataController.Instance.playerData.levelCurrent, LevelDifficulty.Easy, (int)DataController.Instance.timePlay, PassLevelStatus.Pass, "Level " + DataController.Instance.playerData.levelCurrent);
        DataController.Instance.UpdateLevel();
        Invoke("SetNoThank",2);
    }
    void SetNoThank()
    {
        noThank.SetActive(true);
        
    }
    private void ShowDaily()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        //DailyGiftPopup.Instance.ShowPopup();
    }
    private void ClonePlayer()
    {
        //Player = Instantiate(GameObject.FindGameObjectWithTag("Player"), playerTrans.parent);
        //Player.SetActive(true);
        //Player.transform.Find("Pointer").gameObject.SetActive(false);
        //Destroy(Player.GetComponent<Rigidbody>());
        //Destroy(Player.GetComponent<PlayerControl>());
        //Destroy(Player.GetComponent<PlayerMovement>());
       // Animator PlayerAnim = Player.GetComponent<Animator>();
       // PlayerAnim.Play("win");
       //// PlayerAnim.SetFloat("moveSpeed", 0);
       // Player.transform.localPosition = playerTrans.localPosition;
       // Player.transform.localRotation = playerTrans.localRotation;
       // Player.transform.localScale = playerTrans.localScale;
    }
    public void Claim()
    {

        //if (Player) Destroy(Player);
        //if (SceneManager.GetActiveScene().name == "TestSkinScene")
        //{
        //    BackHome();
        //}
        //else
        EffectItemFly.Instance.StartEffectItemFly(coin100.position, coinTrans.position, TypeItemEffectFly.coin, 100, true);

        Debug.Log("Claim");
        
            //GameController.Instance.SetGold(playerParams.gold);
            //GameController.Instance.SetExp(gameConfig.victoryBonusExp);
            //GetX2Popup.SetActive(true);
            //GetX2Popup.transform.localScale = new Vector3(0, 0, 0);
            //GetX2Popup.GetComponent<UnityEngine.UI.Image>().enabled = false;
            //GameController.Instance.SetExp(gameConfig.victoryBonusExp);
           
            //DataController.Instance.UpdateGold(playerParams.gold);
            //if (LevelManager.IsLastLevel())
            //{
            //    BackHome();
            //}
            //else
            //{
            //  //  LevelManager.ChangeToNextLevel();

            //   // IronSourceManager.Instance.ShowInterstitialAd("Next Level", DataController.Instance.playerData.levelCurrent - 1);
            //    //GUIManager.HideGUI(GUIName.VictoryDialog);
            //}
            //return;
        //BackHome();
        //GetX2Popup.transform.DOScale(1.2f, .2f).SetEase(Ease.OutQuad).OnComplete(() =>
        //{
        //    GetX2Popup.transform.DOScale(1, .1f).SetEase(Ease.OutQuad).OnComplete(() =>
        //    {
        //        GetX2Popup.GetComponent<UnityEngine.UI.Image>().enabled = true;
        //    });

        //});
        //if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    GameController.Instance.SetExp(gameConfig.victoryBonusExp);
        //    //DataController.Instance.UpdateGold(playerParams.gold);
        //    if (LevelManager.IsLastLevel())
        //    {
        //        BackHome();
        //    }
        //    else
        //    {
        //        LevelManager.ChangeToNextLevel();

        //        IronSourceManager.Instance.ShowInterstitialAd("Next Level", DataController.Instance.playerData.levelCurrent-1);
        //        //GUIManager.HideGUI(GUIName.VictoryDialog);
        //    }
        //    return;
        //}
        //GetX2Popup.transform.DOScale(1.2f, .2f).SetEase(Ease.OutQuad).OnComplete(()=> {
        //GetX2Popup.transform.DOScale(1, .1f).SetEase(Ease.OutQuad).OnComplete(() => {
        //        GetX2Popup.GetComponent<UnityEngine.UI.Image>().enabled = true;
        //    });

        //});
        //}
    }
 

    public void WatchVideoBonusX2()
    {
        //DataController.Instance.UpdateGold(playerParams.gold);
        //if (!IronSourceManager.Instance.isVideoAvail || Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    MessagePopup.Instance.ShowUp("Ads is not ready ! ", false, true);
        //    MessagePopup.Instance.SetOnButtonOK(() => {

        //        //MessagePopup.Instance.Hide();
        //        GameController.Instance.SetExp(gameConfig.victoryBonusExp);
        //        //DataController.Instance.UpdateGold(playerParams.gold);
        //        if (LevelManager.IsLastLevel())
        //        {
        //            BackHome();
        //        }
        //        else
        //        {
        //            LevelManager.ChangeToNextLevel();

        //            IronSourceManager.Instance.ShowInterstitialAd("Next Level",DataController.Instance.playerData.levelCurrent-1);
        //            //GUIManager.HideGUI(GUIName.VictoryDialog);
        //        }
        //    });


        //}
        //else
        //{
        IronSourceAdsController.Instance.ShowVideoAds(() =>
        {
            
            EffectItemFly.Instance.StartEffectItemFly(coin300.position, coinTrans.position, TypeItemEffectFly.coin, 300,true);
            //BackHome();
            //DataController.Instance.UpdateGold(playerParams.gold);
            //BackHome();
            //if (LevelManager.IsLastLevel())
            //{
            //    BackHome();
            //}
            //else
            //{
            //    LevelManager.ChangeToNextLevel();

            //    // IronSourceManager.Instance.ShowInterstitialAd("Next Level", DataController.Instance.playerData.levelCurrent - 1);
            //    //GUIManager.HideGUI(GUIName.VictoryDialog);
            //}
        },null);
        
        //}

        //GoogleAdsController.instance.ShowVideoAds();
        //GoogleAdsController.instance.evt = () => {

        //};

    }

    public void NoThank()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (LevelManager.IsLastLevel())
        {
            BackHome();
        } 
        else
        {
            if(UserData.NextLevel%2==0)
                DataController.Instance.luckyWheel.SetActive(true);
            else
            {
                LevelManager.ChangeToNextLevel();
                //IronSourceManager.Instance.ShowInterstitialAd("Next Level", DataController.Instance.playerData.levelCurrent-1);
            }
            
            //GUIManager.HideGUI(GUIName.VictoryDialog);
        }
    }

    public void BackHome()
    {

        Initiate.Fade("GamePlay", Color.gray,1.0f);

    }

}
