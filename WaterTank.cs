using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : MonoBehaviour {
    private bool flowing;

	// Use this for initialization
	void Start () {
        flowing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (flowing)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit);
            if (hit.transform.root.gameObject.tag.Equals("Player"))
            {
                hit.transform.GetComponent<Beaker>().increaseWater();
            }
        }
	}

    public void setFlow(bool flow)
    {
        flowing = flow;
    }
}
