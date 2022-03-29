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

public class GamemanagerStation : MonoBehaviour
{
    // Use this for initialization
    //public GameObject levelcomplelpanel;
    public GameObject[] gameplaypanel;
    public GameObject[] mapSpawn;
    private int levelselectionnumber;
    public GameObject[] ControlPanels;
    public GameObject[] GoPanel;
    public GameObject Nextbtn;
    public GameObject lvlcmpEffect1,lvlcmpEffect2;
    private float timesteps = 1.0f;
    private bool isnotwin;
    private int cameraswitch = 1;  
    public Text leveltext;
    public Text scoretext,scoretext1;    
    public GameObject pausedpanel;
    public levelscript[] Levels;
    private GoogleMobileAdsDemoScript ads;
    RectangleBannerAd_aa recAds;
    //private bool adoncetime;
    public GameObject rateuspanel;
    private bool IsAlreadyPaused = false;
    public GameObject StartStageCount;
    public levelscript currentLevel;
    public AudioSource Musicaudio;
    public AudioSource levelfinishaudio;
    public AudioClip finish;
    public int levelno;
    public GameObject RaceModeNoticePlay;
    float Speed;
    AdaptiveBanner adapAds;

    void Start()
    {

        Time.timeScale = 1f;
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
        SetEnviornment();
        levelselectionnumber = Menu.Levelnumberstraffic; 
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
        CustomAnalytics.eventMessage("Thirdscene" + (levelselectionnumber + 1) + "start");
    }

        public void SetEnviornment()
    {
        for (int i = 0; i < Levels.Length; i++)
        {

            Levels[i].gameObject.SetActive(false);
            ControlPanels[i].SetActive(false);
            GoPanel[i].SetActive(false);
            mapSpawn[i].SetActive(false);
        }
        Debug.Log("SetenvironmentLevelStart=" +PlayerPrefs.GetInt("currentlevel"));
        levelno =PlayerPrefs.GetInt("currentlevel");
        Levels[levelno].gameObject.SetActive(true);
        ControlPanels[levelno].SetActive(true);
        GoPanel[levelno].SetActive(true);
        mapSpawn[levelno].SetActive(true);
        currentLevel = Levels[levelno];
        leveltext.text = (PlayerPrefs.GetInt("currentlevel") + 1).ToString();
        StartStageCount.SetActive(true);
        Invoke("startLevel", 4f);
        traincount = currentLevel.Train.Length;
        TrainCountText();
       
    }

    void startLevel()
    {
        currentLevel.spawnTrain();

    }

