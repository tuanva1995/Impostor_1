using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] GameObject popup, stateOnMusic, stateOffMusic, stateOnSound, stateOffSound;

    private void OnEnable()
    {
        Commons.ShowPopup(popup.transform);
        SetStatePanel();
    }
 
    public void btnMusic_Onclick()
    {
        SoundController.Instance.StateMusic = !SoundController.Instance.StateMusic;
        if (SoundController.Instance.StateMusic)
        {
            SoundController.Instance.UnPauseMusic();
        }
        else
        {
            SoundController.Instance.PauseMusic();
        }
        SetStatePanel();
        
    }
    public void btnSound_Onclick()
    {
        SoundController.Instance.StateSound = !SoundController.Instance.StateSound;
        SetStatePanel();
    }
    public void btnRate_Onclick()
    {

    }
    public void btnRestore_Onclick()
    {

    }
    public void btnClose_Onclick()
    {
        Commons.HidePopup(popup.transform, this.gameObject);
    }
    void SetStatePanel()
    {
        stateOnMusic.SetActive(SoundController.Instance.StateMusic);
        stateOffMusic.SetActive(!SoundController.Instance.StateMusic);
        stateOnSound.SetActive(SoundController.Instance.StateSound);
        stateOffSound.SetActive(!SoundController.Instance.StateSound);
    }

}
