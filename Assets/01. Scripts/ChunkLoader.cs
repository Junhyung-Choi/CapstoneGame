using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public GameObject[] chunks;
    public int initalSpawnCount = 5;
    public float destoryZone = 300;

    [Space(10)]
    public Vector3 moveDirection = new Vector3(-1,0,0);
    public float movingSpeed = 1;

    public float chunkSize = 60;
    GameObject lastChunk;

    private void Awake() {
        if(initalSpawnCount <= chunks.Length)
            initalSpawnCount = chunks.Length;
        
        int chunkIndex = 0;
        for (int i = 0; i < initalSpawnCount; i++)
        {
            GameObject chunk = (GameObject)Instantiate(chunks[chunkIndex]);
            chunk.SetActive(true);

            chunk.GetComponent<Chunk>().loader = this;

            chunk.transform.localPosition = new Vector3(-i * chunkSize,0, transform.position.z);
            moveDirection = new Vector3(1,0,0);

            lastChunk = chunk;

            chunkIndex += 1;
            if(chunkIndex >= chunks.Length)
                chunkIndex = 0;
        }
    }

    // 삭제 : 마지막 청크 전으로 옮기기
    public void DestroyChunk(Chunk thisChunk)
    {
        Vector3 newPos = lastChunk.transform.position;
        newPos.x -= chunkSize;

        lastChunk = thisChunk.gameObject;
        lastChunk.transform.position = newPos;
    }
}
