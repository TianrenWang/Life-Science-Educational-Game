using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : EventTrigger {
    public GameObject tap;

    public override void activateState()
    {
        base.activateState();
        tap.GetComponent<ParticleSystem>().Play();
        tap.GetComponent<WaterTank>().setFlow(true);
    }

    public override void deactivateState()
    {
        base.deactivateState();
        tap.GetComponent<ParticleSystem>().Stop();
        tap.GetComponent<WaterTank>().setFlow(false);
    }
}
