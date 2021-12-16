using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventDisPatcher : MonoBehaviour {

	/**
    * - author: datbq
    * - date created: 30/11/2018
    * - description:
    * this is a base class for using Observer Pattern, receive and forward information.
    */

	#region Singleton
	static EventDisPatcher s_instance;
	public static EventDisPatcher Instance
	{
		get
		{
			if(s_instance == null)
			{
				GameObject singletonObj = new GameObject();
				s_instance = singletonObj.AddComponent<EventDisPatcher>();
				singletonObj.name = "Singleton - EventDisPatcher";
			}
			return s_instance;
		}
		private set{}
	}

	public static bool HasInstance()
	{
		return s_instance != null;
	}
	private void Awake()
	{
		if(HasInstance() && s_instance.GetInstanceID() != this.GetInstanceID())
		{
			Destroy(this);
		}
		else
		{
			s_instance = this as EventDisPatcher;
		}
	}
	private void OnDestroy()
	{
		if(s_instance == this)
		{
			ClearAllListener();
			s_instance = null;
		}
	}
	#endregion
	Dictionary<EventID, Action<object>> listeners = new Dictionary<EventID, Action<object>>();

	public void RegisterListener(EventID eventID, Action<object> callback)
	{
		if(listeners.ContainsKey(eventID))
		{
			listeners[eventID] += callback;
		}
		else
		{
			listeners.Add(eventID, null);
			listeners[eventID] += callback; 
		}
	}
	public void PostEvent(EventID eventID, object param = null)
	{
		if (!listeners.ContainsKey(eventID))
			return;
		var callback = listeners[eventID];
		if (callback != null)
			callback(param);
		else
			listeners.Remove(eventID);
	}
	public void RemoveListener(EventID eventID, Action<object> callback)
	{
		if (listeners.ContainsKey(eventID))
			listeners[eventID] -= callback;
	}
	public void ClearAllListener()
	{
		listeners.Clear();
	}
}

///<summary>
/// extension class
/// </summary>
public static class EventDisPatcherExtension
{
	public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
	{
		EventDisPatcher.Instance.RegisterListener(eventID, callback);
	}
	public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
	{
		EventDisPatcher.Instance.PostEvent(eventID, param);
	}
	public static void PostEvent(this MonoBehaviour listener, EventID eventID)
	{
		EventDisPatcher.Instance.PostEvent(eventID, null);
	}
}