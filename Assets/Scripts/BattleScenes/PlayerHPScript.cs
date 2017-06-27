using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPScript : MonoBehaviour {


	private GameObject battlemanager;
	private HPManagerScript HPMS;

	public int playernum;

	public float hp;

	private float fixer;

	States mystates;
	States oppstates;

	// Use this for initialization
	void Start () {
		battlemanager = GameObject.Find ("Battle_Manager");
		HPMS = battlemanager.GetComponent<HPManagerScript> ();

		hp = 100;
		if (playernum == 1) {//1Pの場合
			mystates = new States(HPMS.states_1p.speed,HPMS.states_1p.attack,HPMS.states_1p.defense);
			oppstates = new States(HPMS.states_2p.speed,HPMS.states_2p.attack,HPMS.states_2p.defense);
		} else {//2Pの場合
			mystates = new States(HPMS.states_2p.speed,HPMS.states_2p.attack,HPMS.states_2p.defense);
			oppstates = new States(HPMS.states_1p.speed,HPMS.states_1p.attack,HPMS.states_1p.defense);
		}
		fixer = mystates.attack / oppstates.defense;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage_L (){
		hp -= 30 * fixer;
	}

	public void Damage_M(){
		hp -= 20 * fixer;
	}

	public void Damage_S(){
		hp -= 10 * fixer;
	}
}
