using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beaker : MonoBehaviour
{

    private float waterVolume;
    public float capacity;
	private string[] compounds;
    private float[] compoundQuantity;
	private float[] compoundConc;
    public Text water;
    private bool selected;

    // Use this for initialization
    void Start()
    {
		compounds = new string[2] { "HCl", "NaHCO3" };
		compoundQuantity = new float[compounds.Length];
		compoundConc = new float[compounds.Length];
        waterVolume = 0f;
        selected = false;
		water.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (selected) {
			if (!water.enabled && !isEmpty ())
				water.enabled = true;
			else if (water.enabled && isEmpty ())
				water.enabled = false;
			
			if (Input.GetMouseButton (1) && Input.GetKey (KeyCode.Z)) {
				tilt ();
			} else if (Input.GetKey (KeyCode.Z)) {
				//nothing
			} else {
				untilt ();
			}

			water.text = "Water: " + waterVolume + "mL";
			for (int i = 0; i < compounds.Length; i++) {
				if (compoundQuantity [i] != 0) {
					water.text = water.text + System.Environment.NewLine + compounds [i] + ": " + compoundQuantity[i] + "mol";
				}
			}
		} else {
			water.enabled = false;
		}
		updateConcentration ();
    }

    public void increaseWater()
    {
        if (capacity > waterVolume)
        {
            waterVolume++;
        }
    }

	public void addAqueous(string compound, float concentration)
	{
		if (capacity > waterVolume)
		{
			waterVolume++;
			for (int i = 0; i < compound.Length; i++)
			{
				if (compounds [i].Equals (compound)) {
					compoundQuantity [i] += concentration;
				}
			}
		}
	}

	public void addSolid(string compound, float quantity)
	{
		for (int i = 0; i < compounds.Length; i++)
		{
			if (compounds [i].Equals (compound)) {
				compoundQuantity [i] += quantity;
			}
		}
	}

	public void updateConcentration()
	{
		for (int i = 0; i < compounds.Length; i++)
		{
			compoundConc [i] = compoundQuantity [i] / waterVolume;
		}
	}

    public void setSelectState(bool state)
    {
        selected = state;
		if (state == false) {
			transform.parent.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);
			water.enabled = false;
		}
    }

    public void tilt()
    {
        transform.parent.transform.Rotate(new Vector3(0f,0f,20f) * Time.deltaTime);
    }

    public void untilt()
    {
        if (transform.parent.transform.rotation.z > 0)
        {
            transform.parent.transform.Rotate(new Vector3(0f, 0f, -20f) * Time.deltaTime);
        }   
    }

	public bool isEmpty(){
		for (int i = 0; i < compounds.Length; i++) {
			if (compoundQuantity [i] != 0)
				return false;
		}
		if (waterVolume != 0)
			return false;
		return true;
	}
}
