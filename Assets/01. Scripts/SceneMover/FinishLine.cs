using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : SceneMover
{
    
    void Update()
    {
        if (transform.position.x < 1f)
        {
            PlayerPrefs.SetFloat("PlayTime",GameManager.instance.playTimer); 
            MoveToRanking();
        }
    }
}