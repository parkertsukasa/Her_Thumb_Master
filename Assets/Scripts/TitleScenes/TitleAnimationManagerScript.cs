using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnimationManagerScript : MonoBehaviour {

	public enum TitleState
	{
		logo,select_cpu,select_2p,select_option,select_credit,option
	}

	public TitleState nowstate;

	private Animator animator;

	private GameObject selecticon;//「→」画像

	public float input;

	public bool selectok = true;

	private GameObject optiongo;
	private Animator optionanim;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		selecticon = GameObject.Find ("SelectIcon");
		input = 0.0f;
		optiongo = GameObject.Find ("OptionGroup");
		optionanim = optiongo.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("1P_LeftVertical") >= 0.5f || Input.GetAxis ("1P_LeftVertical") <= -0.5f) {
			input = Input.GetAxis ("1P_LeftVertical");
		} else if (Input.GetAxis ("2P_LeftVertical") >= 0.5f || Input.GetAxis ("2P_LeftVertical") <= -0.5f) {
			input = Input.GetAxis ("2P_LeftVertical");
		} else {
			input = 0.0f;
		}
		/*
		if (Input.GetAxis ("JoyCon1_Vertical") != 0.0f) {
			input = Input.GetAxis ("JoyCon1_Vertical");
		}
		if (Input.GetAxis ("JoyCon2_Vertical") != 0.0f) {
			input = Input.GetAxis ("JoyCon2_Vertical");
		}
		*/

		SelectMode ();

	}

	void ReSelect(){
		selectok = true;
	}

	void SelectMode(){
		switch(nowstate){
		case TitleState.logo:
			if (Input.anyKeyDown) {
				animator.SetBool ("GoMode", true);
				nowstate = TitleState.select_cpu;
			}
			break;

		case TitleState.select_cpu:
			selecticon.transform.localPosition = new Vector3 (122, -48, 0);
			if (Input.GetButtonDown ("1P_Circle") || Input.GetButtonDown ("2P_Circle")) {
				//シーン移動
			}
			if (selectok == true) {
				//「↑」入力
				if (input >= 0.8f) {
					nowstate = TitleState.select_2p;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
				//「↓」入力
				if (input <= -0.8f) {
					nowstate = TitleState.select_credit;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
			}
			break;

		case TitleState.select_2p:
			selecticon.transform.localPosition = new Vector3 (142, -92, 0);
			if (Input.GetButtonDown ("1P_Circle") || Input.GetButtonDown ("2P_Circle")) {
				SceneManager.LoadScene ("Battle");//シーン移動
			}
			if (selectok == true) {
				//「↑」入力
				if (input >= 0.8f) {
					nowstate = TitleState.select_option;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
				//「↓」入力
				if (input <= -0.8f) {
					nowstate = TitleState.select_cpu;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
			}
			break;

		case TitleState.select_option:
			selecticon.transform.localPosition = new Vector3 (188, -138, 0);
			if (Input.GetButtonDown ("1P_Circle") || Input.GetButtonDown ("2P_Circle")) {
				optionanim.SetBool ("GoOption", true);
				nowstate = TitleState.option;
			}
			if (selectok == true) {
				//「↑」入力
				if (input >= 0.8f) {
					nowstate = TitleState.select_credit;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
				//「↓」入力
				if (input <= -0.8f) {
					nowstate = TitleState.select_2p;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
			}
			break;

		case TitleState.select_credit:
			selecticon.transform.localPosition = new Vector3 (196, -178, 0);
			if (Input.GetButtonDown ("1P_Circle") || Input.GetButtonDown ("2P_Circle")) {
				SceneManager.LoadScene ("Credit");//シーン移動
			}
			if (selectok == true) {
				//「↑」入力
				if (input >= 0.8f) {
					nowstate = TitleState.select_cpu;
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
				//「↓」入力
				if (input <= -0.8f) {
					nowstate = TitleState.select_option; 
					selectok = false;
					Invoke ("ReSelect", 0.2f);
				}
			}
			break;

		case TitleState.option:
			if (Input.GetButtonDown ("1P_Cross") || Input.GetButtonDown ("2P_Cross")) {
				optionanim.SetBool ("GoOption", false);
				nowstate = TitleState.select_option;
			}
			break;
		}
	}
}
