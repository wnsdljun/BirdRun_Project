using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject ground;
    public Transform lastObstacle;

    private int groundCnt = 0;
    private bool isObstacle = false;

    void SpawnGround()
    {
        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);
        GameObject newGround = Instantiate(ground, newPosition, Quaternion.identity);
        lastObstacle = newGround.transform;

        groundCnt++;

        isObstacle = false;
    }
    void SpawnObstacle()
    {
        if (groundCnt < 2) return;

        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);

        int randomIdx = Random.Range(0, obstacles.Length);
        GameObject newObstacle = Instantiate(obstacles[randomIdx], newPosition, Quaternion.identity);
        lastObstacle = newObstacle.transform;

        groundCnt = 0;

        isObstacle = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            //if (!isObstacle && Random.value > 0.7f) // 30% 확률로 장애물 생성
            if (!isObstacle)
            {
                SpawnObstacle();
            }
            else
            {
                SpawnGround();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
