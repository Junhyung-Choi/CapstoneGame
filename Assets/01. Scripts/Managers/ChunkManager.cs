using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    public GameObject walkObstacle, stepUpObstacle, squatObstacle, plankObstacle, climbObstacle;
    
    public GameObject startObstacle, endObstacle;

    private void Awake() {
        instance = this;
    }
    
    public void SpawnObstacle(ChunkType type)
    {
        GameObject tmp;
        switch(type)
        {
            case ChunkType.WALK:
                tmp = (GameObject)Instantiate(walkObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
                break;
            case ChunkType.SQUAT:
                tmp = (GameObject)Instantiate(squatObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
                break;
            case ChunkType.STEPUP:
                tmp = (GameObject)Instantiate(stepUpObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
                break;
            case ChunkType.PLANK:
                tmp = (GameObject)Instantiate(plankObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
                break;
            case ChunkType.CLIMB:
                tmp = (GameObject)Instantiate(climbObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
                break;
            case ChunkType.START:
                tmp = (GameObject)Instantiate(startObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
                break;
            case ChunkType.END:
                tmp = (GameObject)Instantiate(endObstacle, new Vector3(200, 0, 0), Quaternion.identity);
                tmp.GetComponent<Obstacle>().loader = this.GetComponent<ChunkLoader>();
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
