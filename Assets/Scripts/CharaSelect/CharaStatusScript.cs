using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaStatusScript : MonoBehaviour {

	public States states1p = new States (1, 1, 1);
	public States states2p = new States (1, 1, 1);

	public float max = 1.2f;
	public float mid = 1.0f;
	public float min = 0.8f;

	public States[] statespattern = new States[3];

	public int num1p = 0;
	public int num2p = 0;


	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		statespattern [0] =  new States (max, min, mid);
		statespattern [1] =  new States (min, mid, max);
		statespattern [2] =  new States (mid, max, min);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (num2p);

		states1p = statespattern [num1p];
		states2p = statespattern [num2p];

	}
		
}
