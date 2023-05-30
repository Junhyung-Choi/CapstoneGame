using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealStartSceneManager : MonoBehaviour
{
    GameObject start, loading;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.Find("Start").gameObject;
        loading = GameObject.Find("Loading");

        start.SetActive(false);
        loading.SetActive(loading);
    
    }

    // Update is called once per frame
    void Update()
    {
        if(RPInputManager.instance.isTimeOutEnded)
        {
            start.SetActive(true);
            loading.SetActive(false);
        }
    }
}
