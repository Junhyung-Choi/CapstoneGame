using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ChunkLoader loader;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(loader.moveDirection * loader.movingSpeed * Time.deltaTime);
    }

    void DestroyChunk(Obstacle now)
    {
        Destroy(now.gameObject);
    }

    void FixedUpdate()
    {
        if(transform.position.x < loader.destoryZone)
            DestroyChunk(this);
    }

}
