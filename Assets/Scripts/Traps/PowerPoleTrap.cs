using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPoleTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.PowerPole; } }

    private void OnTriggerEnter(Collider other)
    {
        if (InitPlay) if (!InitPlay.isStart) return;
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if (dmgTrap != null)
        {
            dmgTrap.StepOnBless(trapData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (InitPlay) if (!InitPlay.isStart) return;
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if (dmgTrap != null)
        {
            dmgTrap.StepOutBless(trapData);
        }
    }

}
