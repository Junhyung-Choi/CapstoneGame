using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public GameObject instructionCanvas;

    ActionManager actionManager;
    public List<ChunkType> chunks = new List<ChunkType>();
    List<GameObject> obstacles = new List<GameObject>();

    public int currentChunkIndex; 
    int maxRep = 10, curRep = 0;

    float obstacleTimer = 0f, obstacleMaxTime = 1f;
    bool isObstacleSpawn = false;

    float obstacleSpawnTimer = 0f, obstacleSpawnMaxTime = 5f;

    bool isStartWaitTimerEnded, isInitNoticeFaded = false;
    float startWaitTimer = 5f;
    Text startWaitTimerText;

    public GameObject nearObs;
    float farDistance = 30f, nearDistance = 5f;

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

        startWaitTimerText = instructionCanvas.transform.Find("StartTimer").GetComponent<Text>();

        isObstacleSpawn = true;
    }

    void Update()
    {
        if(!isInitNoticeFaded) { WaitInitNoticeFaded(); }
        else if(!isStartWaitTimerEnded) { WaitStartText(); }

        SpawnObstacle();

        CheckObstacleNear();

        // if(actionIdentifier.isActionDid)
        // {
        //     MoveSettingScene();
        // }
    }

    void CheckObstacleNear()
    {
        if(obstacles.Count == 0) return;

        nearObs = obstacles[0];
        if(nearObs == null)
        {
            obstacles.RemoveAt(0);
            return;
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

    void WaitInitNoticeFaded()
    {
        Fade fade = instructionCanvas.transform.Find("InitNotice").GetComponent<Fade>();
        if(fade.isFadeOut) { 
            isInitNoticeFaded = true;
            startWaitTimerText.gameObject.SetActive(true);
        }
    }

    IEnumerator WaitStartTextCoroutine()
    {
        yield return new WaitForSeconds(1f);
        startWaitTimerText.gameObject.SetActive(false);
        instructionCanvas.transform.Find("START").gameObject.SetActive(true);
    }

    void WaitStartText()
    {
        startWaitTimer -= Time.deltaTime;
        startWaitTimerText.text = Mathf.Ceil(startWaitTimer).ToString();
        if(startWaitTimer <= 0f)
        {
            isStartWaitTimerEnded = true;
            StartCoroutine(WaitStartTextCoroutine());
        }
    }

    public void DoRep()
    {
        nearObs.GetComponent<Obstacle>().isActionDid = true;
    }

    void NextChunk()
    {
        currentChunkIndex += 1;
        curRep = 0;
        SetMaxRep();
        actionManager.ChangeAction(chunks[currentChunkIndex]);
    }

    void SpawnObstacle()
    {
        if(!isObstacleSpawn) 
        {
            obstacleSpawnTimer += Time.deltaTime;
            if(obstacleSpawnTimer >= obstacleSpawnMaxTime)
            {
                obstacleSpawnTimer = 0f;
                isObstacleSpawn = true;
                NextChunk();
            }
            return;
        }

        obstacleTimer += Time.deltaTime;
        if(obstacleTimer >= obstacleMaxTime)
        {
            obstacleTimer = 0f;
            obstacleMaxTime = Random.Range(1f, 3f);
            GameObject obs = ChunkManager.instance.SpawnObstacle(chunks[currentChunkIndex]);
            obstacles.Add(obs);
            curRep += 1;
            if(curRep >= maxRep) { isObstacleSpawn = false; }

            if(chunks[currentChunkIndex] == ChunkType.START)
            {
                obs.GetComponent<Obstacle>().isStartorEnd = true;
                NextChunk();
            }
        }
    }

    void SetMaxRep()
    {
        if(chunks[currentChunkIndex] == ChunkType.WALK) { maxRep = 10; }
        else if(chunks[currentChunkIndex] == ChunkType.STEPUP) { maxRep = 10; }
        else if(chunks[currentChunkIndex] == ChunkType.CLIMB) { maxRep = 10; }
        else if(chunks[currentChunkIndex] == ChunkType.PLANK) { maxRep = 10; }
        else if(chunks[currentChunkIndex] == ChunkType.SQUAT) { maxRep = 10; }
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