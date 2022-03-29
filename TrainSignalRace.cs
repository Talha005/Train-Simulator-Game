using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSignalRace : MonoBehaviour {

	// Use this for initialization
	public GameObject greenlight, redlight;
	public GameObject busypanel;
	public float Delaytime;

	 void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Trainplayer"))
		{
			Debug.Log("Trainplayer");
			Invoke("Delay", Delaytime);
			Invoke("Delay1", 5.0f);
			busypanel.SetActive(true);
			greenlight.SetActive(false);
			redlight.SetActive(true);
		}
	}
	void Delay()
	{	
		redlight.SetActive (false);
		greenlight.SetActive (true);
    }
    void Delay1()
	{
		busypanel.SetActive (false);
	}

}
