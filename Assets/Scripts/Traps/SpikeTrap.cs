using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.Spike; } }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if(dmgTrap != null)
        {
            dmgTrap.StepOnBless(trapData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if (dmgTrap != null)
        {
            dmgTrap.StepOutBless(trapData);
        }
    }
}
