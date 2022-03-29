using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;

public class levelData : MonoBehaviour 
{
	public CurvySpline playerSpline, RivalSpline;
	public GameObject[] greenlight, redlight;
	public Animator[] Singnalanim;
	public GamemanagerRace Gmr;
	public RivalTrainController RTC;
	public GameObject Rivaldetect, Stagedetect;
	public float Delaytime;
	float time, speed = 1f;
	public float[] trainPositions;
	public float[] Playertrainaxilfront;
	public float[] Playertrainaxilback;
	public float[] RivalPositions;
	public float[] Rivaltrainaxilfront;
	public float[] Rivaltrainaxilback;
	public bool Crashed;
	// Use this for initialization
	void Start () 
	{
		Gmr = FindObjectOfType<GamemanagerRace>();
		RTC = FindObjectOfType<RivalTrainController>();

		for (int i = 0; i < greenlight.Length; i++)
		{
			greenlight[i].SetActive(true);
			Singnalanim[i].SetBool("Barrierdown", false);
			//BR.shipanim.SetBool("Shippullopen", false);
		}

		for (int i = 0; i < redlight.Length; i++)
		{
			redlight[i].SetActive(false);
			Singnalanim[i].SetBool("Barrierup", true);
			//BR.shipanim.SetBool("Shippullclose", true);
		}
	}
    public void SignalControl()
	{	
			Invoke("Delay", Delaytime);
			Invoke("Delay1", 5.0f);
			Gmr.busypanel.SetActive(true);
			Gmr.proceedpanel.SetActive(false);
		    Gmr.RedSignal.SetActive(true);
		   Gmr.GreenSignal.SetActive(false);
		for (int i = 0; i < greenlight.Length; i++)
			{
				greenlight[i].SetActive(false);
				Singnalanim[i].SetBool("Barrierup", false);
			}

			for (int i = 0; i < redlight.Length; i++)
			{
				redlight[i].SetActive(true);
				Singnalanim[i].SetBool("Barrierdown", true);
			}		
	}
	void Delay()
	{
		Gmr.proceedpanel.SetActive(true);
		Gmr.GreenSignal.SetActive(true);
		Gmr.RedSignal.SetActive(false);
		Invoke("GreenSignal", 4f);
		for (int i = 0; i < greenlight.Length; i++)
		{
			greenlight[i].SetActive(true);		
			Singnalanim[i].SetBool("Barrierup", true);
		}

		for (int i = 0; i < redlight.Length; i++)
		{
			redlight[i].SetActive(false);
			Singnalanim[i].SetBool("Barrierdown", false);
		}
		//Debug.Log("Rival GOOOOOOO");
		Gmr.aiController.RTC.RivalGo();
	}

	
	void GreenSignal()
    {
		Gmr.GreenSignal.SetActive(false);
    }
	

	void Delay1()
	{
		Gmr.busypanel.SetActive(false);
	}

	public void RivalWin()
	{
		Debug.Log("RivaLtrigger");
		Stagedetect.SetActive(false);
	}

	public void PlayerWin()
	{
		Debug.Log("Playertrigger");
		Rivaldetect.SetActive(false);
		//Rival.SetActive(false);
	}
}
