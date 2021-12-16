using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.Saw; } }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
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
