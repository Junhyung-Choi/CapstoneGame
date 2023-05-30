using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static float deadZone = -5f;
    public ChunkLoader loader;

    public bool isActionDid = false;
    public bool isStartorEnd = false;

    public ChunkType chunkType = ChunkType.WALK;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(loader.moveDirection * loader.movingSpeed * Time.deltaTime);
    }

    void DestroyObs(Obstacle now)
    {
        Destroy(now.gameObject);
    }

    void FixedUpdate()
    {
        if(transform.position.x < deadZone)
            DestroyObs(this);
    }

}
