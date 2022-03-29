using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Invector.CharacterController;
using WSMGameStudio.RailroadSystem;
using UnityStandardAssets.CrossPlatformInput;
using FluffyUnderware.Curvy.Controllers;
//using FluffyUnderware.Curvy.Examples;
public class GamemanagerRace : MonoBehaviour 
{
	// Use this for initialization
    public GameObject[] trainsimplayer;
	public GameObject[] EngineMat;
	public GameObject[] BuggieMat1;
	public GameObject[] BuggieMat2;
	public GameObject[] BuggieMat3;
	public GameObject[] BuggieMat4;
	public GameObject Levelcompletepanel, Nextbtn;
	public GameObject Rival;
    public GameObject Gmplaybtn,fadeinfadeoutpanel;
    public GameObject StartStageCount;
    public GameObject traincamera;
	public GameObject[] Starttcolliderdetect;
	public Slider SliderControl;
	public GameObject Sliderhandle;
	public Text textSliderValue;
	private TrainController_v3 ___Tv;
	private DemoUI_v3 ___Dmi;
	public GameObject[] Miniindicator;
    public GameObject[] gameplaypanel;
    private int levelselectionnumber;
	private float currenttime;
	private float timesteps=1.0f;
	private  bool isnotwin;
	private int cameraswitch = 1;
	public GameObject cameraaon;
	public GameObject inputtouchcampanel;
	public Text leveltext;
	public Text scoretext,scoretext3;
	public GameObject[] backcolliders;
	bool forraceslidernew;
	public GameObject traincanvas;
	public GameObject envirom;
	public GameObject pausedpanel;
    public GameObject[] trainmodels;
    public levelData[] Levels;
	public GameObject[] ControlPanels;
	public GameObject lvlcmpEffect1, lvlcmpEffect2;
	public AudioSource levelfinishaudio;
	private GoogleMobileAdsDemoScript ads;
    RectangleBannerAd_aa recAds;
    private bool adoncetime;
	public GameObject rateuspanel;
	private bool IsAlreadyPaused=false;
    public int levelno;
    public levelData currentLevel;
	public GameObject busypanel, proceedpanel;
	public float Delaytime;
	public GameObject Rivaldetect, Stagedetect, BoosterDetect;
	public TrainData train, currentPlayerController, aiController;
	public GameObject Greenright, rightanim, Greenleft, leftanim, leftbtnmat, rightbtnmat;
	public Leftrightbuttontrain Trainbutton;
	public List<SplineController> Buggies,Buggies2, Buggies3, Buggies4, buggiesFrontAxel, buggiesFrontAxel2, buggiesFrontAxel3, buggiesFrontAxel4, buggiesBackAxel, buggiesBackAxel2, buggiesBackAxel3, buggiesBackAxel4;
	public GameObject leftrightimageonff;
	public AudioSource HornAudio;
	public AudioSource WheelAudio;
	public AudioSource EngineAudio;
	public AudioSource BrakesAudio;
	public AudioSource CrashAudio;
	int currentTrain;
	public AudioSource Musicaudio;
	public GameObject SpeedEffectimg, boastbtn,BoasterImagefill, BoasterImagedrain;
	public GameObject tutorialSlider, tutorialBoost;
	AdaptiveBanner adapAds;
	public GameObject RedSignal;
	public GameObject GreenSignal;

	void Start ()
	{	
		boastbtn.GetComponent<Button>().interactable = false;
		Sliderhandle.SetActive(false);
		gameplaypanel[3].SetActive(true);
		Invoke("Loading", 2f);
		Time.timeScale = 1f;
		CustomAnalytics.eventMessage("TrainRacescene" + (levelselectionnumber + 1) + "start");
		ads = FindObjectOfType<GoogleMobileAdsDemoScript>();
		if (ads)
        {
            ads.MGrLink();
        }
        recAds = FindObjectOfType<RectangleBannerAd_aa>();
        if (recAds)
        {
            recAds.MGrLink();
        }
		adapAds = FindObjectOfType<AdaptiveBanner>();
		if (adapAds)
		{
			adapAds.MGLink();
		}
		Handheld.StopActivityIndicator ();
		IsAlreadyPaused=false;
		AudioListener.volume = 1;
		trainsimplayer [PlayerPrefs.GetInt("Trainnum")].SetActive (true);
		//trainmodels [PlayerPrefs.GetInt ("Trainnum")].SetActive (true);
		
		SetEnviornment();
		leveltext.text = (PlayerPrefs.GetInt("currentlevel") + 1).ToString();	
		indictorspeedRace(true);
		if (leftrightimageonff == null)
			return;
		if (PlayerPrefs.GetInt("Musiconoff") == 0)
		{
			//Musicaudio.volume = 1f;
			Musicaudio.mute = false;
		}
		if (PlayerPrefs.GetInt("Musiconoff") == 1)
		{
			//Musicaudio.volume = 0f;
			Musicaudio.mute = true;
		}	
	}

