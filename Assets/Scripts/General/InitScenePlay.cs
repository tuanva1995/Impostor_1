using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitScenePlay : MonoBehaviour
{
    [HideInInspector] public bool isStart;

    private GameObject StartPanel;
    private Text txtStart;

    protected void SetStartScene()
    {
        StartPanel = GameObject.Find("StartPanel");
        txtStart = StartPanel.GetComponentInChildren<Text>();
        txtStart.text = 3.ToString();
        TweenControl.GetInstance().DelayCall(transform, 0.75f, () =>
        {
            txtStart.text = 2.ToString();
            TweenControl.GetInstance().DelayCall(transform, 0.75f, () =>
            {
                txtStart.text = 1.ToString();
                TweenControl.GetInstance().DelayCall(transform, 0.75f, () =>
                {
                    txtStart.text = "GO";
                    MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxGo);
                    TweenControl.GetInstance().DelayCall(transform, 0.75f, () =>
                    {
                        StartPanel.SetActive(false);
                        txtStart.text = "";
                        isStart = true;
                    });
                });
            });
        });

    }
}
