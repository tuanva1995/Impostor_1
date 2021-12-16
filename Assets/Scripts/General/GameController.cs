//using FalconSDK.Advertising;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameController : Singleton<GameController>
{
  
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private ItemsDataSO itemsData;
    [SerializeField] Sprite[] arraySpriteWeapon;
    [SerializeField] Image imageItemWeapon1, imageItemWeapon2;
    [HideInInspector]
    public Texture PlayerSkin;
    [HideInInspector]
    public Sprite PlayerAvatar;
    [HideInInspector]
    public Sprite PlayerFlag;
    //[HideInInspector]
    //public ItemData[] itemArray = new ItemData[4];
    [HideInInspector]
    public bool isFinish;
    public int exp { get; private set; }
    public int healAmount, shieldAmount, weaponAmount, strengthAmount;
    public GameObject[] wepon1, wepon2;
    public GameObject go,startPanel,nothank,setting,menuPanel,mapPanel,level,joytick;
    public Image weaponSkill;
    public Sprite[] weaponSPrite;
    public Text lvCurrent, lvNext, playerLeft;
    public TextMeshProUGUI txtCoin;
    public Combo combo;
    public Material[] matOndie;
    public OffScreenIndicator _offScreenIndicator;
    private new void Awake()
    {
        base.Awake();
        //SoundController.Instance.PlayMusic(FXSound.Instance.GamePlayMusic);
        Initialize();
        if (PlayerPrefs.GetInt("IsFirstShowDaily") == 0)
        {
            PlayerPrefs.SetInt("IsFirstShowDaily", 1);
        }
        //Debug.Log("init");
        //IronSourceManager.Instance.LoadBaner();
       
        lvCurrent.text = UserData.NextLevel.ToString();
        lvNext.text = (UserData.NextLevel + 1).ToString();
        StartGame();
        UpdatecOIN();
    }
    public void UpdatecOIN()
    {
        txtCoin.text = Commons.GetFriendlyShortNumber(UserData.Coin);
    }
    public void StartGame()
    {
        //Time.timeScale = 0;
        //wepon1[Random.Range(0, 2)].gameObject.SetActive(true);
        //wepon2[Random.Range(0, 2)].gameObject.SetActive(true);
        imageItemWeapon1.sprite = arraySpriteWeapon[UnityEngine.Random.Range(0, arraySpriteWeapon.Length)];
        imageItemWeapon2.sprite = arraySpriteWeapon[UnityEngine.Random.Range(0, arraySpriteWeapon.Length)];
        Invoke("SetNoThank", 2);
    }
    public void SetWeapon(int index)
    {
        IronSourceAdsController.Instance.ShowVideoAds(() => {
            Invoke("GoGame", 1);
            startPanel.SetActive(true);
            go.SetActive(false);
            FindObjectOfType<PlayerMovement>().weaponScript.rdWeapon = index;
            Time.timeScale = 1;
            weaponSkill.sprite = weaponSPrite[index];
        }, null);
       
    }
    public void Setting()
    {
        setting.SetActive(true);
    }
    void SetNoThank()
    {
        nothank.SetActive(true);
    }
    public void NoThank()
    {
        Invoke("GoGame", 1);
        startPanel.SetActive(true);
        go.SetActive(false);
       // FindObjectOfType<PlayerMovement>().weaponScript.rdWeapon = index;
        Time.timeScale = 1;
        weaponSkill.sprite = weaponSPrite[FindObjectOfType<PlayerMovement>().weaponScript.rdWeapon];
        //SetWeapon(FindObjectOfType<PlayerMovement>().weaponScript.rdWeapon);
    }
    void GoGame()
    {
        FindObjectOfType<InitScenePlay>().isStart = true;
        joytick.SetActive(true);
        level.SetActive(true);
        startPanel.SetActive(false);
        SoundController.Instance.PlaySfxButtonClick();
        SoundController.Instance.PlayBGMusic(SoundController.Instance.musicGameplay);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
        isFinish = false;
    }
    public void PlayGame()
    {
        mapPanel.SetActive(false);
        FindObjectOfType<InitLevelData>().StartLevel();
        go.gameObject.SetActive(true);
        menuPanel.SetActive(false);
        SoundController.Instance.PlaySfxButtonClick();
    }
    public Texture GetPlayerSkin(int index)
    {        
        if (index >= playerData.PlayerSkins.Length)
        {
            Debug.LogError("Don't have a skin that has " + index + " index");
            return playerData.PlayerSkins[0];
        }
        else return playerData.PlayerSkins[index];
    }

    public Sprite GetPlayerAvatar(int index)
    {
        if (index >= playerData.PlayerAvatars.Length)
        {
            Debug.LogError("Don't have a avatar that has " + index + " index");
            return playerData.PlayerAvatars[0];
        }
        else return playerData.PlayerAvatars[index];
    }

    public Sprite GetFlag(int index)
    {
        if (index >= playerData.PlayerFlags.Length)
        {
            Debug.LogError("Don't have a flag that has " + index + " index");
            return playerData.PlayerFlags[0];
        }
        else return playerData.PlayerFlags[index];
    }

    public Sprite GetRandomFlag()
    {
        return playerData.PlayerFlags[Random.Range(0, playerData.PlayerFlags.Length)];
    }
    public ItemData GetItemData(ItemKind itemKind)
    {
        ItemData item = new ItemData();
        //foreach (var _item in itemArray)
        //{
        //    if (_item.kindItem == itemKind)
        //        item = _item;
        //}
        return item;
    }
    public void SetItemData(ItemData itemdata)
    {
        //for (int i = 0; i < itemArray.Length; i++)
        //{
        //    if (itemArray[i].kindItem == itemdata.kindItem)
        //    {
        //        itemArray[i] = itemdata;
        //        //PlayerPrefs.SetInt(itemArray[i].kindItem.ToString(), itemArray[i].itemAmount);
        //    }
        //}
    }


    private void Initialize()
    {
        GUIManager.Init();
        SetPlayerSkin();
        SetPlayerFlag();
        GetExp();
        GetItemValue();
    }
    private void SetPlayerSkin()
    {
        //if (DataController.Instance != null)
        //{
        //    PlayerSkin = GetPlayerSkin(DataController.Instance.playerData.playerCurrentIndex);
        //    PlayerAvatar = GetPlayerAvatar(DataController.Instance.playerData.playerCurrentIndex);
        //}
        //else
        //{
        //    PlayerSkin = GetPlayerSkin(0);
        //    PlayerAvatar = GetPlayerAvatar(0);
        //}
    }

    private void SetPlayerFlag()
    {
        if (DataController.Instance != null)
            PlayerFlag = GetFlag(DataController.Instance.playerData.FlagIndex);
        else PlayerFlag = GetFlag(0);
    }

    public void SetGold(int bonusGold)
    {
        if (DataController.Instance != null)
            DataController.Instance.UpdateGold(bonusGold);
    }

    public void SetExp(int bonusExp)
    {
        exp += bonusExp;
        PlayerPrefs.SetInt("Exp", exp);
    }

    private void GetExp()
    {
        if (PlayerPrefs.HasKey("Exp"))
        {
            exp = PlayerPrefs.GetInt("Exp");
        }
        else
        {
            exp = 0;
            PlayerPrefs.SetInt("Exp", 0);
        }
    }

    private void GetItemValue()
    {
        //for (int i = 0; i < itemArray.Length; i++)
        //{
        //    itemArray[i] = itemsData.itemDataArray[i];
        //    //var itemName = itemArray[i].kindItem.ToString();
        //    //if (PlayerPrefs.HasKey(itemName))
        //    //{
        //    //    itemArray[i].itemAmount = PlayerPrefs.GetInt(itemName);
        //    //}
        //    //else
        //    //{
        //    //    PlayerPrefs.SetInt(itemName, itemArray[i].itemAmount);
        //    //}
        //}
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
