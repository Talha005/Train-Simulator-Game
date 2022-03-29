using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WSMGameStudio.RailroadSystem;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;

public class TrainManagerRace : MonoBehaviour
{

    // Use this for initialization
    public GamemanagerRace gmr;
    public TrainData TD;
    public levelData levelsignal;
    public AiSpwan Spawn;
    public GameObject camepos;
    public GameObject Startpos, frontpos;
    public GameObject interioronff;
    public GameObject Explosion;
    public GameObject BoosterOn, BoosterOff;
   // public AudioSource Impactexplosion;
    public GameObject Rival, ProceedPanel;
    bool change;
    void Start()
    {
        Invoke("startt", 1f);

    }
    void startt()
    {
        gmr = FindObjectOfType<GamemanagerRace>();
        levelsignal = gmr.Levels[gmr.levelno].GetComponent<levelData>();
        Spawn = FindObjectOfType<AiSpwan>();

    }

    public void levelfail()
    {
        camepos.transform.Rotate(new Vector3(0, -125, 0));
        gmr.levelFailed();
    }

    public void frontcam()
    {
        camepos.transform.position = frontpos.transform.position;
        camepos.transform.rotation = frontpos.transform.rotation;
        interioronff.SetActive(true);
    }
    public void resetcame()
    {
        camepos.transform.position = Startpos.transform.position;
        camepos.transform.rotation = Startpos.transform.rotation;
        interioronff.SetActive(false);
    }

    //public void camerarotate(bool camerarota)
    //{
    //    if (camerarota)
    //    {
    //        Campos.transform.Rotate(new Vector3(0, 180, 0));
    //        Campos.transform.localPosition = new Vector3(0.0f, 2.09f, -4.56f);
    //        //Cameralerp=-90;
    //    }
    //    else
    //    {
    //        Campos.transform.Rotate(new Vector3(0, -180, 0));
    //        Campos.transform.localPosition = new Vector3(0.0f, 2.21f, 1.59f);
    //        //Cameralerp=90;
    //    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SignalStop"))
        {
           
            levelsignal.SignalControl();
        }

        if (col.CompareTag("Win"))
        {
                   
            gmr.lvlcomp();
            gmr.levelfinishaudio.Play();
            gmr.lvlcmpEffect1.SetActive(true);
            gmr.lvlcmpEffect2.SetActive(true);     
            gmr.PlayerWin();
            gmr.trainsimplayer[0].GetComponent<TrainData>().TrainSpeed = 0;
            gmr.trainsimplayer[1].GetComponent<TrainData>().TrainSpeed = 0;
            gmr.trainsimplayer[2].GetComponent<TrainData>().TrainSpeed = 0;
            gmr.trainsimplayer[3].GetComponent<TrainData>().TrainSpeed = 0;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            gmr.Rival.GetComponent<TrainData>().TrainSpeed = 0;
        }

        //Debug.Log(col.gameObject.name);
        if (col.CompareTag("JunctionCollide"))

        {
            if (col.GetComponent<JunctionController>().usejunction)
            {
                if (!change)
                {
                    change = true;
                    JunctionOn();
                }
            }
            else
            {
                if (change)
                {
                    change = false;
                    JunctionOff();
                }
            }
        }

        if (col.CompareTag("TrackChangeRight"))
        {
            Debug.Log(col.gameObject.name);
            //gmr.Greenright.SetActive(true);
            gmr.rightanim.SetActive(true);
        }

        if (col.CompareTag("TrackChangeLeft"))
        {
            //gmr.Greenleft.SetActive(true);
            gmr.leftanim.SetActive(true);
        }

        {
            if (col.CompareTag("BoasterON"))
            {
                gmr.boastbtnACtive();
            }
        }

