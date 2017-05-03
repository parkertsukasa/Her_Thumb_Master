using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPScript : MonoBehaviour {

	public int hp;

	// Use this for initialization
	void Start () {
		hp = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage_L (){
		hp -= 40;
	}

	public void Damage_M(){
		hp -= 20;
	}

	public void Damage_S(){
		hp -= 10;
	}
}
