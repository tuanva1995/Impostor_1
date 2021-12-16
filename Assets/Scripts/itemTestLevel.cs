using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemTestLevel : MonoBehaviour
{
    public Text txtLevel;
    public int index;
   
    public void SetItem(int _indexLevel)
    {
        index = _indexLevel;
        txtLevel.text = "Level " + index;
    }
    public void Click()
    {
        UserData.NextLevel = index;
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        Initiate.Fade("GamePlay", Color.black, 3.0f);
    }
}
