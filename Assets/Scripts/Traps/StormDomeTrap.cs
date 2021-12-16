using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormDomeTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.StormDome; } }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InitPlay) if (!InitPlay.isStart) return;
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if (dmgTrap != null)
        {
            dmgTrap.StepOnSlow(trapData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (InitPlay) if (!InitPlay.isStart) return;
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if (dmgTrap != null)
        {
            dmgTrap.StepOutSlow(trapData);
        }
    }
}