    public void SetEnviornment()
    {
		
        for (int i = 0; i < Levels.Length; i++)
        {
         Levels[i].gameObject.SetActive(false);
			ControlPanels[i].SetActive(false);
		}
        levelno = PlayerPrefs.GetInt("currentlevel");
        Levels[levelno].gameObject.SetActive(true);
        currentLevel = Levels[levelno];
		currentTrain = PlayerPrefs.GetInt("Trainnum");
		trainsimplayer[currentTrain].SetActive(true);
		ControlPanels[levelno].SetActive(true);
		Invoke("RivalSpawn", 2f);
		train = trainsimplayer[currentTrain].GetComponent<TrainData>();
		currentPlayerController = trainsimplayer[currentTrain].GetComponent<TrainData>();
		currentPlayerController.SetplayerSpline(currentLevel.playerSpline, Levels[levelno].GetComponent<levelData>());
		aiController = Rival.GetComponent<TrainData>();
		aiController.SetRivalSpline(currentLevel.RivalSpline, Levels[levelno].GetComponent<levelData>());		
		//aiController.SetSpline(currentLevel.RivalSpline);
		StartStageCount.SetActive(true);		
		Invoke("tutorialsliderfunc", 4f);
	    EngineMat [currentTrain].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + currentTrain + "texture"));
		BuggieMat1[currentTrain].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + currentTrain + "texture"));
		BuggieMat2[currentTrain].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + currentTrain + "texture"));
		BuggieMat3[currentTrain].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + currentTrain + "texture"));
		BuggieMat4[currentTrain].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + currentTrain + "texture"));
	}

	public void tutorialsliderfunc()
    {
		Time.timeScale = 0;
		currentPlayerController.letsGo = true;
		aiController.letsGo = true;
		Sliderhandle.SetActive(true);
		tutorialSlider.SetActive(true);
	}
	 public void Loading()
    {
		gameplaypanel[3].SetActive(false);
    }
	public void RivalSpawn()
    {
		
		Rival.SetActive(true);
	}
    void LateUpdate()
    {
		textSliderValue.text = ((int)(currentPlayerController.currentSpeed) * 4).ToString();
		if (brake)
		{
			train.targetspeed = Mathf.Lerp(train.targetspeed, 0, 1f * Time.deltaTime);
			SliderControl.value= train.targetspeed;
		}

		if (IsAlreadyPaused)
		{ 
			Time.timeScale = 0;
		}

        if (SliderControl.value > 0)
        {
            bool moving = false;
            if (moving == false)
            {				
				moving = true;
            }
            else
            {				
				moving = false;
            }			
        }
    }

	public void Fadeinfadeout()
	{
		fadeinfadeoutpanel.SetActive (false);
	}
	//public Slider SliderControl;
	
	public void TrainSpeedControl()
    {
		currentPlayerController.targetspeed = SliderControl.value;
		Time.timeScale = 1;	
		if(SliderControl.value == 0)
        {
			EngineAudio.Play();
			WheelAudio.Stop();
        }
		else if(SliderControl.value == 30)
        {
			EngineAudio.Stop();
			WheelAudio.Play();
		}
	}


	bool brake;
	public void BrakeBtn(bool value)
	{
		brake = value;	
	}


	public void TrainBoost()
    {
		train.targetspeed = 45f;
		
		BoasterImagedrain.SetActive(true);
		BoasterImagefill.SetActive(false);
		SpeedEffectimg.SetActive(true);
		boastbtn.GetComponent<Button>().interactable = false;
		Invoke("TrainGo", 6f);
	}
	public void TrainGo()
    {
		train.targetspeed = 30;
		SpeedEffectimg.SetActive(false);	
	}

	public void boastbtnACtive()
    {
		BoasterImagefill.SetActive(true);
		boastbtn.GetComponent<Button>().interactable = true;
	}
	public void boastbtnOff()
	{
		BoasterImagefill.SetActive(false);
		BoasterImagedrain.SetActive(false);
		boastbtn.GetComponent<Button>().interactable = false;
	}


	public void TrainsStop()
    {
		train.targetspeed = 0;	
    }
	public void cameraswitchnew()
	{
		if (cameraswitch == 0)
        {
			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			FindObjectOfType<TrainManagerRace> ().resetcame ();
			inputtouchcampanel.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().distance = 25f;
			cameraaon.GetComponent<SmoothFollow> ().height = 8f;
			cameraswitch = 1;
		}

        else if (cameraswitch == 1)
        {
			cameraaon.GetComponent<SmoothFollow> ().enabled = false;
			cameraaon.GetComponent<Came> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = true;
			FindObjectOfType<TrainManagerRace>().resetcame ();
			inputtouchcampanel.SetActive (true);
			cameraswitch = 2;
		}

        else if (cameraswitch == 2)
        {
			cameraswitch = 0;
			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			inputtouchcampanel.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().distance = 0.1f;
			cameraaon.GetComponent<SmoothFollow> ().height = 0.0f;
			
			FindObjectOfType<TrainManagerRace> ().frontcam ();
		}
	}

	public void GameplayController(string Gameplaybtn)
	{
		switch(Gameplaybtn)
		{
		case "Home": 
			Handheld.StartActivityIndicator ();
			Time.timeScale = 1;
           // hideBanner();
            hideRecBanner();
				hideadapBanner();
            showInterstitial();
			gameplaypanel [3].SetActive (true);  //for loading
			IsAlreadyPaused =false;
			PlayerPrefs.SetInt ("IsNext", 0);
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			if(adoncetime)
//			{
//			  adobj.ShowInterstitial ();
//			}
//			  
//			}
//			#endif
			SceneManager.LoadScene(1);
			break;
		case "Restart": 
			Time.timeScale = 1;
				hideRecBanner();
				hideadapBanner();
           // hideBanner();
			AudioListener.volume = 1;
		    Handheld.StartActivityIndicator ();
			IsAlreadyPaused =false;
			gameplaypanel [3].SetActive (true);  //for loading
			//			if (MenuManager.Careerstring == "Carrermode") 
			//			{
			//				CustomAnalytics.eventMessage ("Restart_Career"+levelselectionnumber);
			//			}
			SceneManager.LoadScene(5);
			break; 
		case "Resume":
           // hideBanner();
           hideRecBanner();
				hideadapBanner();
				Time.timeScale = 1;           
			gameplaypanel [2].SetActive (false);   //for unpause
			AudioListener.volume = 1;
			IsAlreadyPaused =false;

			if (PlayerPrefs.GetInt ("Musiconff") == 0) 
			{

				//mainaudiosource.mute = false;

			}
                else 
			{
				//mainaudiosource.mute = true;
			}
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			adobj.HideBannerFunc1();
//			adobj.HideBannerFunc();
//			}
//			#endif
			break;
		case "Resumecar":
           // hideBanner();
            hideRecBanner();
				hideadapBanner();
				Time.timeScale = 1;
			AudioListener.volume = 1;
			pausedpanel.SetActive (false);
			IsAlreadyPaused =false;
			if (PlayerPrefs.GetInt ("Musiconff") == 0) 
			{

				//mainaudiosource.mute = false;

			} else 
			{
				//mainaudiosource.mute = true;
			}
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			adobj.HideBannerFunc1();
//			adobj.HideBannerFunc();
//			}
//			#endif
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
           // showBanner();
				showAdapbanner();
            showRecBanner();
			Time.timeScale = 0;
			adoncetime = true;
			AudioListener.volume = 0;
			IsAlreadyPaused = true;          
			PlayerPrefs.SetInt ("pausecounter", PlayerPrefs.GetInt ("pausecounter") + 1);
			gameplaypanel [2].SetActive (true);  //for pause
			//mainaudiosource.mute = true;
			break;
		case "Pausedcar":
           // showBanner();
				showAdapbanner();
				showRecBanner();
            Time.timeScale = 0;
			AudioListener.volume = 0;
			adoncetime = true;
			IsAlreadyPaused = true;
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			adobj.ShowBannerFunc1 ();
//			adobj.ShowBannerFunc();
//			}
//			#endif
			pausedpanel.SetActive(true);
			break;
		case "Next":
			
		    //levelcomplete();
		    Time.timeScale = 1;
			AudioListener.volume = 1;
			hideRecBanner();
			hideadapBanner();
			//hideBanner();
			showInterstitial();
			gameplaypanel[3].SetActive(true);	
			PlayerPrefs.SetInt("currentlevel", PlayerPrefs.GetInt("currentlevel") + 1);
		    SceneManager.LoadScene(5);
			//PlayerPrefs.SetInt ("IsNext", 4);            
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

	public void lvlcomp()
	{        
        Invoke ("levelcomplete",1.0f);
		
	}
	public void levelcomplete()
	{
		levelno++;
		//LevelAudio.PlayOneShot(finish, 0.7F);	
		levelfinishaudio.Play();
		Levelcompletepanel.SetActive(true);
		//showBanner();
		showAdapbanner();
		showRecBanner();
		if ((PlayerPrefs.GetInt("currentlevel")) == Levels.Length - 1)
        {
			Nextbtn.SetActive(false);
        }
        CustomAnalytics.eventMessage("TrainRacescene" + (levelselectionnumber + 1) + "_complete");
		
		//if (Menu.LevelnumberRaceunlock == PlayerPrefs.GetInt("UnlockRacelevels"))
		if (levelno > PlayerPrefs.GetInt("UnlockRacelevels"))
		{
			Debug.Log("LEVELUNLOCKno =" + levelno);
			if (PlayerPrefs.GetInt("UnlockRacelevels") < 10)
			{
				Debug.Log("LEVELUNLOCKcompleteBefore =" + PlayerPrefs.GetInt("UnlockRacelevels"));
				PlayerPrefs.SetInt("UnlockRacelevels", PlayerPrefs.GetInt("UnlockRacelevels")+1);
				Debug.Log("LEVELUNLOCKcompleteafter =" + PlayerPrefs.GetInt("UnlockRacelevels"));
			}
		}
		//PlayerPrefs.SetInt("UnlockRacelevels", PlayerPrefs.GetInt("UnlockRacelevels") + 1 );        //PLUS 1 FOR UNLOCK LEVELS
		AudioListener.volume = 0;
		adoncetime = false;
		PlayerPrefs.SetInt("Cashscore", PlayerPrefs.GetInt("Cashscore") + 500);
		scoretext.text = "500";
		scoretext3.text = PlayerPrefs.GetInt ("Cashscore").ToString ();
		PlayerPrefs.SetInt ("Rateus",PlayerPrefs.GetInt("Rateus")+1);
		//if (PlayerPrefs.GetInt ("oncetimeshow") == 2) 
		//{
			if (PlayerPrefs.GetInt("currentlevel")== 2 || PlayerPrefs.GetInt("currentlevel") == 6 || PlayerPrefs.GetInt("currentlevel") == 9)
			{
				rateuspanel.SetActive (true);
			}       
        //}

		Time.timeScale = 0;
		//PlayerPrefs.SetInt("currentlevel", PlayerPrefs.GetInt("currentlevel") + 1);
	}
	public void levelFailed()
	{  
        
        AudioListener.volume = 0;
		Crash();
		adoncetime = false;
		train.targetspeed = 0f;
		Invoke ("levelfaileddelay",2f);
        CustomAnalytics.eventMessage("TrainRacescene" + (levelselectionnumber + 1) + "_fail");
    }

	public void levelfaileddelay()
	{	
		
		gameplaypanel [1].SetActive (true);
		showRecBanner();
		showAdapbanner();
		//showBanner();
	}

	public void RaceLost()
    {
		currentLevel.RivalWin();
		AudioListener.volume = 0;
		adoncetime = false;
		gameplaypanel[1].SetActive(true);
		CustomAnalytics.eventMessage("TrainRacescene" + (levelselectionnumber + 1) + "_Racefail");
	}
	public void Honk(bool hornonoff)
	{
		if (hornonoff)
		{
			Honkfunc(true);
		}
		else
		{
			Honkfunc(false);
		}
	}

	
	public void Honkfunc(bool hornonff)
	{
		if (hornonff)
		{

			HornAudio.Play();
			
		}
		else
		{
			HornAudio.Stop();
		}

	}

	public void Crash()
	{
		CrashAudio.Play();

	}
	void OnDisable()
	{

		if (IsInvoking("levelfaileddelay"))
			CancelInvoke("levelfaileddelay");
		if (IsInvoking("levelcomplete"))
			CancelInvoke("levelcomplete");
		if (IsInvoking("Fadeinfadeout"))
			CancelInvoke("Fadeinfadeout");
		if (IsInvoking("Thirdpersonoff1"))
			CancelInvoke("Thirdpersonoff1");
		if (IsInvoking("Thirdpersonoff"))
			CancelInvoke("Thirdpersonoff");
	}

	public void scorecollect2()
	{
		//PlayerPrefs.SetInt("Cashscore", PlayerPrefs.GetInt("Cashscore") + 100);
		//scoretext.text = PlayerPrefs.GetInt("Cashscore").ToString();
	}

	public void indictorspeedRace(bool speedtutonff)
	{
		if (speedtutonff == false)
		{
			//Tutpanel1.SetActive(false);
		}
		else
		{
			//Tutpanel1.SetActive(true);
			forraceslidernew = false;
		}
	}
	public void RivalWin()
    {
		
		Stagedetect.SetActive(false);
	}

	public void PlayerWin()
	{
		
		Rivaldetect.SetActive(false);
		//Rival.SetActive(false);
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
	void hideBanner()
	{
		//if (ads)
			//ads.HideBannerFunc();
	}
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
}

