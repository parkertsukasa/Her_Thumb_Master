using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuaringAttackMoveForwardScript : MonoBehaviour {

	private int count = 0;

	private bool start = false;

	private Vector3 move;

	public int playernum;

	// Use this for initialization
	void Start () {
		if (playernum == 1) {
			move = new Vector3 (0.3f, 0, 0);
		} else {
			move = new Vector3 (-0.3f, 0, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (start == true) {
			if (count < 5) {
				transform.position += move;
				count += 1;
			} else {
				start = false;
				count = 0;
			}


		}

	}

	public void MoveForward(){
		start = true;
	}

}
