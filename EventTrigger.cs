using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public Text text;
    public string message;
    protected bool inTrigger;
    protected bool activated;
    public GameObject character;
    public Quaternion rotation;

	// Use this for initialization
	public virtual void Start () {
        inTrigger = false;
        activated = false;
	}
	
	// Update is called once per frame
	public void Update () {
        if (inTrigger)
        {
            if (Input.GetButtonDown("Submit") && !activated)
            {
                activateState();
            }
            else if (Input.GetButtonDown("Submit") && activated)
            {
                deactivateState();
            }
        }
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            text.text = message;
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            text.text = "";
            inTrigger = false;
            deactivateState();
        }
    }

    public virtual void activateState()
    {
        activated = true;
    }

    public virtual void deactivateState()
    {
        activated = false;
    }
}
