using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGamePlayData : MonoBehaviour
{
    [SerializeField] private Text txtTime, txtTotalGold;
    [SerializeField] private Text txtRoundBefore, txtRoundNow, txtRoundNext;
    [SerializeField] private Image imgRound; 
    private float remainTime, startTime;
    [HideInInspector] public float levelTime { set; private get; }
    private double lastTotalGold;
    private InitScenePlay InitPlay;

    // Start is called before the first frame update
    void Start()
    {
        InitPlay = FindObjectOfType<InitScenePlay>();
        startTime = Time.time;
        levelTime = LevelManager.GetCurrentLevelData().timeLevel;
        //levelTime = GameController.Instance.levelData.GetLevelData(SceneManager.GetActiveScene().buildIndex - 2).timeLevel;
        lastTotalGold = DataController.Instance.GetGold();
        txtTotalGold.text = lastTotalGold.ToString();
        SetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (InitPlay) if (!InitPlay.isStart) { startTime = Time.time; return; }

        TimeStopWatch();
        if (lastTotalGold != DataController.Instance.GetGold())
        {
            lastTotalGold = DataController.Instance.GetGold();
            txtTotalGold.text = lastTotalGold.ToString();
        }
    }

    /// <summary>
    /// Convert time from float to string
    /// </summary>
    private void TimeStopWatch()
    {
        remainTime = levelTime - (Time.time - startTime);
        DataController.Instance.timePlay = remainTime;
        string minutes = ((int)remainTime / 60).ToString("00");
        string seconds = ((int)remainTime % 60).ToString("00");
        txtTime.text = minutes + ":" + seconds;

        if (remainTime <= 0 && !GameController.Instance.isFinish)
        {
            GUIManager.ShowGUI(GUIName.DefeatDialog);
            GameController.Instance.isFinish = true;
        }
    }

    private void SetLevel()
    {
        int level = UserData.NextLevel;
        if(level == 1)
        {
            imgRound.fillAmount = 0.7f;
            txtRoundBefore.text = "";
            txtRoundNow.text = 1.ToString();
            txtRoundNext.text = 2.ToString();
        }
        else if(LevelManager.IsLastLevel())
        {
            imgRound.fillAmount = 0.7f;
            imgRound.fillOrigin = 0;
            txtRoundBefore.text = (level - 1).ToString();
            txtRoundNow.text = (level).ToString();
            txtRoundNext.text = "";
        }
        else
        {
            imgRound.fillAmount = 1f;
            txtRoundBefore.text = (level - 1).ToString();
            txtRoundNow.text = level.ToString();
            txtRoundNext.text = (level + 1).ToString();
        }
    }
}
