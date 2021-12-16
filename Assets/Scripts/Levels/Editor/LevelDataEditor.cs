using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CreateLevelData))]
public class LevelDataEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CreateLevelData level = (CreateLevelData)target;
        GUILayout.Label("Obstacles Data");
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Copy"))
        {
            LevelEditorBuffer.CopyObstacle(level.Obstacles);
        }
        if(GUILayout.Button("Paste"))
        {
            PasteObstacle(level);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Traps Data");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Copy"))
        {
            LevelEditorBuffer.CopyTrapData(level.TrapDatas);
        }
        if(GUILayout.Button("Paste"))
        {
            PasteTrapData(level);
        }
        GUILayout.EndHorizontal();
    }
    private void PasteObstacle(CreateLevelData level)
    {
        ObstacleData[] tmpData = new ObstacleData[level.Obstacles.Length + LevelEditorBuffer.ObstacleBuffer.Length];
        for(int i=0;i<tmpData.Length;i++)
        {
            if(i < level.Obstacles.Length)
            {
                tmpData[i] = level.Obstacles[i];
            }
            else
            {
                tmpData[i] = LevelEditorBuffer.ObstacleBuffer[i - level.Obstacles.Length];
            }
        }
        level.Obstacles = new ObstacleData[level.Obstacles.Length + LevelEditorBuffer.ObstacleBuffer.Length];
        level.Obstacles = tmpData;
    }
    private void PasteTrapData(CreateLevelData level)
    {
        TrapDataLevel[] tmpData = new TrapDataLevel[level.TrapDatas.Length + LevelEditorBuffer.TrapDataBuffer.Length];
        for(int i=0;i<tmpData.Length;i++)
        {
            if(i < level.TrapDatas.Length)
            {
                tmpData[i] = level.TrapDatas[i];
            }
            else
            {
                tmpData[i] = LevelEditorBuffer.TrapDataBuffer[i - level.TrapDatas.Length];
            }
        }
        level.TrapDatas = new TrapDataLevel[level.TrapDatas.Length + LevelEditorBuffer.TrapDataBuffer.Length];
        level.TrapDatas = tmpData;
    }
}
