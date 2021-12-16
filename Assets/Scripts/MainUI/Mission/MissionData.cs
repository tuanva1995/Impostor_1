using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class MissionData : ScriptableObject
{
    public int id;
    public string name;
    public int[] lsTarget;
    public int[] lsReward;
    public Sprite iconSpr;
    //public PayOuts type;
    public bool isComplete = false;
    //public void UpdateProcess(int id,double count)
    //{
    //    if(level == lsTarget.Length)
    //    {
    //        isComplete = true;
    //        return;
    //    }
    //    if (id < 1)
    //        process += count;
    //    else if(count!=0)
    //    {
    //        process = count;
            
    //    }
           
       
    //    double temp = process % lsTarget[level];
        
    //    if (!isRewarded)
    //    {
    //        if (process >= lsTarget[level])
    //        {
    //            isRewarded = true;
                
    //        }  
    //    }
        
    //    Debug.Log(" level : " + level); 
    //    DataController.Instance.playerData.lsMissionLevel[id] = level;
    //    DataController.Instance.playerData.lsMissionRewarded[id] = isRewarded;
    //    DataController.Instance.playerData.lsMissionProcess[id] = process;
    //    DataController.Instance.SaveData();
    //}
}
