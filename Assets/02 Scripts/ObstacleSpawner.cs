using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesList;  //��ֹ�����Ʈ

    public GameObject ground;
    public Transform lastObstacle;
    public GameObject curObjstacle;

    private int groundCnt = 0;
    private bool isObstacle = false;

    //Camera cam;   //�׽�Ʈ�� ī�޶�

    private void Awake()
    {
        Obstacle[] curObjstacle = GameObject.FindObjectsOfType<Obstacle>();
    }
    void Start()
    {
        //cam = Camera.main;    //�׽�Ʈ�� ī�޶�
    }

    void Update()
    {
        //cam.transform.position += Vector3.right * 4f * Time.deltaTime;    //�׽�Ʈ�� ī�޶�

        //������ Obstacle�� 20�� ������ ���� �߰��� ����
        if (curObjstacle.transform.childCount < 20)
        {
            if (!isObstacle && Random.value > 0.5f) // 50% Ȯ���� ��ֹ� ����
            {
                SpawnObstacle();
            }
            else
            {
                SpawnGround();
            }
        }
    }

    //�ٴ� ����
    void SpawnGround()
    {
        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);
        GameObject newGround = Instantiate(ground, newPosition, Quaternion.identity);
        newGround.transform.SetParent(curObjstacle.transform);
        lastObstacle = newGround.transform;

        groundCnt++;

        isObstacle = false;
    }

    //��ֹ� ����
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
