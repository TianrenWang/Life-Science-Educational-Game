using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Experiment : EventTrigger {
	public override void activateState()
	{
		base.activateState();
		SceneManager.LoadScene("_Titration", LoadSceneMode.Single);
	}
}
