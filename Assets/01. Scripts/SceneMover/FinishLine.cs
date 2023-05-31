using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : SceneMover
{
    
    void Update()
    {
        if (transform.position.z < -10)
        {
            PlayerPrefs.SetFloat("PlayTime",GameManager.instance.playTimer); 
        }
    }
}