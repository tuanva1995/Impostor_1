using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    private static Dictionary<int, CreateLevelData> m_Levels = new Dictionary<int, CreateLevelData>();

	public static void AddLevel(CreateLevelData level)
	{
		if (!m_Levels.ContainsKey(level.levelID))
		{
			m_Levels.Add(level.levelID, level);
		}
		else
			Debug.LogError("Existed level " + level.levelID);
	}

	public static CreateLevelData GetCurrentLevelData()
	{
		return GetLevelData(1);
	}

	public static bool IsLastLevel()
	{
		return UserData.NextLevel == m_Levels.Count;
	}
	public static void ChangeToNextLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
		//Initiate.Fade("GamePlay", Color.gray, 3.0f);
	}

	public static CreateLevelData GetLevelData(int levelID)
	{
		CreateLevelData result;
		if (m_Levels.TryGetValue(levelID, out result))
		{
			return result;
		}
		else
		{
			return null;
		}
	}
}