using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour {

	public GameObject mythumb;
	private FingerScript FS;

	public GameObject guardeffect;
	public GameObject chargeeffect;
	private StateManagerScript SMS;

	// Use this for initialization
	void Start () {
		FS = mythumb.GetComponent<FingerScript> ();
		SMS = GetComponent<StateManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (SMS.nowstate == StateManagerScript.state.check) {
			chargeeffect.SetActive (true);
		} else {
			chargeeffect.SetActive (false);
		}

		
	}

	public void Call_IF(){
		FS.InstanceEffect ();
	}

	public void GuardEffect_On(){
		guardeffect.SetActive (true);
		Invoke ("GuardEffect_Off", 1.0f);
	}

	private void GuardEffect_Off(){
		guardeffect.SetActive (false);
	}

}
