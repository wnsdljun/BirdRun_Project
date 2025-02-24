using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesList;  //장애물리스트

    public GameObject ground;
    public Transform lastObstacle;
    public GameObject curObjstacle;

    private int groundCnt = 0;
    private bool isObstacle = false;

    //Camera cam;   //테스트용 카메라

    private void Awake()
    {
        Obstacle[] curObjstacle = GameObject.FindObjectsOfType<Obstacle>();
    }
    void Start()
    {
        //cam = Camera.main;    //테스트용 카메라
    }

    void Update()
    {
        //cam.transform.position += Vector3.right * 4f * Time.deltaTime;    //테스트용 카메라

        //생성된 Obstacle이 20개 이하일 때만 추가로 생성
        if (curObjstacle.transform.childCount < 20)
        {
            if (!isObstacle && Random.value > 0.5f) // 50% 확률로 장애물 생성
            {
                SpawnObstacle();
            }
            else
            {
                SpawnGround();
            }
        }
    }

    //바닥 생성
    void SpawnGround()
    {
        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);
        GameObject newGround = Instantiate(ground, newPosition, Quaternion.identity);
        newGround.transform.SetParent(curObjstacle.transform);
        lastObstacle = newGround.transform;

        groundCnt++;

        isObstacle = false;
    }

    //장애물 생성
    void SpawnObstacle()
    {
        if (groundCnt < 2) return;

        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);

        int randomIdx = Random.Range(0, obstaclesList.Length);
        GameObject newObstacle = Instantiate(obstaclesList[randomIdx], newPosition, Quaternion.identity);
        newObstacle.transform.SetParent(curObjstacle.transform);
        lastObstacle = newObstacle.transform;

        groundCnt = 0;

        isObstacle = true;
    }
}
