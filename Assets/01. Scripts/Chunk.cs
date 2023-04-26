using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public ChunkLoader loader;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(loader.moveDirection * loader.movingSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if(transform.position.x < loader.destoryZone)
            loader.DestroyChunk(this);
    }
}
