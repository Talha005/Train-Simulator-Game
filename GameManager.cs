using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using Invector.CharacterController;
public class GameManager : MonoBehaviour {

	// Use this for initialization

	public GameObject[] gameplaypanel;
	public GameObject[] trainsimplayer;
	public GameObject[] EngineMat;
	public GameObject[] BuggieMat1;
	public GameObject[] BuggieMat2;
	public GameObject[] BuggieMat3;
	public GameObject[] BuggieMat4;
	private  int levelselectionnumber;
	public GameObject levels;
	private float currenttime;
	//public Text showtimer;
	private float timesteps=1.0f;
	private  bool isnotwin;
	private static bool showadonce = true;
	public GameObject Trainpos;
	public GameObject canvasb;
	private int cameraswitch=1;
	public GameObject cameraaon;
	public GameObject inputtouchcampanel;
	public GameObject Skipsbtn,Skipsbtn2;
	public Text leveltext;
	public Text scoretext,totalscore;
	//public GameObject thirdpersoncont,thirdpersoncame;
	public GameObject traincame;
	public GameObject instructionpanel,instructionpanellevel,ticketrecevid;
	public GameObject fadeinfadeout;
	IEnumerator Thirdpersoncolliderin;
	IEnumerator Thirdpersoncollideroff;
	public static string instructext;
	public GameObject Canvasnew;
	public GameObject[] Thirdpersonpos;
	//private vThirdPersonController Inputcha2;
	public GameObject[] Thirdcollider;
	private bool iswin,isfail;
	public GameObject Tutpanel1,Tutpanel2,Tutpanel3,Tutpanel4;
    public GameObject lvlcmpEffect1, lvlcmpEffect2;
    public Slider Raceslide;
	bool forraceslider;
	public int[] levelwisescroe;
    private GoogleMobileAdsDemoScript ads;
    RectangleBannerAd_aa recAds;
	AdaptiveBanner adapAds;
    private bool pausedad;
	private bool IsAlreadyPaused=false;
	public GameObject rateuspanel;
	public GameObject[] Thirdpersoneyes;
    public levelsData[] Levels;
    public AudioSource levelfinishaudio, Trainannouncementaudio, Musicaudio;
	public AudioClip finish, Trainannouncment;
    public GameObject LvlcmpInstruction;
	public SpeedChangeBtn Speed;
	public GameObject TrafficManagerPlayNotification;
	[System.Serializable]
    public class levelsData
    {
        public string Scenename;
        public int levelno;
        public int nextLevelUnlock;
    }

