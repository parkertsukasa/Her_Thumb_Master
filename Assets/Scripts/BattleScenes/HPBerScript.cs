using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HPBerScript : MonoBehaviour {

	public GameObject hpber;
	private Image image;
	private Image myimage;

	// Use this for initialization
	void Start () {
		image = hpber.GetComponent<Image> ();
		myimage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (myimage.fillAmount > image.fillAmount) {
			myimage.fillAmount -= Time.deltaTime * 0.2f;
		}

	}
}
