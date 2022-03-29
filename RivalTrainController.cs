using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;

public class RivalTrainController : MonoBehaviour
{
   public TrainData train;
   public GamemanagerRace gmr;
    void start()
    {
        gmr = FindObjectOfType<GamemanagerRace>();  
    }

    void OnTriggerEnter(Collider col)
    {    
        if (col.CompareTag("detect"))
        {
            train.targetspeed = 0f;
            col.gameObject.SetActive(false);
        }

        if (col.CompareTag("RivalSpeed"))
        {
            print("RivalSpeedIncrease");
            train.targetspeed = 30f;
            col.gameObject.SetActive(false);
            Invoke("RivalGo", 10f);
        }
        if (col.CompareTag("Lose"))
        {
            Debug.Log("Lose");
            train.targetspeed = 0f;
            gmr.currentPlayerController.GetComponent<TrainData>().TrainSpeed = 0;
            Raceover();        
            //train.targetspeed = 0f;                            
        }
        if (col.CompareTag("RivalSlow"))
        {
            
            train.targetspeed = 15f;
            col.gameObject.SetActive(false);
           
        }
    }

    //IEnumerator RivalStop()
    //{
    //    yield return new WaitForSeconds(5.0f);
    //    RivalGo();
    //}

    public void RivalGo()
    {
       
        //train.targetspeed = train.TrainSpeed;
        train.targetspeed = 28;
    }

     private void Raceover()
    {
        gmr.RaceLost();
    }

    public void rivalTrainSpeedStop()
    {
        train.targetspeed = 0f;
    }
}
