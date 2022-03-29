using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTrainRace : MonoBehaviour 
{
    public levelData LD;
    void start()
    {
        LD = FindObjectOfType<levelData>();
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.CompareTag("AiTrainGo1"))
    //    {
    //        LD.barriertrigger[0].SetActive(false);
    //    }
    //    if (col.CompareTag("AiTrainGo2"))
    //    {
    //        LD.barriertrigger[1].SetActive(false);
    //    }
    //    if (col.CompareTag("AiTrainGo3"))
    //    {
    //        LD.barriertrigger[2].SetActive(false);
    //    }
    //}
}
