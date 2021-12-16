using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSoundButton : MonoBehaviour
{
    //public float deltaMove = 10;
    //[SerializeField] private GameObject bgSoundGreen, btnSound, txtOnObj, txtOffObj;
    //[SerializeField] private RectTransform btnOffTrans;
    //private Vector2 btnOnTrans;
    //private bool isMove, isOn;
    //private RectTransform btnRect;
    //public bool isSound;
    //private void Awake()
    //{
    //    btnRect = btnSound.GetComponent<RectTransform>();
    //    btnOnTrans = btnRect.anchoredPosition;
    //    if (isSound)
    //    {
    //        isOn = !PlayerPrefsX.GetBool("IsSoundOn");
    //    }
    //    else
    //    {
    //        isOn = !PlayerPrefsX.GetBool("IsMuSicOn");
    //    }
    //    ActiveSound();
    //}
    //private void Update()
    //{
    //    if (isOn)
    //    {
    //        if (isMove)
    //        {
    //            btnRect.anchoredPosition = new Vector2(btnRect.anchoredPosition.x - deltaMove, btnRect.anchoredPosition.y);
    //        }
    //        if (btnRect.anchoredPosition.x <= btnOffTrans.anchoredPosition.x) isMove = false;
    //    }
    //    else
    //    {
    //        if (isMove)
    //        {
    //            btnRect.anchoredPosition = new Vector2(btnRect.anchoredPosition.x + deltaMove, btnRect.anchoredPosition.y);
    //        }
    //        if (btnRect.anchoredPosition.x >= btnOnTrans.x) isMove = false;
    //    }
    //}
    //public void ActiveSound()
    //{

    //    isOn = bgSoundGreen.activeInHierarchy;
    //    isMove = true;
    //    bgSoundGreen.SetActive(!isOn);
    //    txtOnObj.SetActive(!isOn);
    //    txtOffObj.SetActive(isOn);
    //}
    public GameObject musicObjOn, soundObjOn, musicObjOff, soundObjOff;
    private void Awake()
    {
        SwitchState();
    }
    void SwitchState()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (MusicManager.Instance.SoundVolume>0)
        {
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
            MusicManager.Instance.MusicVolume = 0f;
        }
        SwitchState();
    }
    public void OnButtonSound(bool isOn)
    {

        MusicManager.Instance.ToggleSound(isOn);
        SwitchState();
    }
}
