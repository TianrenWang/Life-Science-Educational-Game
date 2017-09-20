using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletinBoard : EventTrigger {
    public Image email;

    public override void activateState()
    {
        base.activateState();
        email.enabled = true;
    }

    public override void deactivateState()
    {
        base.deactivateState();
        email.enabled = false;
    }
}
