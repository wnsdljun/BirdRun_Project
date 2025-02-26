using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesList;  //���� ���
    public GameObject itemObstacle;     //������ ����

    public GameObject ground;
    public Transform lastObstacle;

    [HideInInspector] public ItemSpawner itemSpawner;

    private int groundCnt = 0;
    [HideInInspector] public bool isObstacle = false;
    [HideInInspector] public bool isSpawner = true;

    void Update()
    {
    }

    //�ٴ� ����
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

    //��ֹ� ����
    public void SpawnObstacle()
    {
        //��ֹ��� �������� �������� �ʵ��� ���� ó��
        if (groundCnt < 2)
        {
            isSpawner = false;
            return;
        }

        GameObject newObstacle;

        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);

        //10% Ȯ���� ������ ���� ����
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
