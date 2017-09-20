using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleTreatment : EventTrigger {
	public Image texts;

	public override void activateState()
	{
		base.activateState();
		texts.enabled = true;
	}

	public override void deactivateState()
	{
		base.deactivateState();
		texts.enabled = false;
	}
}
