using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayScript : MonoBehaviour {

	public AudioClip tsumetobashi;
	public AudioClip guardgrip;
	public AudioClip guardcomp;
	public AudioClip charging;
	public AudioClip hissatsu;
	public AudioClip snakeswing;
	public AudioClip snakecomp;
	public AudioClip bind;
	public AudioClip holding;
	public AudioClip hold;
	public AudioClip attackswing;
	public AudioClip hert;
	public AudioClip ko_voice;

	private GameObject player1;
	private AudioSource AS1;
	private GameObject player2;
	private AudioSource AS2;
	private AudioSource AS;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find ("1P_Manager");
		AS1 = player1.GetComponent<AudioSource> ();
		player2 = GameObject.Find ("2P_Manager");
		AS2 = player2.GetComponent<AudioSource> ();
		AS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//------------ 以下各音源再生用メソッド -------------

	public void P1_Tsumetobashi(){
		AS1.PlayOneShot (tsumetobashi,1.0f);
		//頭に空白挿入
	}
	public void P2_Tsumetobashi(){
		AS2.PlayOneShot (tsumetobashi,1.0f);
	}

	public void Guard(){
		AS.PlayOneShot (guardgrip, 1.0f);
	}
	public void GuardSuccess(){
		AS.PlayOneShot (guardcomp, 1.0f);
	}
	public void Charge(){
		AS.clip = charging;
		AS.Play ();
		//音源変えたいLoopできるやつ
	}

	public void Snake(){
		AS.PlayOneShot (snakeswing, 1.0f);
	}
	public void SnakeSuccess(){
		AS.PlayOneShot (snakecomp, 1.0f);
	}

	public void Bind(){
		AS.PlayOneShot (bind, 1.0f);
		//音源変更or頭に空白挿入
	}

	public void Hold(){
		AS.PlayOneShot (hold, 1.0f);
	}

	public void Holding(){
		AS.clip = holding;
		AS.Play ();
		//音源変えたいLoopできるやつ
	}

	public void Hert(){
		AS.clip = hert;
		AS.Play ();
		//音源変えたいLoopできるやつ
	}

	public void Resist(){
		AS.PlayOneShot(holding,1.0f);
	}

	public void KO(){
		AS.PlayOneShot(ko_voice,1.0f);
	}

	//---------- 音限停止用メソッド ----------

	public void Stop(){
		AS.Stop ();
		AS1.Stop ();
		AS2.Stop ();
	}

}