    void LateUpdate()
    {
        if (IsAlreadyPaused)
            Time.timeScale = 0;
    }

 
    public void GameplayController(string Gameplaybtn)
    {

        switch (Gameplaybtn)
        {
            case "Home":
                Handheld.StartActivityIndicator();
                Time.timeScale = 1;
                //hideBanner();
                hideadapBanner();
                hideRecBanner();
                showInterstitial();
                gameplaypanel[3].SetActive(true);  //for loading
                IsAlreadyPaused = false;
                PlayerPrefs.SetInt("IsNext", 0);
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
            case "Carpanels":
               // carpanel.SetActive(false);
                //carbutton.SetActive(true);
                break;
            case "Restart":
                Time.timeScale = 1;
                hideRecBanner();
                //hideBanner();
                hideadapBanner();
                AudioListener.volume = 1;
                Handheld.StartActivityIndicator();
                IsAlreadyPaused = false;
                gameplaypanel[3].SetActive(true);  
                SceneManager.LoadScene(4);
                break;       
            case "Resume":
                if(increase)
                {
                    Time.timeScale = 2f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                hideBanner();
                hideadapBanner();
                hideRecBanner();
                gameplaypanel[2].SetActive(false);   //for unpause
                AudioListener.volume = 1;
                IsAlreadyPaused = false;

                if (PlayerPrefs.GetInt("Musiconff") == 0)
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
                //Time.timeScale = 1;
                
                if (increase)
                {
                    Time.timeScale = 2f;

                }
                else
                {
                    Time.timeScale = 1f;

                }
                AudioListener.volume = 1;
                pausedpanel.SetActive(false);
                IsAlreadyPaused = false;
                if (PlayerPrefs.GetInt("Musiconff") == 0)
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
            case "Rateus":
                PlayerPrefs.SetInt("oncetimeshow", 1);
                Application.OpenURL("market://details?id=com.bettergames.euro.train.driver.sim");
                rateuspanel.SetActive(false);
                break;
            case "Laternow":
                rateuspanel.SetActive(false);
                break;
            case "Paused":
                //showBanner();
                showAdapbanner();
                showRecBanner();
                Time.timeScale = 0;
                //adoncetime = true;
                AudioListener.volume = 0;
                IsAlreadyPaused = true;
                PlayerPrefs.SetInt("pausecounter", PlayerPrefs.GetInt("pausecounter") + 1);
                gameplaypanel[2].SetActive(true);  //for pause
              //mainaudiosource.mute = true;
                break;
            case "Pausedcar":
                Time.timeScale = 0;
                AudioListener.volume = 0;
                //adoncetime = true;
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
                Time.timeScale = 1f;
                AudioListener.volume = 1;
                hideRecBanner();
                //hideBanner();
                hideadapBanner();
                showInterstitial();
                gameplaypanel[3].SetActive(true);  //for loading
                Debug.Log("OnNExt Levelstart" + PlayerPrefs.GetInt("currentlevel"));
                PlayerPrefs.SetInt("currentlevel", PlayerPrefs.GetInt("currentlevel")+1);
                SceneManager.LoadScene(4);              
                break;
            case "Resumetut":
                if (increase)
                {
                    Time.timeScale = 2f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                break;
        }
    }

    void OnApplicationPause(bool isPause)
    {
        if (isPause)
        { // App going to background
            Time.timeScale = 0;
        }
        else
        {
            if (IsAlreadyPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1f;
        }
    }
    public void RMfunc(int levelnums)
    {
        RaceModeNoticePlay.SetActive(false);
        gameplaypanel[3].SetActive(true);
        Menu.LevelnumbersRace = levelnums;
        PlayerPrefs.SetInt("currentlevel", levelnums);
        SceneManager.LoadScene("TrainRacescene");
        AudioListener.volume = 1;
        //hideBanner();
        hideadapBanner();
        hideRecBanner();
    }
    public void lvlcomp()
        {
        Invoke("levelcomplete", 2.0f);
        }
    public void TMplaybackbtn()
    {
        RaceModeNoticePlay.SetActive(false);
       // showRecBanner();
    }

    public void levelcomplete()
   {
        levelno++;
        gameplaypanel[0].SetActive(true);
        showRecBanner();
        showAdapbanner();
        if ((PlayerPrefs.GetInt("currentlevel")) == Levels.Length -1)
        {
           Nextbtn.SetActive(false);
        }
        CustomAnalytics.eventMessage("Thirdscene" + (levelselectionnumber + 1) + "_complete");
        Debug.Log("LEVELUNLOCKTM FIrst =" + PlayerPrefs.GetInt("UnlockTrafficlevels"));
        //if (levelselectionnumber == PlayerPrefs.GetInt("UnlockTrafficlevels
        if (levelno >PlayerPrefs.GetInt("UnlockTrafficlevels"))
           {
            Debug.Log("Levelno =" + levelno);
            if (PlayerPrefs.GetInt("UnlockTrafficlevels") < 13)
             {
                Debug.Log("LEVELUNLOCKcompleteBefore =" + PlayerPrefs.GetInt("UnlockTrafficlevels"));
                PlayerPrefs.SetInt("UnlockTrafficlevels", PlayerPrefs.GetInt("UnlockTrafficlevels")+1);
                Debug.Log("LEVELUNLOCKcompleteAfter =" + PlayerPrefs.GetInt("UnlockTrafficlevels"));
             }
           }
           if (PlayerPrefs.GetInt("UnlockTrafficlevels") == 5)
           {
             RaceModeNoticePlay.SetActive(true);
             //hideRecBanner();
           }
            AudioListener.volume = 0;
            //PlayerPrefs.SetInt("Tick" + Menu.Levelnumbertrafficunlock.ToString(), 1);
            PlayerPrefs.SetInt("Cashscore", PlayerPrefs.GetInt("Cashscore") + 500);//work on coin text    
            scoretext.text = "500";        
            scoretext1.text = PlayerPrefs.GetInt("Cashscore").ToString();    
            PlayerPrefs.SetInt("Rateus",PlayerPrefs.GetInt("Rateus")+1);
        // if (PlayerPrefs.GetInt("oncetimeshow") == 4)
        //{
        //if (PlayerPrefs.GetInt("Rateus") == 4 || PlayerPrefs.GetInt("Rateus") == 9 || PlayerPrefs.GetInt("Rateus") == 11)
        //{
        //    rateuspanel.SetActive(true);
        //}
        // }
            if (PlayerPrefs.GetInt("currentlevel") == 4 || PlayerPrefs.GetInt("currentlevel") == 9 || PlayerPrefs.GetInt("currentlevel") == 11 )
            {
               rateuspanel.SetActive(true);
            }
            Time.timeScale = 1f;
           // PlayerPrefs.SetInt("currentlevel", PlayerPrefs.GetInt("currentlevel") + 1);
            //Debug.Log("LEVELUNLOCKCurrent =" + (PlayerPrefs.GetInt("currentlevel")));
    }

        public void levelFailed()
        {     
        Time.timeScale = 1f;
  
         AudioListener.volume = 0;
        
        for (int i = 0; i < currentLevel.TrainEngines.Length; i++)
        {
            currentLevel.TrainEngines[i].StopTrain();
            //Explosion.SetActive(true);
           
        }
        //adoncetime = false;
        Invoke("levelfaileddelay", 1.5f);
        CustomAnalytics.eventMessage("Thirdscene" + (levelselectionnumber + 1) + "_fail");
    }

        public void levelfaileddelay()
        {
        gameplaypanel[1].SetActive(true);
        //showBanner();
        showAdapbanner();
        showRecBanner();
        
        currentLevel.DeactivateAllTrain();
        
        }

    public  int traincount;
    public  int counter = 0 ;
    public Text Traincounttxt;
    public Text Traincountertxt;
    public GameObject TrainPlusOne;
    public void CheckTrainCounter(SplineController currentspline)
    {
        if (currentspline.Position >= 0.98f)
        {
            
            currentspline.GetComponent<trainController>().DeactivateTrain();
            counter++;
            TrainCountText();
            currentLevel.TrainPlusOnefunc(currentspline.gameObject);
           
            if (counter == traincount)
            {
                         
                lvlcmpEffect1.SetActive(true);
                lvlcmpEffect2.SetActive(true);
                levelfinishaudio.PlayOneShot(finish, 0.7F);
                Invoke("lvlcomp", 2.0f);
               
            }
            
        }
        
    }

    void TrainCountText()
    {
        Traincounttxt.text = traincount.ToString();
        Traincountertxt.text = counter.ToString();
    }

    bool increase;
    public GameObject tutorialslidertrigger;
    public void increaseSpeed()
    {
        increase = !increase;
        if (increase)
        {
            Time.timeScale = 2f;
            tutorialslidertrigger.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
           
        }
    }
    
    void showInterstitial()
    {
        if (ads)
            ads.ShowInterstitial();
    }
    void showBanner()
    {
        //ads.ShowBannerFunc();
    }
    void hideBanner()
    {
        //ads.HideBannerFunc();
    }
    void showRecBanner()
    {
        recAds.ShowBanner();
    }
    void hideRecBanner()
    {
        recAds.HideBanner();
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