	void Start ()
	{
        CustomAnalytics.eventMessage("SampleScene"+ (levelselectionnumber + 1 ) +"start" );
        Handheld.StopActivityIndicator ();
		trainsimplayer [PlayerPrefs.GetInt ("Trainnum")].SetActive (true);
		Time.timeScale=1f;
        ads = FindObjectOfType<GoogleMobileAdsDemoScript>();
        if (ads)
        {
            //ads.MGLink();
        }
        recAds = FindObjectOfType<RectangleBannerAd_aa>();
        if (recAds)
        {
            recAds.MGLink();
        }
		adapAds = FindObjectOfType<AdaptiveBanner>();
		if(adapAds)
		{
			adapAds.MGLink();
		}
        if (!PlayerPrefs.HasKey("Unlocklevels")) 
		{
			PlayerPrefs.SetInt ("Unlocklevels", 1);
		}
		AudioListener.volume = 1;
		//parentTutpanel.SetActive (true);
//		if (SystemInfo.systemMemorySize > 0 && SystemInfo.systemMemorySize <= 2200) 
//		{
//			Cameraplayer.GetComponent<Camera> ().farClipPlane = 300;
//		}
//		else if (SystemInfo.systemMemorySize > 2200 && SystemInfo.systemMemorySize <= 3200) 
//		{
//			Cameraplayer.GetComponent<Camera> ().farClipPlane = 400;
//		}
//		else if (SystemInfo.systemMemorySize > 3200) 
//		{
//			Cameraplayer.GetComponent<Camera> ().farClipPlane = 500;
//		}
//		 Handheld.StopActivityIndicator ();
		//PlayerPrefs.DeleteAll ();
		scoretext.text = PlayerPrefs.GetInt ("Cashscore").ToString ();
		levelselectionnumber=Menu.Levelnumberspass;
		levels.transform.GetChild(levelselectionnumber-1).transform.gameObject.SetActive(true);
		trainsimplayer[PlayerPrefs.GetInt ("Trainnum")].gameObject.transform.position=Trainpos.transform.GetChild(levelselectionnumber-1).gameObject.transform.position;
		trainsimplayer[PlayerPrefs.GetInt ("Trainnum")].gameObject.transform.rotation=Trainpos.transform.GetChild(levelselectionnumber-1).gameObject.transform.rotation;
		EngineMat[PlayerPrefs.GetInt("Trainnum")].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + PlayerPrefs.GetInt("Trainnum") + "texture"));
		BuggieMat1[PlayerPrefs.GetInt("Trainnum")].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + PlayerPrefs.GetInt("Trainnum") + "texture"));
		BuggieMat2[PlayerPrefs.GetInt("Trainnum")].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + PlayerPrefs.GetInt("Trainnum") + "texture"));
		BuggieMat3[PlayerPrefs.GetInt("Trainnum")].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + PlayerPrefs.GetInt("Trainnum") + "texture"));
		BuggieMat4[PlayerPrefs.GetInt("Trainnum")].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + PlayerPrefs.GetInt("Trainnum") + "texture"));
		//thirdpersoncont.transform.position=Thirdpersonpos[levelselectionnumber-1].gameObject.transform.position;
		//thirdpersoncont.transform.rotation=Thirdpersonpos[levelselectionnumber-1].gameObject.transform.rotation;
        Menu.Levelnumbersunlock = Levels[levelselectionnumber].nextLevelUnlock;
        leveltext.text = Menu.Levelnumbersunlock.ToString ();
		//Inputcha2 = thirdpersoncont.GetComponent<vThirdPersonController>();
		IsAlreadyPaused=false;
		Trainannouncementaudio.PlayOneShot(Trainannouncment, 0.6f);
		if (PlayerPrefs.GetInt("Musiconoff") == 0)
		{
			//Musicaudio.volume = 1f;
			Musicaudio.mute = false;
			Trainannouncementaudio.mute = false;
		}
		if (PlayerPrefs.GetInt("Musiconoff") == 1)
		{
			//Musicaudio.volume = 0f;
			Musicaudio.mute = true;
			Trainannouncementaudio.mute = true;

		}
		//		adobj = (GoogleMobileAdsDemoScript)FindObjectOfType (typeof(GoogleMobileAdsDemoScript));
		//		#if !UNITY_EDITOR
		//		adobj.HideBannerFunc1();
		//		adobj.HideBannerFunc();
		//		#endif
		/*if (PlayerPrefs.GetInt("Musiconoff") == 0)
        {
            //Musicaudio.volume = 1f;
            Musicaudio.mute = false;
        }
        if (PlayerPrefs.GetInt("Musiconoff") == 1)
        {
            //Musicaudio.volume = 0f;
            Musicaudio.mute = true;
        }*/
	}
	private void Tutof()
	{
		Tutpanel4.SetActive(false);
	}
	//public void jummpchrater()
	//{
	//	//Inputcha2.Jump();
	//}
	void LateUpdate()
	{
		if(IsAlreadyPaused)
			Time.timeScale = 0;
	}
	public void GameplayController(string Gameplaybtn)
	{

		switch(Gameplaybtn)
		{

		case "Home": 
			Handheld.StartActivityIndicator ();
			Time.timeScale = 1;
			gameplaypanel [3].SetActive (true);  //for loading
			PlayerPrefs.SetInt ("IsNext", 0);
			IsAlreadyPaused =false;
            //hideBanner();
			hideadapBanner();
            hideRecBanner();
            showInterstitial();
            SceneManager.LoadScene(1);			
			break; 
		case "Restart": 
			Time.timeScale = 1;
            //hideBanner();
			hideadapBanner();
            hideRecBanner();
            Handheld.StartActivityIndicator ();
			gameplaypanel [3].SetActive (true);
			IsAlreadyPaused =false;
			AudioListener.volume = 1;
            SceneManager.LoadScene(2);
			break; 
		case "Resume":
                //hideBanner();
				hideadapBanner();
                hideRecBanner();   
			
				if (Raceslide.value > 0)
				{
					Time.timeScale = 2f;
					speed1xbtn.SetActive(true);
					speed2xbtn.SetActive(false);
				}
                else
                {
					Time.timeScale = 1;
				}
				gameplaypanel [2].SetActive (false);
			AudioListener.volume = 1;
			IsAlreadyPaused =false;
			if (PlayerPrefs.GetInt ("Musiconff") == 0) 
			{

				//mainaudiosource.mute = false;

			} else 
			{
				//mainaudiosource.mute = true;
			}
			break;
		case "Rateus": 
			PlayerPrefs.SetInt ("oncetimeshow", 1);
			Application.OpenURL ("market://details?id=com.bettergames.euro.train.driver.sim");
			rateuspanel.SetActive (false);
			break;
		case "Laternow": 
			rateuspanel.SetActive (false);
			break;
		case "Paused":
				//showBanner();
		    showAdapbanner();			
            showRecBanner();
            Time.timeScale = 0;
			IsAlreadyPaused = true;
			PlayerPrefs.SetInt ("pausecounter", PlayerPrefs.GetInt ("pausecounter") + 1);
			AudioListener.volume = 0;
			pausedad = true;
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			adobj.ShowBannerFunc1 ();
//			adobj.ShowBannerFunc ();
//			}
//			#endif
//			if (PlayerPrefs.GetInt ("pausecounter") == 2) 
//			{
//				PlayerPrefs.SetInt ("pausecounter", 0);
//				if (Application.internetReachability != NetworkReachability.NotReachable) 
//				{
//					//Change the Text
//					Debug.Log ("Internet Available");
//					#if !UNITY_EDITOR
//					if(adobj!=null)
//					adobj.ShowInterstitial ();
//					#endif
//
//				} 
//			}
			gameplaypanel [2].SetActive (true);
               
			//mainaudiosource.mute = true;
			break;
		case "Next":
		    //levelcomplete();
            //hideBanner();
				hideadapBanner();
            hideRecBanner();
            Time.timeScale = 1;
            showInterstitial();
            Handheld.StartActivityIndicator ();
			gameplaypanel [3].SetActive (true);  //for loading
			PlayerPrefs.SetInt ("IsNext", 1);
			IsAlreadyPaused =false;
            Menu.Levelnumberspass = Levels[levelselectionnumber].levelno;
		    SceneManager.LoadScene(Levels[levelselectionnumber].Scenename);
				// Menu.Levelnumberscargo += 1;
				break;

		}

	}
	void OnApplicationPause (bool isPause)
	{
		if (isPause) { // App going to background
			Time.timeScale = 0;
		} else {
			if(IsAlreadyPaused)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}
	}


	//	IEnumerator Timerwait ()
	//	{
	//		while (currenttime > 0) 
	//		{
	//			yield return new WaitForSeconds (timesteps);
	//			currenttime -= timesteps;
	//			int minutes = Mathf.FloorToInt(currenttime / 60F);
	//			int seconds = Mathf.FloorToInt(currenttime - minutes * 60);
	//			string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
	//			//showtimer.text = niceTime.ToString ();
	//		}
	//		if (currenttime == 0&&!isnotwin) 
	//		{
	//			levelFailed ();
	//		}
	//
	//	}

	
	// Update is called once per frame
	void Update () 
	{
		if (!forraceslider) 
		{
			if (Raceslide.value > 1) 
			{
				
				indictorspeed (false);		
			} 
		}	

		//if(Raceslide.value ==0)
  //      {
		//	Time.timeScale = 1;
		//	Debug.Log("Spriterenderhere");
		//	Speed.SpeedIncrease = true;
		//	Speed.Speedincrease() ;
  //      }
	}

	public void SliderSpeedbtnfunc()
    {
		if(Raceslide.value == 0)
        {
			Time.timeScale = 1;
			speed1xbtn.SetActive(false);
			speed2xbtn.SetActive(true);
        }
		else
        {
			speed2xbtn.SetActive(true);
        }
    }
	public void canvasonoff(bool canonoff)
	{
		if (canonoff) 
		{
			
			//thirdpersoncont.SetActive (true);
			//thirdpersoncame.SetActive (true);
			//thirdpersoncont.GetComponent<vThirdPersonInput> ().enabled = false;
			//traincame.SetActive (false);
			Skipsbtn.SetActive (false);
			//instructionpanel.SetActive (true);
			

		}
		else 
		{
			//canvasb.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			inputtouchcampanel.SetActive (false);
		}
	}

	public void indictorbrake(bool braketutonff)
	{
		if (braketutonff == false) 
		{
			Tutpanel2.SetActive (false);
		} 
		else 
		{
			Tutpanel2.SetActive (true);
		}
	}

	public GameObject speed1xbtn,speed2xbtn;
	public void increaseSpeedfunc()
	{
		if (Raceslide.value > 0)
		{
			Time.timeScale = 2f;
			speed1xbtn.SetActive(true);
			speed2xbtn.SetActive(false);
		}
	}
	public void decreaseSpeedfunc()
    {
			Time.timeScale = 1f;
			speed1xbtn.SetActive(false);
			speed2xbtn.SetActive(true);	
	}
	public void indictorspeed(bool speedtutonff)
	{
		if (speedtutonff == false) 
		{
			Tutpanel1.SetActive (false);
		} 
		else 
		{
			Tutpanel1.SetActive (true);
			forraceslider = false;
		}

	}
	public void indictorcamera()
	{
		if (!PlayerPrefs.HasKey ("Tut3")) 
		{
			Tutpanel4.SetActive (true);
			PlayerPrefs.SetInt ("Tut3",1);
			Invoke ("Tutof",2.0f);
		}
	}

	public void cameraswitchnew()
	{
		
		if (cameraswitch == 0) 
		{
            print("CAMERACHANGE");
			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			FindObjectOfType<Trainmanagers> ().resetcame ();
			inputtouchcampanel.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().distance = 15f;
			cameraaon.GetComponent<SmoothFollow> ().height = 5f;
			cameraswitch = 1;


		} else if (cameraswitch == 1) 
		{
			cameraaon.GetComponent<SmoothFollow> ().enabled = false;
			cameraaon.GetComponent<Came> ().enabled = true;
			FindObjectOfType<Trainmanagers> ().resetcame ();
			inputtouchcampanel.SetActive (true);
			cameraswitch = 2;
			if (PlayerPrefs.GetInt ("Tut3")==1) 
			{
				Tutpanel4.SetActive (true);
				PlayerPrefs.SetInt ("Tut3",0);
				Invoke ("Tutof",2.0f);
			}
        }
        else if (cameraswitch == 2) 
		{
			cameraswitch = 0;
			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			inputtouchcampanel.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().distance = 0.1f;
			cameraaon.GetComponent<SmoothFollow> ().height = 0.0f;
			FindObjectOfType<Trainmanagers> ().frontcam ();
		}					
	}

	public void TMfunc(int levelnums)
	{
		TrafficManagerPlayNotification.SetActive(false);
		gameplaypanel[3].SetActive(true);
		Menu.Levelnumberstraffic = levelnums;
		Musicaudio.mute = false;
		PlayerPrefs.SetInt("currentlevel", levelnums);
		SceneManager.LoadScene("Thirdscene");
		hideadapBanner();
		hideRecBanner();
	}

		public void lvlcomp()
    {      
        //LvlcmpInstruction.SetActive(true);
        lvlcmpEffect1.SetActive(true);
        lvlcmpEffect2.SetActive(true);
        levelfinishaudio.PlayOneShot(finish, 3.0F);
		Invoke("levelcomplete", 2.0f);	
    } 

	public void TMplaybackbtn()
    {
		TrafficManagerPlayNotification.SetActive(false);
		//showRecBanner();
	}
    public void levelcomplete()
	{
		
		if (PlayerPrefs.GetInt("Unlocklevels") == 2)
		{
			TrafficManagerPlayNotification.SetActive(true);
			//hideRecBanner();
		}
		gameplaypanel[0].SetActive(true);
		showRecBanner();
		//showBanner();
		showAdapbanner();
		CustomAnalytics.eventMessage("SampleScene" + (levelselectionnumber + 1) + "_complete");
        isnotwin = true;
		
		if (Menu.Levelnumbersunlock == PlayerPrefs.GetInt("Unlocklevels"))
		{
			if (PlayerPrefs.GetInt("Unlocklevels") < Levels.Length - 1)
			{
				PlayerPrefs.SetInt("Unlocklevels", PlayerPrefs.GetInt("Unlocklevels") + 1);
			}
		}

		pausedad = false;
		AudioListener.volume = 0;
		PlayerPrefs.SetInt("Tick"+Menu.Levelnumbersunlock.ToString(),1);
		PlayerPrefs.SetInt ("Rateus",PlayerPrefs.GetInt("Rateus")+1);
		//if (PlayerPrefs.GetInt ("oncetimeshow") == 2) 
		//{
			if (PlayerPrefs.GetInt ("Rateus")==2 ||PlayerPrefs.GetInt ("Rateus")==6||PlayerPrefs.GetInt ("Rateus")==9||PlayerPrefs.GetInt ("Rateus")==12||PlayerPrefs.GetInt ("Rateus")==15)
			{
				rateuspanel.SetActive (true);
				//showBanner();
				showAdapbanner();
                showRecBanner();
            }
		//}
       
        
        PlayerPrefs.SetInt ("Cashscore",PlayerPrefs.GetInt("Cashscore")+levelwisescroe[levelselectionnumber-1]);//work on coin text
		totalscore.text=PlayerPrefs.GetInt("Cashscore").ToString();
		Time.timeScale = 0;
//		if (Application.internetReachability != NetworkReachability.NotReachable) 
//		{
//			//Change the Text
//			Debug.Log ("Internet Available");
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			adobj.ShowInterstitial ();
//			#endif
//
//		} 
	}
	public void levelFailed()
	{
      
        AudioListener.volume = 0;
		if (!isfail) 
		{
			iswin = true;
			Invoke ("levelfaileddelay",2.5f);
		}
        CustomAnalytics.eventMessage("SampleScene" + (levelselectionnumber + 1) + "_failed");
    }
	public void levelfaileddelay()
	{
		pausedad = false;
		showRecBanner();
		//showRecBanner();
		//showBanner();
		showAdapbanner();
		Time.timeScale = 0;
		gameplaypanel [1].SetActive (true);
	}

	public void skip()
	{
		Debug.Log("SKIPPPPPPPPP");
		Trainannouncementaudio.mute = true;
		Skipsbtn.SetActive (false);
		Canvasnew.SetActive(true);		
		CivilainMovement.civiliancount = 0;
		foreach (GameObject Civilian in GameObject.FindGameObjectsWithTag("Civi"))
		{
			Destroy (Civilian.gameObject);
		}
		if (Menu.Levelnumberspass == 1)
		{
			indictorspeed(true);
		}
		FindObjectOfType<Trainmanagers> ().dooropenclose1 (1);
		CivilainMovement.civiliancount = 0;
		levels.transform.GetChild(levelselectionnumber - 1).transform.gameObject.SetActive(true);
		//thirdpersoncont.GetComponent<vThirdPersonInput>().enabled = true;
		//instructionpanel.SetActive (true);	
	}

	public void skipins()
	{
		//Skipsbtn2.SetActive (false);
		instructionpanel.SetActive (false);
		//thirdpersoncont.GetComponent<vThirdPersonInput> ().enabled = true;
		canvasb.SetActive (true);
	}

	public void skipins2()
	{
		//Skipsbtn2.SetActive (false);

		instructionpanellevel.SetActive (false);

	}

	public void levelcomthird()
	{
		fadeinfadeout.SetActive (true);
		Canvasnew.SetActive (false);
		//thirdpersoncame.transform.rotation = Quaternion.Euler(0,0,0);

		//Invoke ("DelayForthirdperson",2.0f);
		//instructionpanellevel.SetActive (true);
	}

	/*void DelayForthirdperson()
	{
		thirdpersoncont.SetActive (true);
		thirdpersoncame.SetActive (true);
		thirdpersoncont.transform.position=Thirdpersonpos[levelselectionnumber].gameObject.transform.position;
		thirdpersoncont.transform.rotation=Thirdpersonpos[levelselectionnumber].gameObject.transform.rotation;
		thirdpersoncont.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		thirdpersoncont.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		thirdpersoncont.GetComponent<vThirdPersonInput> ().enabled = true;
		thirdpersoncont.GetComponent<Animator> ().enabled = true; 
		canvasb.SetActive (true);
		fadeinfadeout.SetActive (false);
		traincame.SetActive (false);
		Skipsbtn.SetActive (false);
	}*/

	void OnDisable()
	{

		if (IsInvoking ("Fadepanalonoffnew"))
			CancelInvoke ("Fadepanalonoffnew");
		if (IsInvoking ("Fadepanalonoff"))
			CancelInvoke ("Fadepanalonoff");
		if (IsInvoking ("DelayForthirdperson"))
			CancelInvoke ("DelayForthirdperson");
		if (IsInvoking ("levelfaileddelay"))
			CancelInvoke ("levelfaileddelay");

	}
	public void Fadepanalonoff()
	{
		fadeinfadeout.SetActive (false);
	}
	public void scorecollect ()
	{
		PlayerPrefs.SetInt ("Cashscore", PlayerPrefs.GetInt ("Cashscore") + 100);
		scoretext.text = PlayerPrefs.GetInt ("Cashscore").ToString ();
	}
	public void fadeinnfadeout (GameObject Colliderget,GameObject Colliderget2,GameObject Colliderget3)
	{
		fadeinfadeout.SetActive (true);
		Thirdpersoncolliderin = Thirdpersonoff (Colliderget.gameObject,Colliderget2,Colliderget3);
		StartCoroutine(Thirdpersoncolliderin);

		print ("Ho");
		Invoke ("Fadepanalonoff",2f);
	}
	IEnumerator Thirdpersonoff(GameObject Colliobjext,GameObject Colliobjext2,GameObject Colliobjext3)
	{
		ticketrecevid.SetActive (true);
		yield return new WaitForSeconds(1.5f);
		Colliobjext3.transform.rotation=Thirdpersoneyes[levelselectionnumber-1].gameObject.transform.rotation;
		Colliobjext3.transform.position=Thirdpersoneyes[levelselectionnumber-1].gameObject.transform.position;
		vThirdPersonCamera.instance.Init ();
		ticketrecevid.SetActive (false);
		Colliobjext.SetActive (false);
		Colliobjext2.SetActive (true);
		Colliobjext3.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		Colliobjext3.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		Colliobjext3.GetComponent<vThirdPersonInput> ().enabled = true;
		Colliobjext3.GetComponent<Animator> ().enabled = true;

	}
	public void fadeinnfadein (GameObject Collidergets)
	{
		fadeinfadeout.SetActive (true);
		Thirdpersoncollideroff = Thirdpersonin (Collidergets);
		StartCoroutine(Thirdpersoncollideroff);
		Invoke ("Fadepanalonoff",1f);
	}
	public void fadeinnfadeininn (GameObject Collidergets)
	{
		fadeinfadeout.SetActive (true);
		Invoke ("Fadepanalonoffnew",2f);
	}
	public void Fadepanalonoffnew()
	{
		fadeinfadeout.SetActive (false);
		//thirdpersoncont.SetActive (false);
		if (!iswin) 
		{
			isfail = true;
			levelcomplete ();
		}

	}
	IEnumerator Thirdpersonin(GameObject Colliobjext)
	{
		yield return new WaitForSeconds(1);
		Colliobjext.SetActive (false);
		//thirdpersoncont.SetActive (false);
		//thirdpersoncame.SetActive (false);
		traincame.SetActive (true);
		canvasb.SetActive (false);
		Canvasnew.SetActive (true);
		//indictorspeed(true);
	}
	public void thirdcollideron()
	{
		Thirdcollider [levelselectionnumber-1].gameObject.SetActive(true);
	}
    void showInterstitial()
    {
		if (ads)
			ads.ShowInterstitial();
    }
	void showBanner()
	{
		//if (ads)
			//ads.ShowBannerFunc();
	}
	void showAdapbanner()
	{
		if (adapAds)
			adapAds.ShowBanner();
	}
    
	void hideadapBanner()
       {
		if (adapAds)
			adapAds.HideBanner();
       }
	//void hideBanner()
	//{
	//	if (ads)
	//		ads.HideBannerFunc();
	//}
	void showRecBanner()
	{
		if (recAds)
			recAds.ShowBanner();

	}
	void hideRecBanner()
	{
		if (recAds)
			recAds.HideBanner();

	}
	void ShowMenuRectAds()
	{
		if (recAds)
			recAds.RequestBanner();
		//recAds.ShowBanner();
	}
	void ShowMenubannerAds()
	{
		if (ads)
			ads.RequestBanner();
		// ads.ShowBannerFunc();
	}

}
