using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.BobTrap; } }

    [SerializeField] private float timeStun;
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
            dmgTrap.StepOnStun(trapData, timeStun);
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