        {
            if (col.CompareTag("BoasterOff"))
            {
                gmr.boastbtnOff();
            }
        }
        {
            if (col.CompareTag("TuTCollider"))
            {
                gmr.tutorialBoost.SetActive(true);
            }
        }
        //if (col.CompareTag("SignalStop"))
        //{
        //    Debug.Log("AITRIGGER");
        //    for (int i = 0; i < Spawn.SpawnAi.Length; i++)
        //     {
        //      Spawn.SpawnAi[i].SetActive(true);
        //     }
        //}

    }

    private void OnTriggerExit(Collider other)
    {
        Invoke("Indicatoronoff", 8.0f);
       
        //if (leftrightimageonff != null)
        //    leftrightimageonff.SetActive(false);
    }
 
    private void Indicatoronoff()
    {
        gmr.Greenleft.SetActive(false);
        gmr.Greenright.SetActive(false);
        gmr.rightbtnmat.GetComponent<Image>().color = Color.white;
        gmr.leftbtnmat.GetComponent<Image>().color = Color.white;
    }

    public void JunctionOn()
    {
        for (int i = 0; i < gmr.Buggies.Count; i++)
        {
           
            gmr.Buggies[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesFrontAxel[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies[i].allowDirectionChange = true;
            gmr.buggiesFrontAxel[i].allowDirectionChange = true;
            gmr.buggiesBackAxel[i].allowDirectionChange = true;
            gmr.Buggies[i].rejectCurrentSpline = true;
            gmr.buggiesFrontAxel[i].rejectCurrentSpline = true;
            gmr.buggiesBackAxel[i].rejectCurrentSpline = true;
        }
        for (int i = 0; i < gmr.Buggies2.Count; i++)
        {

            gmr.Buggies2[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesFrontAxel2[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel2[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies2[i].allowDirectionChange = true;
            gmr.buggiesFrontAxel2[i].allowDirectionChange = true;
            gmr.buggiesBackAxel2[i].allowDirectionChange = true;
            gmr.Buggies2[i].rejectCurrentSpline = true;
            gmr.buggiesFrontAxel2[i].rejectCurrentSpline = true;
            gmr.buggiesBackAxel2[i].rejectCurrentSpline = true;
        }
        for (int i = 0; i < gmr.Buggies3.Count; i++)
        {

            gmr.Buggies3[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesFrontAxel3[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel3[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies3[i].allowDirectionChange = true;
            gmr.buggiesFrontAxel3[i].allowDirectionChange = true;
            gmr.buggiesBackAxel3[i].allowDirectionChange = true;
            gmr.Buggies3[i].rejectCurrentSpline = true;
            gmr.buggiesFrontAxel3[i].rejectCurrentSpline = true;
            gmr.buggiesBackAxel3[i].rejectCurrentSpline = true;
        }
        for (int i = 0; i < gmr.Buggies4.Count; i++)
        {
            gmr.Buggies4[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesFrontAxel4[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel4[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies4[i].allowDirectionChange = true;
            gmr.buggiesFrontAxel4[i].allowDirectionChange = true;
            gmr.buggiesBackAxel4[i].allowDirectionChange = true;
            gmr.Buggies4[i].rejectCurrentSpline = true;
            gmr.buggiesFrontAxel4[i].rejectCurrentSpline = true;
            gmr.buggiesBackAxel4[i].rejectCurrentSpline = true;
        }
    }

    public void JunctionOff()
    {
        for (int i = 0; i < gmr.Buggies.Count; i++)
        {
           
            gmr.Buggies[i].connectionBehavior = SplineControllerConnectionBehavior.CurrentSpline;
            gmr.buggiesFrontAxel[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies[i].allowDirectionChange = false;
            gmr.buggiesFrontAxel[i].allowDirectionChange = false;
            gmr.buggiesBackAxel[i].allowDirectionChange = false;
            gmr.Buggies[i].rejectCurrentSpline = false;
            gmr.buggiesFrontAxel[i].rejectCurrentSpline = false;
            gmr.buggiesBackAxel[i].rejectCurrentSpline = false;
        }
        for (int i = 0; i < gmr.Buggies2.Count; i++)
        {

            gmr.Buggies2[i].connectionBehavior = SplineControllerConnectionBehavior.CurrentSpline;
            gmr.buggiesFrontAxel2[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel2[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies2[i].allowDirectionChange = false;
            gmr.buggiesFrontAxel2[i].allowDirectionChange = false;
            gmr.buggiesBackAxel2[i].allowDirectionChange = false;
            gmr.Buggies2[i].rejectCurrentSpline = false;
            gmr.buggiesFrontAxel2[i].rejectCurrentSpline = false;
            gmr.buggiesBackAxel2[i].rejectCurrentSpline = false;
        }
        for (int i = 0; i < gmr.Buggies3.Count; i++)
        {

            gmr.Buggies3[i].connectionBehavior = SplineControllerConnectionBehavior.CurrentSpline;
            gmr.buggiesFrontAxel3[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel3[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies3[i].allowDirectionChange = false;
            gmr.buggiesFrontAxel3[i].allowDirectionChange = false;
            gmr.buggiesBackAxel3[i].allowDirectionChange = false;
            gmr.Buggies3[i].rejectCurrentSpline = false;
            gmr.buggiesFrontAxel3[i].rejectCurrentSpline = false;
            gmr.buggiesBackAxel3[i].rejectCurrentSpline = false;
        }
        for (int i = 0; i < gmr.Buggies4.Count; i++)
        {

            gmr.Buggies4[i].connectionBehavior = SplineControllerConnectionBehavior.CurrentSpline;
            gmr.buggiesFrontAxel4[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.buggiesBackAxel4[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            gmr.Buggies4[i].allowDirectionChange = false;
            gmr.buggiesFrontAxel4[i].allowDirectionChange = false;
            gmr.buggiesBackAxel4[i].allowDirectionChange = false;
            gmr.Buggies4[i].rejectCurrentSpline = false;
            gmr.buggiesFrontAxel4[i].rejectCurrentSpline = false;
            gmr.buggiesBackAxel4[i].rejectCurrentSpline = false;
        }
    }
    public void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Crash")
        {
            Debug.Log("SOUNDCOLLISION");                       
            gmr.Rival.GetComponent<TrainData>().TrainSpeed = 0;
            TD.targetspeed = 0f;
            TD.currentSpeed = 0f;      
            Explosion.SetActive(true);                 
            gmr.levelFailed();          
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
        if (col.gameObject.tag == "BoosterOn")
        {
            Debug.Log("Booster");
            BoosterOn.SetActive(true);
        }
    }
}
