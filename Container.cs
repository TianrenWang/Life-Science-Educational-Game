using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour {

	public Text label;
	public string chemicalName;
	private bool selected;
	private bool flowing;
	private float flowRate;
	public float flowConstant;
	public GameObject emitter;
	public float concentration;
	public GameObject cap;

	// Use this for initialization
	void Start()
	{
		selected = false;
		label.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (flowing)
		{
			RaycastHit hit;
			Physics.Raycast(cap.transform.position, Vector3.down, out hit);
			if (hit.transform.gameObject.tag.Equals ("Beaker")) {
				hit.transform.GetComponent<Beaker> ().addSolid (chemicalName, flowRate * concentration);
			}
		}
		if (selected)
		{
			if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.Z))
			{
				tilt();
			}
			else if (Input.GetKey(KeyCode.Z))
			{
				//nothing
			}
			else
			{
				untilt();
			}
		}
		flowRate = transform.rotation.eulerAngles.z * flowConstant;
		if (transform.rotation.eulerAngles.z > 45 && !emitter.GetComponent<ParticleSystem> ().isPlaying) {
			flowing = true;
			emitter.GetComponent<ParticleSystem> ().Play (true);

		} 
		else if (transform.rotation.eulerAngles.z < 45 && emitter.GetComponent<ParticleSystem> ().isPlaying){
			flowing = false;
			emitter.GetComponent<ParticleSystem> ().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	public void setSelectState(bool state)
	{
		selected = state;
		if (state == false) {
			label.enabled = false;
			flowing = false;
			emitter.GetComponent<ParticleSystem> ().Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
			transform.parent.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
			gameObject.GetComponent<Collider> ().enabled = true;
		} else {
			gameObject.GetComponent<Collider> ().enabled = false;
			label.enabled = true;
			label.text = chemicalName;
		}
	}

	public void tilt()
	{
		if (transform.parent.transform.rotation.eulerAngles.z < 180) {
			transform.parent.transform.Rotate (new Vector3 (0f, 0f, 20f) * Time.deltaTime);
		}
	}

	public void untilt()
	{
		if (transform.parent.transform.rotation.eulerAngles.z > 1)
		{
			transform.parent.transform.Rotate(new Vector3(0f, 0f, -20f) * Time.deltaTime);
		}
		if (transform.parent.transform.rotation.eulerAngles.z > 300) {
			transform.parent.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
		}
	}
}
