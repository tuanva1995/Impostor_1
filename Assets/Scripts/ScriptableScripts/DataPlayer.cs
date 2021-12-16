using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class DataPlayer : ScriptableObject
{
    public List<DataPlayerModle> datas;
    public List<int> ListGiftInFreeGift;
    public List<int> ListGiftInDailyGift;
    public DataPlayerModle GetDataPlayer(int id)
    {
        DataPlayerModle data = datas.Find(x => x.ID == id);
        return data;
    }

    /// <summary>
    /// Just Prefab
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //public Transform GetPrefabPlayer(int id)
    //{
    //    return GetDataPlayer(id).prefab;
    //}

    //public List<DataPlayerModle> lsPlayerCanUnlock()
    //{
    //    List<DataPlayerModle> ls = new List<DataPlayerModle>();
    //    for (int i = 0; i < datas.Count; i++)
    //    {
    //        if (datas[i].State == StatePlayerItem.LOCK )
    //            ls.Add(datas[i]);
    //    }
    //    return ls;
    //}

    //public List<DataPlayerModle> GetPlayersCoinUnlocked()
    //{
    //    List<DataPlayerModle> ls = new List<DataPlayerModle>();
    //    for (int i = 0; i < datas.Count; i++)
    //    {
    //        if (datas[i].State != StatePlayerItem.LOCK )
    //            ls.Add(datas[i]);
    //    }
    //    return ls;
    //}

    //public void UnSell
}
public enum StatePlayerItem
{
    LOCK,
    UNLOCKED
}
[System.Serializable]
public class DataPlayerModle
{
    /// <summary>
    /// Default: 0
    /// Regular: 1-99
    /// Premium: 100-199
    /// PrizeRoom: 200 - 299
    /// </summary>
    public int ID;
    public string name;
    public string description;
    public float dame;
    public float Hp;
    public float Price;
    
    //public TypeShopPlayer typeShopPlayer;

    //public Sprite iconPlayer;
    // public PlayerController prefabPlayer;
    //public Transform prefab;//Just anim, no code

    public DataPlayerModle(DataPlayerModle objectP)
    {
        this.ID = objectP.ID;
        this.name = objectP.name;
        this.description = objectP.description;
        this.dame = objectP.dame;
        this.Hp = objectP.Hp;
        //this.iconPlayer = objectP.iconPlayer;
        //this.prefabPlayer = objectP.prefabPlayer;
    }

    //public StatePlayerItem State
    //{
    //    get
    //    {
    //        if (ID == 0)
    //        {
    //            return StatePlayerItem.UNLOCKED;
    //        }
    //        else
    //            return (StatePlayerItem) PlayerPrefs.GetInt("PlayerUnLock"+ID, 0);
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetInt("PlayerUnLock" + ID, (int) value);
    //        PlayerPrefs.Save();
    //    }
    //}

    public void Unlock()
    {
        //State = StatePlayerItem.UNLOCKED;
    }

    //public void Select()
    //{
    //    var oldPlayer = GameController.Instance.dataContains.dataPlayer.GetDataPlayer(DataManager.CurrentIDPlayer);
    //    if (oldPlayer.State != StatePlayerItem.LOCK)
    //        oldPlayer.UnSelect();

    //    DataManager.CurrentIDPlayer = ID;
    //    State = StatePlayerItem.SELECTED;

    //}

    //public void UnSelect()
    //{
    //    State = StatePlayerItem.UNLOCKED;
    //}
}