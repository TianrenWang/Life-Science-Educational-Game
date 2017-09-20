using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public float speed = 10.0F;
    private bool normalState;
    private Camera view;
    public GameObject eventTriggers;
    private bool holdingItem;
    private GameObject heldItem;
    public Transform laboratoryItems;

	// Use this for initialization
	void Start () {
        normalState = true;
        Cursor.lockState = CursorLockMode.Locked;
        view = GetComponentInChildren<Camera>();
        holdingItem = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (normalState)
        {
            if (Input.GetButtonDown("Fire1") && !holdingItem)
            {
                setEventTriggerActivity(false);
                Vector3 rayOrigin = view.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
                RaycastHit hit;
                Physics.Raycast(rayOrigin, view.transform.forward, out hit);
                
                //This currently works only on the right most beaker, because that one has a collider
                if (hit.transform.root.gameObject.tag.Equals("LaboratoryItem"))
                {
                    heldItem = hit.transform.gameObject;
                    hit.transform.parent.parent = this.transform;
                    hit.transform.parent.position = view.ViewportToWorldPoint(new Vector3(.5f, 0f, 0)) + view.transform.forward + Vector3.down * 0.2f;
					if (hit.transform.parent.gameObject.tag.Equals ("Beaker"))
						hit.transform.GetComponent<Beaker> ().setSelectState (true);
					else if (hit.transform.parent.gameObject.tag.Equals ("Container"))
						hit.transform.GetComponent<Container> ().setSelectState (true);
                    holdingItem = true;
                }
                setEventTriggerActivity(true);
            }
            else if (Input.GetButtonDown("Fire1") && holdingItem)
            {
                setEventTriggerActivity(false);
				this.GetComponent<Collider>().enabled = false;
				heldItem.GetComponent<Collider> ().enabled = false;
                Vector3 rayOrigin = view.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
                RaycastHit hit;
                Physics.Raycast(rayOrigin, view.transform.forward, out hit);
				this.GetComponent<Collider>().enabled = true;
				heldItem.GetComponent<Collider> ().enabled = true;

                if (!hit.transform.root.gameObject.tag.Equals("Environment"))
                {
                    heldItem.transform.parent.parent = laboratoryItems;
                    heldItem.transform.parent.position = hit.point;
					if (heldItem.transform.parent.gameObject.tag.Equals ("Beaker"))
                    	heldItem.GetComponent<Beaker>().setSelectState(false);
					else if (heldItem.gameObject.tag.Equals ("Container"))
						heldItem.GetComponent<Container> ().setSelectState (false);
                    holdingItem = false;
                    heldItem = null;
                }
                setEventTriggerActivity(true);
            }

            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.fixedDeltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);

            if (Input.GetKeyDown("escape"))
                Cursor.lockState = CursorLockMode.None;
        }
	}

    private void setEventTriggerActivity(bool state)
    {
        eventTriggers.SetActive(state);
    }
}
