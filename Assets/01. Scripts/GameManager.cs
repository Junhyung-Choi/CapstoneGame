using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<ActionType> actions = new List<ActionType>();
    public ActionType action = new ActionType();

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

    public enum ActionType
    {
        // 가만히 있는거
        IDLE,
        // 걷는거
        WALK,
        // 내려갔다 올라갔다 하면서 걷는거
        STEPUP,
        // 위로 올라가는거
        CLIMB,
        // 팔만 위에 두고 하는 엎드려 뻗쳐 자세
        PLANK,
        // 스쾃.
        SQAUT,
        // 시작 청크
        START,
        // 끝 청크
        END
    }
}
