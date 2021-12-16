using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveDefeatGUI : MonoBehaviour
{
    [SerializeField] private GameConfigSO gameConfig;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private Text txtGetGold, txtGetExp;
    private GameObject Player;

    private void OnEnable()
    {
        SoundController.Instance.PlaySfx(SoundController.Instance.lose);
        if (SceneManager.GetActiveScene().name == "TestSkinScene")
        {
            txtGetGold.text = "+" + 0;
            txtGetExp.text = "+" + 0;
        }
        else
        {
            txtGetGold.text = "+" + gameConfig.defeatBonusGold;
            txtGetExp.text = "+" + gameConfig.defeatBonusExp;
        }
        FindObjectOfType<InitLevelData>().SetCameraOff();
        //LogManager.LogLevel(DataController.Instance.playerData.levelCurrent, LevelDifficulty.Easy, (int)DataController.Instance.timePlay, PassLevelStatus.Fail, "Level " + DataController.Instance.playerData.levelCurrent);
        //ClonePlayer();
       
    }

    private void ClonePlayer()
    {
        //Player = Instantiate(CustomUtils.GameObjectUtils.FindObjectsOfType<PlayerControl>().gameObject, playerTrans.parent);
        //Player.SetActive(true);
        //Player.transform.Find("Pointer").gameObject.SetActive(false);
        //Destroy(Player.GetComponent<Rigidbody>());
        //Destroy(Player.GetComponent<PlayerControl>());
        //Destroy(Player.GetComponent<PlayerMovement>());
        //Animator PlayerAnim = Player.GetComponent<Animator>();
        //PlayerAnim.SetBool("isAttack", false);
        //PlayerAnim.SetFloat("moveSpeed", 0);
        //Player.transform.localPosition = playerTrans.localPosition;
        //Player.transform.localRotation = playerTrans.localRotation;
        //Player.transform.localScale = playerTrans.localScale;
    }
    private void OnDisable()
    {
        if (Player) Destroy(Player);
    }

    public void BackHome()
    {
      
        Initiate.Fade("GamePlay", Color.gray, 2.0f);
    }

    public void Claim()
    {
        SoundController.Instance.PlaySfxButtonClick();
        BackHome();
        UserData.Coin+=300;
        DataController.Instance.UpdateGold(gameConfig.defeatBonusGold);
        GameController.Instance.SetExp(gameConfig.defeatBonusExp);
    }
    public void Retry()
    {
        SoundController.Instance.PlaySfxButtonClick();
        if (SceneManager.GetActiveScene().name != "TestSkinScene")
        {
            DataController.Instance.UpdateGold(gameConfig.defeatBonusGold);
            GameController.Instance.SetExp(gameConfig.defeatBonusExp);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
