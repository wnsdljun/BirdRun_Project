using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesList;  //��ֹ�����Ʈ
    public GameObject itemObstacle;

    public GameObject ground;
    public Transform lastObstacle;

    private ItemSpawner itemSpawner;

    private int groundCnt = 0;
    private bool isObstacle = false;



    private void Awake()
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        //������ Obstacle�� 20�� ������ ���� �߰��� ����
        if (transform.childCount < 20)
        {
            if (!isObstacle && Random.value > 0.5f) // 50% Ȯ���� ��ֹ� ����
            {
                SpawnObstacle();
            }
            else
            {
                SpawnGround();
            }
            itemSpawner = lastObstacle.GetComponent<ItemSpawner>();
            itemSpawner.SpawnItem();
        }
    }

    //�ٴ� ����
    void SpawnGround()
    {
        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);
        GameObject newGround = Instantiate(ground, newPosition, Quaternion.identity);
        newGround.transform.SetParent(transform);
        lastObstacle = newGround.transform;

        groundCnt++;

        isObstacle = false;
    }

    //��ֹ� ����
    void SpawnObstacle()
    {
        if (groundCnt < 2) return;

        GameObject newObstacle;

        Vector3 newPosition = lastObstacle.position + new Vector3(3f, 0, 0);

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
    }
}
