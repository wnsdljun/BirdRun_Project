using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesList;  //지형 목록
    public GameObject itemObstacle;     //아이템 지형

    public GameObject ground;
    public Transform lastObstacle;

    [HideInInspector] public ItemSpawner itemSpawner;

    private int groundCnt = 0;
    [HideInInspector] public bool isObstacle = false;
    [HideInInspector] public bool isSpawner = true;

    void Update()
    {
    }

    //바닥 생성
    public void SpawnGround()
    {
        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);
        GameObject newGround = Instantiate(ground, newPosition, Quaternion.identity);
        newGround.transform.SetParent(transform);
        lastObstacle = newGround.transform;

        groundCnt++;

        isObstacle = false;
        isSpawner = true;
    }

    //장애물 생성
    public void SpawnObstacle()
    {
        //장애물이 연속으로 생성되지 않도록 조건 처리
        if (groundCnt < 2)
        {
            isSpawner = false;
            return;
        }

        GameObject newObstacle;

        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);

        //10% 확률로 아이템 지형 생성
        if (Random.value < 0.1f)
            newObstacle = Instantiate(itemObstacle, newPosition, Quaternion.identity);
        else
        {
            int randomIdx = Random.Range(0, obstaclesList.Length);
            newObstacle = Instantiate(obstaclesList[randomIdx], newPosition, Quaternion.identity);
        }

        newObstacle.transform.SetParent(transform);
        lastObstacle = newObstacle.transform;

        groundCnt = 0;

        isObstacle = true;
        isSpawner = true;
    }
}
