﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSpinXAxisScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right * 360.0f * Time.deltaTime);
	}
}
