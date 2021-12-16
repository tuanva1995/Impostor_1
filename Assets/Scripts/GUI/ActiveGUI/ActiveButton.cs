using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ActiveButton : MonoBehaviour
{
    public void ReloadScene()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackMenu()
    {
        Time.timeScale = 1;
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        Initiate.Fade("MainScene", Color.gray, 2.0f);
    }
    public void Pause(bool isPause)
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        if (isPause)
        {
            Time.timeScale = 0;
            GUIManager.ShowGUI(GUIName.PauseDialog);
        }
        else
        {
            Time.timeScale = 1;
            GUIManager.HideGUI(GUIName.PauseDialog);
        }
    }        
}
