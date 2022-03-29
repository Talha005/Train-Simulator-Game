using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Controllers;

public class levelscript : MonoBehaviour {

    public GameObject[] Train;
    public GameObject[] TrainIndicator;
    public trainController[] TrainEngines;
    public float[] spawningTime;
    public GameObject[] Trainplusone;
    float time, speed = 1f;
   
    //public GameObject[] TrainStop;

    void Start()
    {
        for (int i = 0; i < Train.Length; i++)
        {

            Train[i].SetActive(false);
        }
    }

    public void spawnTrain()
    {     
        StartCoroutine(spawn());
    }
 

    IEnumerator spawn()
    {     
        for (int i = 0; i < Train.Length; i++)
        {
            yield return new WaitForSeconds(spawningTime[i]);
            Train[i].SetActive(true);
            TrainIndicator[i].SetActive(true);
        }   
    }

    public void DeactivateAllTrain()
    {

        for (int i = 0; i < Train.Length; i++)
        {                      
            Train[i].SetActive(false);         
        }
    }

    public void TrainPlusOnefunc(GameObject train)
    {
        
        for (int i = 0; i < TrainEngines.Length; i++)
        {
            if (train == TrainEngines[i].gameObject)
            {
                Trainplusone[i].SetActive(true);
               
                break;
            }

        }
        
    }


}
