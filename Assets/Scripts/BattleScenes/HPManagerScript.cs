using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPManagerScript : MonoBehaviour {

	private GameObject player1;
	private GameObject player2;

	private PlayerHPScript PHPS_1P;
	private PlayerHPScript PHPS_2P;

	private int hp_1p;
	private int hp_2p;

	public GameObject hpb1;
	public GameObject hpb2;

	public Image hpber_1p;
	public Image hpber_2p;

	public GameObject win;
	private Text wintext;



	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		player2 = GameObject.Find ("2P_Manager");
		PHPS_1P = player1.GetComponent<PlayerHPScript> ();
		PHPS_2P = player2.GetComponent<PlayerHPScript> ();

		hpber_1p = hpb1.GetComponent<Image> ();
		hpber_2p = hpb2.GetComponent<Image> ();

		hp_1p = PHPS_1P.hp;
		hp_2p = PHPS_2P.hp;

		wintext = win.GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {

		hp_1p = PHPS_1P.hp;
		hp_2p = PHPS_2P.hp;

		hpber_1p.fillAmount = hp_1p * 0.01f;
		hpber_2p.fillAmount = hp_2p * 0.01f;

		if (hp_1p <= 0) {
			win.SetActive (true);
			wintext.text = "2P WIN!";
			if (Input.anyKeyDown) {
				SceneManager.LoadScene ("Title");
			}
		}
		if (hp_2p <= 0) {
			win.SetActive (true);
			wintext.text = "1P WIN!";
			if (Input.anyKeyDown) {
				SceneManager.LoadScene ("Title");
			}
		}

	}
		
}
