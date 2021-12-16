using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
[Serializable]
public class PlayerData
{
   
    public double Gold, Diamond;
    public int CurrentDailyGiftRevc = 0; // so ngay nhan dc qua
    public bool isDailyReward = false; // so ngay nhan dc qua
    public int dailyMissionCOunt = 0;
    public float[] LsPlayerDame;
    public float[] LsPlayerEXP;
    public int[] lsUnlock;
    public int[] ListItem;
    public double[] lsMissionProcess;
    public int[] lsMissionLevel;
    public bool[] lsMissionRewarded;
    public int playerCurrentIndex = 0;
    public float playerDameCurrentIndex = 0;
    public float playerHPCurrentIndex = 0;
    public int FlagIndex = 0;
    //public int levelCurrent = 1;
    public int FreeGiftIndex = 0;
    public float timeCountDown;
    public bool isRewardGiftOnClock = false;
}
public class DataController : MonoBehaviour
{
    public static DataController Instance;
    public PlayerData playerData;
    public DataPlayer baseData;
    public List<MissionData> lsMission;
    public int testSkinID;
    public  int TIME_MAX = 3 * 60;
    public GameObject luckyWheel;
    public bool isShowDaily = false;
    public float timePlay = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
       
        playerData = new PlayerData();
        
        
        LoadData();
        CheckNewDay();
        if (PlayerPrefs.GetInt("IsFirstTimeOpenApp") == 0)
        {
            PlayerPrefs.SetInt("IsFirstShowDaily", 0);
            PlayerPrefs.SetInt("IsFirstTimeOpenApp", 1);
            ResetData();
            SaveData();
        }
        //playerData.levelCurrent = 4;
    }
    private void Update()
    {
        if(playerData.timeCountDown > 0)
        {
            playerData.timeCountDown -= Time.deltaTime;
        }
        else
        {
            playerData.isRewardGiftOnClock = true;
        }
    }
    
    private void OnApplicationQuit()
    {
        SaveData();
    }
    public double GetGold() {
        return playerData.Gold;
    }

    public void UpdateGold(double count)
    {
        playerData.Gold += count;
        if (playerData.Gold < 0) playerData.Gold = 0;

        GameObject Coin = GameObject.FindGameObjectWithTag("TxtCoin");
        if(Coin)
        {
            Text txtCoin = Coin.GetComponent<Text>();
            if (txtCoin != null)
                txtCoin.text = playerData.Gold.ToString();
        }
        DataController.Instance.UpdateMisson(1, (int)playerData.Gold);
        SaveData();

    }
   
    public void UpdateItems(int id,int count)
    {
        playerData.ListItem[id] += count;
        SaveData();
    }
    public void UpdateLevel()
    {
        //playerData.levelCurrent++;
        UserData.NextLevel += 1;
        SaveData();
    }
    public int GetItem(int id)
    {
        return playerData.ListItem[id];
    }
    public bool isNewDay = false;
    public void CheckNewDay()
    {
        //PlayerPrefs.GetString("NewDay")
        string today = System.DateTime.Today.ToString();
        bool a = (today == PlayerPrefs.GetString("NewDay")) ? true : false;
        Debug.Log(a + " ---- " + PlayerPrefs.GetString("NewDay"));
        if (!a)
        {
            isNewDay = true;
            isShowDaily = true;
            PlayerPrefs.SetString("NewDay", today);
            playerData.isDailyReward = false;
            playerData.FreeGiftIndex = 0;
            playerData.timeCountDown = TIME_MAX;
            SaveData();

        }
        //string DD = 
    }
    public void UpdateMisson(int id, double count)
    {
        if (playerData.lsMissionLevel[id] == lsMission[id].lsTarget.Length)
        {
            lsMission[id].isComplete = true;
            return;
        }
        if (id < 1)
            playerData.lsMissionProcess[id] += count;
        else if (count != 0)
        {
            playerData.lsMissionProcess[id] = count;

        }


        double temp = playerData.lsMissionProcess[id] % lsMission[id].lsTarget[playerData.lsMissionLevel[id]];

        if (!playerData.lsMissionRewarded[id])
        {
            if (playerData.lsMissionProcess[id] >= lsMission[id].lsTarget[playerData.lsMissionLevel[id]])
            {
                playerData.lsMissionRewarded[id] = true;

            }
        }

        //Debug.Log(" level : " + level);
        //DataController.Instance.playerData.lsMissionLevel[id] = level;
        //DataController.Instance.playerData.lsMissionRewarded[id] = isRewarded;
        //DataController.Instance.playerData.lsMissionProcess[id] = process;
        DataController.Instance.SaveData();
    }
    public void LoadData()
    {
        Debug.Log(Application.persistentDataPath + "/bs.gd");
        if (File.Exists(Application.persistentDataPath + "/bs.gd"))
        {
            try
            {
                using (FileStream file = File.Open(Application.persistentDataPath + "/bs.gd", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    playerData = (PlayerData)bf.Deserialize(file);
                    file.Close();
                    
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }

        }
        else
        {
            ResetData();

        }
    }
   
    public void SaveData()
    {
        
        
        try
        {
            using (FileStream file = File.Open(Application.persistentDataPath + "/bs.gd", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, playerData);
                file.Close();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }
    void ResetData()
    {
        playerData.playerCurrentIndex = 0;
        //playerData.levelCurrent = 1;
        playerData.CurrentDailyGiftRevc = 0;
        playerData.Gold = 300;
        playerData.LsPlayerDame = new float[baseData.datas.Count];
        playerData.LsPlayerDame[0] = 1;
        playerData.LsPlayerEXP = new float[baseData.datas.Count];
        playerData.LsPlayerEXP[0] = 1;
        playerData.lsUnlock = new int[baseData.datas.Count];
        playerData.lsUnlock[0] = (int)StatePlayerItem.UNLOCKED;
        playerData.lsMissionLevel = new int[3];
        playerData.lsMissionProcess = new double[3];
        playerData.lsMissionRewarded = new bool[3];
        playerData.ListItem = new int[4];
        playerData.ListItem[0] = 2;
        playerData.ListItem[1] = 2;
        playerData.ListItem[2] = 2;
        playerData.ListItem[3] = 2;
    }
}
