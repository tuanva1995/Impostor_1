using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIManager
{
	private static Dictionary<GUIName, GUIControl> m_Controls = new Dictionary<GUIName, GUIControl>();

	/// <summary>
	/// register into m_Controls
	/// </summary>
	/// <param name="control"></param>
	public static void Register(GUIControl control)
	{
		if (control != null && !m_Controls.ContainsKey(control.NameGUI))
		{
			m_Controls.Add(control.NameGUI, control);
		}
	}
	/// <summary>
	/// unregister into m_Controls
	/// </summary>
	/// <param name="control"></param>
	public static void UnRegister(GUIControl control)
	{
		if (control != null && m_Controls.ContainsKey(control.NameGUI))
		{
			m_Controls.Remove(control.NameGUI);
		}
	}
	public static void Init()
	{
		m_Controls.Clear();
		RegisterAllGUI();
		foreach (KeyValuePair<GUIName, GUIControl> control in m_Controls)
		{
			control.Value.Init();
		}
	}

	/// <summary>
	/// register all GUI in current scene
	/// </summary>
	static void RegisterAllGUI()
	{
		var listGUI = CustomUtils.GameObjectUtils.FindObjectsOfTypeAll<GUIControl>();
		foreach (var gui in listGUI)
		{
			Register(gui);
		}
	}

	/// <summary>
	/// clear list m_Controls;
	/// </summary>
	public static void CleanGUIManager()
	{
		m_Controls.Clear();
	}
	/// <summary>
	/// Call to show a GUI that registered in list 
	/// </summary>
	/// <param name="nameGUI"></param>
	public static void ShowGUI(GUIName nameGUI)
	{
		if (!IsContainsKey(nameGUI))
			Debug.LogError("GUI " + nameGUI + " doesn't registered");

		GUIControl resultControl;
		if (m_Controls.TryGetValue(nameGUI, out resultControl))
		{
			resultControl.OnShow();
		}
	}
	/// <summary>
	/// Call to hide a GUI that registered in list
	/// </summary>
	/// <param name="nameGUI"></param>
	public static void HideGUI(GUIName nameGUI)
	{
		if (!IsContainsKey(nameGUI))
			Debug.LogError("GUI " + nameGUI + " doesn't registered");

		GUIControl resultControl;
		if (m_Controls.TryGetValue(nameGUI, out resultControl) && resultControl.gameObject.activeSelf)
		{
			resultControl.OnHide();
		}
	}
	/// <summary>
	/// hide all GUI in list m_Controls
	/// </summary>
	public static void HideAllGUI()
	{
		foreach (KeyValuePair<GUIName, GUIControl> control in m_Controls)
		{
			control.Value.OnHide();
		}
	}
	/// <summary>
	/// Check have a GUI active at scene
	/// </summary>
	/// <returns><c>true</c>, if have a GUI active at scene, <c>false</c> all gui is inactive. </returns>
	public static bool HaveGUIActiveAtScene()
	{
		foreach (KeyValuePair<GUIName, GUIControl> control in m_Controls)
		{
			if (control.Value.IsGUIActive())
				return true;
		}
		return false;
	}

	/// <summary>
	/// only search GUI board active at scene.
	/// </summary>
	/// <returns><c>true</c>, if GUI Board active at scene was had, <c>false</c> otherwise.</returns>
	public static bool HaveGUIBoardActiveAtScene()
	{
		foreach (KeyValuePair<GUIName, GUIControl> control in m_Controls)
		{
			if (control.Value.IsGUIActive())
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// check a GUI is active or disactive?
	/// </summary>
	/// <param name="nameGUI"></param>
	/// <returns></returns>
	public static bool IsGUIActive(GUIName nameGUI)
	{
		GUIControl resultControl;
		if (m_Controls.TryGetValue(nameGUI, out resultControl))
		{
			return resultControl.IsGUIActive();
		}
		else
		{
			Debug.LogError("nameGUI is not available");
			return false;
		}
	}

	/// <summary>
	/// Get a GUIControl
	/// </summary>
	/// <param name="nameGUI"></param>
	/// <returns></returns>
	public static GUIControl GetGUI(GUIName nameGUI)
	{
		GUIControl resultControl;
		if (m_Controls.TryGetValue(nameGUI, out resultControl))
		{
			return resultControl.OnGetGUI();
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// check a GUIName exist in list m_Controls
	/// </summary>
	/// <param name="nameGUI"></param>
	/// <returns></returns>
	public static bool IsContainsKey(GUIName nameGUI)
	{
		if (m_Controls.ContainsKey(nameGUI))
			return true;
		else return false;
	}
}