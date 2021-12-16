using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class GUIControl : MonoBehaviour
{
	public abstract GUIName NameGUI { get; }
	public event Action OnShowGUI, OnHideGUI;

	private Animator animator;

	// Use this for initialization
	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
	}
	public virtual void Init()
	{

	}
	private void OnDestroy()
	{
		GUIManager.UnRegister(this);
	}

	/// <summary>
	/// Show GUI
	/// </summary>
	public virtual void OnShow()
	{
        OnShowGUI?.Invoke();

        gameObject.SetActive(true);
		animator = GetComponent<Animator>();
		if (animator != null)
		{
			animator.SetBool("isActive", true);
		}
	}

	/// <summary>
	/// Hide GUI
	/// </summary>
	public virtual void OnHide()
	{
        OnHideGUI?.Invoke();

        if (gameObject)
			gameObject.SetActive(false);
		if (animator != null)
			animator.SetBool("isActive", false);
	}

	/// <summary>
	/// Get this GUI
	/// </summary>
	/// <returns></returns>
	public virtual GUIControl OnGetGUI()
	{
		return this;
	}
	/// <summary>
	/// check GUI is active?
	/// </summary>
	/// <returns></returns>
	public virtual bool IsGUIActive()
	{
		if (gameObject.activeInHierarchy)
			return true;
		else return false;
	}
}
