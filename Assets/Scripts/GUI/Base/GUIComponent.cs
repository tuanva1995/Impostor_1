using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GUIComponent : GUIControl
{

    public GUIName nameGUI;
    public event Action OnInit;

    /// <summary>
    /// get GUI name
    /// </summary>
	public override GUIName NameGUI
    {
        get
        {
            return nameGUI;
        }
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Init()
    {
        base.Init();
        Initialize();
    }

    public override void OnShow()
    {
        base.OnShow();
        UltimateJoystick.DisableJoystick("Movement");
    }
    public override void OnHide()
    {
        if (GUIManager.IsGUIActive(nameGUI))
            UltimateJoystick.EnableJoystick("Movement");
        base.OnHide();
    }
    private void Initialize()
    {
        OnInit?.Invoke();
    }
}
