using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopup : SimplePopup
{
    public GameObject musicObjOn, soundObjOn, musicObjOff, soundObjOff;
    protected override void Start()
    {
        base.Start();
        SwitchState();
    }
    void SwitchState()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (MusicManager.Instance.SoundVolume > 0) {
            soundObjOff.SetActive(false);
            soundObjOn.SetActive(true);
        }
        else
        {
            soundObjOff.SetActive(true);
            soundObjOn.SetActive(false);
        }
        if (MusicManager.Instance.MusicVolume > 0)
        {
            musicObjOff.SetActive(false);
            musicObjOn.SetActive(true);
        }
        else
        {
            musicObjOff.SetActive(true);
            musicObjOn.SetActive(false);
        }
    }
    public void OnButtonMusic(bool isOn)
    {
        if (isOn)
        {
            MusicManager.Instance.ResumeBGMusic();
            MusicManager.Instance.MusicVolume = 0.5f;
        }
        else
        {
            MusicManager.Instance.PauseBGMusic();
            MusicManager.Instance.MusicVolume = 0;
        }
        SwitchState();
    }
    public void OnButtonSound(bool isOn)
    {
        MusicManager.Instance.ToggleSound(isOn);
        //SoundController.Instance.ToggleSound(isOn);
        SwitchState();
    }
}
