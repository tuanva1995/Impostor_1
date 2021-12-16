using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct MapData
{
    public GameObject Map;
    public GameObject[] Obstacles;
}
public class InitLevelData : InitScenePlay
{
    public MapData[] Maps;
    public GameObject[] Players;
    public GameObject[] Bot;
    public GameObject[] Traps;
    public GameObject currentmap;
    public Camera camera;
    private NavMeshSurface[] surfaces;
    private CreateLevelData currentLevelData;
    private bool onlyOne;
    public List<GameObject> lsEnemys = new List<GameObject>();
    private void Awake()
    {
       // StartLevel();
    }
    private void Start()
    {
    }
    public void StartLevel()
    {
        currentLevelData = LevelManager.GetCurrentLevelData();
        SpawnMap();
        camera = currentmap.GetComponentInChildren<Camera>();
        GameObject Player = Players[Random.Range(0, 2)];
        var lsEnemyPoints = currentmap.GetComponent<MapPoint>().enmyPoint;
        var PlayerClone = Instantiate(Player, currentmap.GetComponent<MapPoint>().playerPoint[0].position, Player.transform.rotation);

        lsEnemys.Add(PlayerClone);
        int rdCOunt = Random.Range(10, 15);
        GameObject[] ObjBots = new GameObject[rdCOunt];
        int count = 0;
        int time = 0;
        for (int i = 0; i < 10; i++)
        {
            var BotClone = Instantiate(Bot[count]);
            lsEnemys.Add(BotClone);
            var rdPoint = Random.Range(0, lsEnemyPoints.Count);
            BotClone.GetComponent<NavMeshAgent>().Warp(lsEnemyPoints[rdPoint].position);
            lsEnemyPoints.RemoveAt(rdPoint);
            var BotControl = BotClone.GetComponent<BotControl>();

            BotControl.botID = i;
            BotControl.UpdateSkinBot();
            ObjBots[i] = BotClone;
            count++;
            if (count == 3)
            {
                count = 0;
                time++;
                if (time == 2)
                {
                    count = Random.Range(3, 7);
                    time = 0;
                }

            }
            else if (count >3)
            {
                count = 0;
            }
        }
       
        PlayerClone.GetComponent<PlayerMovement>().enabled = true;
        //SetStartScene();
        GameController.Instance.playerLeft.text = currentLevelData.BotPositions.Length.ToString();
    }
    private void Update()
    {
        //if(surfaces[0].navMeshData != null && !onlyOne)
        //{
        //    SpawnTraps();
        //    onlyOne = true;
        //}
        if (isSlide&&camera!=null&&camera.fieldOfView>maxView)
        {
            camera.fieldOfView -= 30 * Time.deltaTime;
        }
        
        
    }
    int isFirst = 1;
    private void SpawnMap()
    {
        if(currentmap==null)
            currentmap = Instantiate(Maps[Random.Range(0, Maps.Length)].Map);
        //currentmap = Instantiate(Maps[1].Map);
        //currentmap = Instantiate(Maps[6].Map);
        //if (currentLevelData.Obstacles.Length != 0)
        //{
        //    foreach (var obs in currentLevelData.Obstacles)
        //    {
        //        var ObsClone = Instantiate(Maps[currentLevelData.mapID].Obstacles[obs.obstacleID], obs.Position, Quaternion.Euler(obs.Rotation));
        //        ObsClone.transform.localScale = obs.Scale;
        //    }
        //}
        BuildNavMesh();
    }
    private void SpawnTraps()
    {
        if (currentLevelData.TrapDatas.Length == 0) return;
        for (int i = 0; i < currentLevelData.TrapDatas.Length; i++)
        {
            foreach (var trap in Traps)
            {
                if (trap.GetComponent<TrapBase>().trapKind == currentLevelData.TrapDatas[i].trapKind)
                {
                    var TrapClone = Instantiate(trap, currentLevelData.TrapDatas[i].BeginPos, trap.transform.rotation);
                    TrapClone.transform.rotation = Quaternion.Euler(currentLevelData.TrapDatas[i].TrapRotation);
                    if (currentLevelData.TrapDatas[i].trapKind == TrapKind.SkullBall)
                    {
                        var SkullBall = TrapClone.GetComponent<SkullBallTrap>();
                        SkullBall.beginPos = currentLevelData.TrapDatas[i].BeginPos;
                        SkullBall.targetPos = currentLevelData.TrapDatas[i].TargetPos;
                    }
                }
            }
        }
    }

    private void BuildNavMesh()
    {
        surfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (var surface in surfaces) surface.BuildNavMesh();
    }
    bool isSlide = false;
    float maxView;
    public void SetCameraOff()
    {
        isSlide = true;
        if (camera.fieldOfView > 60)
            maxView = 60;
        else
            maxView = 30;

        //currentmap.transform.position = new Vector3(currentmap.transform.position.x, currentmap.transform.position.y, currentmap.transform.position.z -4);
        
        //var players = FindObjectsOfType<CharacterParams>();
        //foreach (var player in players)
        //{
        //    player.gameObject.SetActive(false);
        //}
    }
}
