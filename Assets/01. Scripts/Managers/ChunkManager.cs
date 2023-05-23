using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    public GameObject walkObstacle, squatObstacle, stepUpObstacle, plankObstacle, climbObstacle;
    
    private void Awake() {
        instance = this;
    }
    
    public void SpawnObstacle(ChunkType type)
    {
        switch(type)
        {
            case ChunkType.WALK:
                Instantiate(walkObstacle, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case ChunkType.SQUAT:
                Instantiate(squatObstacle, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case ChunkType.STEPUP:
                Instantiate(stepUpObstacle, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case ChunkType.PLANK:
                Instantiate(plankObstacle, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case ChunkType.CLIMB:
                Instantiate(climbObstacle, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            default:
                throw new System.Exception("Invalid ChunkType");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
