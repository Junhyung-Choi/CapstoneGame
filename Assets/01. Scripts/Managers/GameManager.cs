using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ActionManager actionManager;
    public List<ChunkType> chunks = new List<ChunkType>();

    int currentChunkIndex; 
    int currentChunkMaxRep = 1, currentChunkMaxSet = 1;
    int currentChunkRep = 0, currentChunkSet = 1;

    float obstacleTimer = 0f, obstacleMaxTime = 1f;

    bool isObstacleSpawn = false;


    // Start is called before the first frame update
    void Start()
    {
        currentChunkIndex = 0;
        actionManager = this.GetComponent<ActionManager>();
        actionManager.ChangeAction(chunks[currentChunkIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObstacle();

        // if(actionIdentifier.isActionDid)
        // {
        //     MoveSettingScene();
        // }
    }

    public void DoRep()
    {
        currentChunkRep += 1;
        if(currentChunkRep >= currentChunkMaxRep)
        {
            currentChunkRep = 0;
            currentChunkSet += 1;
            if(currentChunkSet > currentChunkMaxSet)
            {
                currentChunkSet = 1;
                NextChunk();
            }
        }
    }

    void NextChunk()
    {
        currentChunkIndex += 1;
        actionManager.ChangeAction(chunks[currentChunkIndex]);
    }

    void SpawnObstacle()
    {
        if(!isObstacleSpawn) return;

        obstacleTimer += Time.deltaTime;
        if(obstacleTimer >= obstacleMaxTime)
        {
            obstacleTimer = 0f;
            obstacleMaxTime = Random.Range(1f, 3f);
            ChunkManager.instance.SpawnObstacle(chunks[currentChunkIndex]);
        }
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