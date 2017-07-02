using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharaSelectUIrScript : MonoBehaviour {

	private GameObject CSM;
	private CharaStatusScript CSS;

	public Text text1p;
	public Text text2p;

	public Text attack1;
	public Text defence1;
	public Text speed1;

	public Text attack2;
	public Text defence2;
	public Text speed2;

	public GameObject hert1;
	public GameObject star1;
	public GameObject snow1;
	public GameObject hert2;
	public GameObject star2;
	public GameObject snow2;

	private bool selected_1p = false;
	private bool selected_2p = false;

	public GameObject select1;
	public GameObject select2;


	// Use this for initialization
	void Start () {
		CSM = GameObject.Find ("CharaStatusManager");
		CSS = CSM.GetComponent<CharaStatusScript> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("1P_Circle")) {
			selected_1p = true;
			select1.SetActive (true);
		}
		if (Input.GetButtonDown ("1P_Cross")) {
			selected_1p = false;
			select1.SetActive (false);
		}


		if (Input.GetButtonDown ("2P_Circle")) {
			selected_2p = true;
			select2.SetActive (true);
		}
		if (Input.GetButtonDown ("2P_Cross")) {
			selected_2p = false;
			select2.SetActive (false);
		}

		if (selected_1p == true && selected_2p == true) {
			SceneManager.LoadScene ("Battle");
		}

		
		switch (CSS.num1p) {
		case 0:
			text1p.text = "ワイルドハート";
			text1p.color = new Color (0.9f, 0.5f, 0.85f);
			attack1.text = "★★★";
			defence1.text = "★☆☆";
			speed1.text = "★★☆";
			hert1.SetActive (true);
			star1.SetActive (false);
			snow1.SetActive (false);
			break;
		case 1:
			text1p.text = "スピードスター";
			text1p.color = new Color (0.75f,0.8f,0.3f);
			attack1.text = "★☆☆";
			defence1.text = "★★☆";
			speed1.text = "★★★";
			hert1.SetActive (false);
			star1.SetActive (true);
			snow1.SetActive (false);
			break;
		case 2:
			text1p.text = "スノークリスタル";
			text1p.color =  new Color (0.4f,0.65f,0.95f);
			attack1.text = "★★☆";
			defence1.text = "★★★";
			speed1.text = "★☆☆";
			hert1.SetActive (false);
			star1.SetActive (false);
			snow1.SetActive (true);
			break;
		}

		switch (CSS.num2p) {
		case 0:
			text2p.text = "ワイルドハート";
			text2p.color = new Color (0.9f,0.5f,0.85f);
			attack2.text = "★★★";
			defence2.text = "★☆☆";
			speed2.text = "★★☆";
			hert2.SetActive (true);
			star2.SetActive (false);
			snow2.SetActive (false);
			break;
		case 1:
			text2p.text = "スピードスター";
			text2p.color =  new Color (0.75f,0.8f,0.3f);
			attack2.text = "★☆☆";
			defence2.text = "★★☆";
			speed2.text = "★★★";
			hert2.SetActive (false);
			star2.SetActive (true);
			snow2.SetActive (false);
			break;
		case 2:
			text2p.text = "スノークリスタル";
			text2p.color = new Color (0.4f,0.65f,0.95f);
			attack2.text = "★★☆";
			defence2.text = "★★★";
			speed2.text = "★☆☆";
			hert2.SetActive (false);
			star2.SetActive (false);
			snow2.SetActive (true);
			break;
		}
	}
}
;