using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour {

	public GameObject mythumb;
	private FingerScript FS;

	public GameObject guardeffect;
	public GameObject chargeeffect;
	private StateManagerScript SMS;

	public GameObject pistoleffect;

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
		StartCoroutine ("Wait_Frame");
	}

	private void GuardEffect_Off(){
		guardeffect.SetActive (false);
	}
		
	public void PistolEffect_On(){
		pistoleffect.SetActive (true);
		Invoke ("PistolEffect_Off", 0.5f);
	}

	private void PistolEffect_Off(){
		pistoleffect.SetActive (false);
	}


	private IEnumerator Wait_Frame(){
		for (int i = 0; i < 7; i++) {
			yield return null;
		}
		guardeffect.SetActive (true);
		Invoke ("GuardEffect_Off", 1.0f);
	}

}
