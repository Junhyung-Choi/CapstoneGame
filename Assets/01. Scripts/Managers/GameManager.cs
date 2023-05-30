using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    
    /// <summary>
    /// InstructionManager
    /// InstructionCanvas 관리하는 매니저
    /// </summary>
    InstructionManager instructionManager;
    /// <summary>
    /// ActionManager
    /// 동작을 관리하는 매니저
    /// </summary>
    ActionManager actionManager;
    
    public List<ChunkType> chunks = new List<ChunkType>();
    public List<GameObject> obstacles = new List<GameObject>();

    public int currentChunkIndex; 
    int maxObstacleSpawnNum = 10, curSpawnedObstacleNum = 0;

    [SerializeField]
    int maxRep = 1, curRep = 0;

    float obstacleTimer = 0f, obstacleSpawnTime = 1f;

    bool isWaitChunk = true;
    float ChunkWaitTimer = 0f, ChunkWaitMaxTime = 5f;

    bool isStartWaitTimerEnded, isInitNoticeFaded = false;
    float startWaitTimer = 5f;

    public GameObject nearObs;
    float farDistance = 40f, nearDistance = 15f;

    float playTimer = 0f;
    bool isPlayStart = false;

    public static bool isPaused = false;
    float pauseTimer = 0f, pauseMaxTime = 5f;

    Coroutine coroutine;
    Vector3 camStartPos;

    //------------------------------------------------------------------------------------------------------------------

    private void Awake() {
        if(instance == null) { instance = this; }
        else if(instance != this) { Destroy(this.gameObject); }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentChunkIndex = 0;
        actionManager = this.GetComponent<ActionManager>();
        actionManager.ChangeAction(chunks[currentChunkIndex]);

        SetMaxObstacle();
        maxRep = InitMaxRep();

        camStartPos = Camera.main.transform.position;

        instructionManager = GameObject.Find("Instruction Canvas").GetComponent<InstructionManager>();

        isWaitChunk = false;
    }

    void Update()
    {
        HandlePause();
        if(isPaused) { return; }

        if(!isInitNoticeFaded) { WaitInitNoticeFaded(); }
        else if(!isStartWaitTimerEnded) { WaitStartWaitTimerEnded(); }

        if(isPlayStart) { SetPlayTimer(); }

        SpawnObstacle();

        HandleObstacle();

        SetAction();
    }

    //---------------------------------------정지 관련 코드들-----------------------------------------------------------
    void HandlePause()
    {
        if(!isPaused)
        {
            float leftValue = 0f;
            leftValue = RPInputManager.inputMatrix[0, 0] + RPInputManager.inputMatrix[1, 0];
            
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;
                instructionManager.SetPauseActive(true);
                Time.timeScale = 0f;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
                instructionManager.SetPauseActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    //------------------------------------------시작 관련 코드들--------------------------------------------------------

    void SetPlayTimer()
    {
        if(!isPaused)
        {
            playTimer += Time.unscaledDeltaTime;
            instructionManager.SetPlayTimer(playTimer);
        }
    }

    void WaitInitNoticeFaded()
    {
        if(instructionManager.isInitNoticeFaded) { 
            isInitNoticeFaded = true;
            instructionManager.SetStartWaitTimer(true);
        }
    }

    IEnumerator WaitStartTextCoroutine()
    {
        yield return new WaitForSeconds(1f);
        instructionManager.AcitvateStartMessage();
        isPlayStart = true;
    }

    void WaitStartWaitTimerEnded()
    {
        startWaitTimer -= Time.deltaTime;
        instructionManager.SetStartWaitTimerText(startWaitTimer);
        if(startWaitTimer <= 0f)
        {
            isStartWaitTimerEnded = true;
            StartCoroutine(WaitStartTextCoroutine());
        }
    }
    
    //------------------------------------------스폰 관련 코드들--------------------------------------------------------

    void SpawnObstacle()
    {
        // 다음 청크로 넘어가기 전에 대기
        if(isWaitChunk) 
        {
            ChunkWaitTimer += Time.deltaTime;
            if(ChunkWaitTimer >= ChunkWaitMaxTime)
            {
                ChunkWaitTimer = 0f;
                isWaitChunk = false;
                NextChunk();
            }
            return;
        }

        obstacleTimer += Time.deltaTime;
        if(obstacleTimer >= obstacleSpawnTime)
        {
            obstacleTimer = 0f;
            obstacleSpawnTime = GetObstacleSpawnTime();
            GameObject obs = ChunkManager.instance.SpawnObstacle(chunks[currentChunkIndex]);
            obs.name = chunks[currentChunkIndex].ToString() + curSpawnedObstacleNum.ToString();
            obstacles.Add(obs);
            curSpawnedObstacleNum += 1;
            if(curSpawnedObstacleNum >= maxObstacleSpawnNum) { isWaitChunk = true; }

            if(chunks[currentChunkIndex] == ChunkType.START )
            {
                obs.GetComponent<Obstacle>().isStartorEnd = true;
                NextChunk();
                actionManager.ChangeAction(chunks[currentChunkIndex]);
            }

            if(chunks[currentChunkIndex] == ChunkType.END)
            {
                obs.GetComponent<Obstacle>().isStartorEnd = true;
                // 끝날때 정지시키는 코드.
            }
        }
    }

    float GetTimeScale(float dist)
    {
        float timeScale = 1f;

        if(dist < nearDistance) { timeScale = 0f; }
        else if(dist < farDistance)
        { 
            timeScale = (dist - nearDistance) / (farDistance - nearDistance);
        }
        return timeScale;
    }
    
    //------------------------------------------스폰 관련 코드들--------------------------------------------------------
    
    void HandleObstacle()
    {
        if(obstacles.Count == 0) return;

        nearObs = obstacles[0];
        while(nearObs == null)
        {
            if(obstacles.Count == 0) return;
            obstacles.RemoveAt(0);
            nearObs = obstacles[0];
        }

        float dist = Vector3.Distance(nearObs.transform.position, transform.position);

        if(dist < farDistance && !nearObs.GetComponent<Obstacle>().isActionDid)
        {
            if(!nearObs.GetComponent<Obstacle>().isStartorEnd)
            {
                float timeScale = GetTimeScale(dist);
                TimeHandler.instance.SetTimeFactor(timeScale);
            }
        }
        else { TimeHandler.instance.SetTimeFactor(1f); }
    }


    public void DoRep()
    {
        float dist = Vector3.Distance(nearObs.transform.position, transform.position);

        if(dist > farDistance)
        {
            RPInputManager.instance.ShowNotice("Too Far");
            Debug.Log("Too Far");
            return;
        }

        if(nearObs.GetComponent<Obstacle>().isActionDid)
        {
            RPInputManager.instance.ShowNotice("Already Did");
            Debug.Log("Already Did");
            return;
        }

        PlayRepEffect();

        nearObs.GetComponent<Obstacle>().isActionDid = true;
        curRep += 1;
    }

    public void PlayRepEffect()
    {
        Debug.Log("ActionManager.curAction" + actionManager.curAction);
        switch(actionManager.curAction)
        {
            case ChunkType.WALK:
                if(coroutine != null) { StopCoroutine(coroutine); }
                coroutine = StartCoroutine(PlayWalkEffect());
                break;
            case ChunkType.STEPUP:
                if(coroutine != null) { StopCoroutine(coroutine); }
                coroutine = StartCoroutine(PlayWalkEffect());
                break;
            case ChunkType.SQUAT:
                if(coroutine != null) { StopCoroutine(coroutine); }
                coroutine = StartCoroutine(PlayWalkEffect());
                break;
            case ChunkType.PLANK:
                break;
            case ChunkType.CLIMB:
                if(coroutine != null) { StopCoroutine(coroutine); }
                coroutine = StartCoroutine(PlayClimbEffect());
                break;
            default:
                break;
        }
    }

    IEnumerator PlayWalkEffect()
    {
        Vector3 height = new Vector3(0,2,0);
        float time = 0f;
        while(Camera.main.transform.position.y < camStartPos.y + 2f)
        {
            time += Time.deltaTime;
            Camera.main.transform.position = Vector3.Lerp(camStartPos, camStartPos + height, time * 2);
            yield return null;
        }
        
        time = 0f;
        while(Camera.main.transform.position.y > camStartPos.y)
        {
            time += Time.deltaTime;
            Camera.main.transform.position = Vector3.Lerp(camStartPos + height, camStartPos, time * 2);
            yield return null;
        }
    }

    IEnumerator PlayClimbEffect()
    {
        float time = 0f;
        Vector3 height = new Vector3(0,-1,0);
        Quaternion startDegree = Quaternion.Euler(0, 90, 0);
        Quaternion rotDegree = Quaternion.Euler(90, 90, 0);
        while(Camera.main.transform.eulerAngles.x < 90f)
        {
            time += Time.deltaTime;
            Camera.main.transform.rotation = Quaternion.Lerp(startDegree, rotDegree, time);
            Camera.main.transform.position = Vector3.Lerp(camStartPos, camStartPos + height, time * 3);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        
        time = 0f;
        while(Camera.main.transform.eulerAngles.x > 0f)
        {
            time += Time.deltaTime;
            Camera.main.transform.rotation = Quaternion.Lerp(rotDegree, startDegree, time);
            Camera.main.transform.position = Vector3.Lerp(camStartPos + height, camStartPos, time * 2);
            yield return null;
        }
    }




    void NextChunk()
    {
        currentChunkIndex += 1;
        curSpawnedObstacleNum = 0;
        SetMaxObstacle();
        // actionManager.ChangeAction(chunks[currentChunkIndex]);
    }

    //------------------------------------------------------------------------------------------------------------------

    void SetAction()
    {
        if(curRep >= maxRep)
        {
            actionManager.ChangeAction(chunks[currentChunkIndex]);
            curRep = 0;
            SetMaxRep();
        }
    }


    //------------------------------------------------------------------------------------------------------------------

    float GetObstacleSpawnTime()
    {
        switch(this.chunks[currentChunkIndex])
        {
            case ChunkType.STEPUP:
                return Random.Range(1f,2f);
            default:
                return Random.Range(3f,5f);
        }
    }

    void SetMaxObstacle()
    {
        if(chunks[currentChunkIndex] == ChunkType.WALK) { maxObstacleSpawnNum = 7;}
        else if(chunks[currentChunkIndex] == ChunkType.STEPUP) { maxObstacleSpawnNum = 7;}
        else if(chunks[currentChunkIndex] == ChunkType.CLIMB) { maxObstacleSpawnNum = 5;}
        else if(chunks[currentChunkIndex] == ChunkType.PLANK) { maxObstacleSpawnNum = 5;}
        else if(chunks[currentChunkIndex] == ChunkType.SQUAT) { maxObstacleSpawnNum = 5;}
    }

    void SetMaxRep()
    {
        if(nearObs == null) { return; } 
        if( obstacles[1].GetComponent<Obstacle>().chunkType == ChunkType.WALK) { maxRep = 7;}
        else if( obstacles[1].GetComponent<Obstacle>().chunkType == ChunkType.STEPUP) { maxRep = 7;}
        else if( obstacles[1].GetComponent<Obstacle>().chunkType == ChunkType.CLIMB) { maxRep = 5;}
        else if( obstacles[1].GetComponent<Obstacle>().chunkType == ChunkType.PLANK) { maxRep = 5;}
        else if( obstacles[1].GetComponent<Obstacle>().chunkType == ChunkType.SQUAT) { maxRep = 5;}
    }

    int InitMaxRep()
    {
        if(chunks[1] == ChunkType.WALK) { return 7;}
        else if(chunks[1] == ChunkType.STEPUP) { return 7;}
        else if(chunks[1] == ChunkType.CLIMB) { return 5;}
        else if(chunks[1] == ChunkType.PLANK) { return 5;}
        else if(chunks[1] == ChunkType.SQUAT) { return 5;}
        else { return 0; }
    }
}

public enum ChunkType
{
    // 시작 청크
    START,
    // 걷는거
    WALK,
    // 내려갔다 올라갔다 하면서 걷는거
    STEPUP,
    // 위로 올라가는거
    CLIMB,
    // 팔만 위에 두고 하는 엎드려 뻗쳐 자세
    PLANK,
    // 스쾃.
    SQUAT,
    // 끝 청크
    END
}