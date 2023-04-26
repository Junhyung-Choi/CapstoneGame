using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ActionManager actionIdentifier;
    // Start is called before the first frame update
    void Start()
    {
        actionIdentifier = GameObject.Find("RP").GetComponent<ActionManager>();
        actionIdentifier.ChangeAction(EAction.WALK);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionIdentifier.isActionDid)
        {
            MoveSettingScene();
        }
    }

    public void MoveSettingScene()
    {
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Setting");
    }
}
