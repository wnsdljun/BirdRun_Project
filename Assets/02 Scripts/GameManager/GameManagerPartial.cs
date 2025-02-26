using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public string potionType = "Small";
    private int lastSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene();
        //SceneManager.LoadSceneAsync();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((int)player.SurviveTime % 60 == 0 && lastSpawnTime != (int)player.SurviveTime)
        {
            lastSpawnTime = (int)player.SurviveTime;
            potionType = "Large";
            PotionSpawn();
        }
        else if ((int)player.SurviveTime % 15 == 0 && lastSpawnTime != (int)player.SurviveTime)
        {
            lastSpawnTime = (int)player.SurviveTime;
            potionType = "Small";
            PotionSpawn();
        }
        else
            ObstacleSpawn();
    }

    void ObstacleSpawn()
    {
        //���� ����
        //������ Obstacle�� 20�� ������ ���� �߰��� ����
        if (obstacles.transform.childCount < 20)
        {
            if (!obstacles.isObstacle && Random.value > 0.5f) // 50% Ȯ���� ��ֹ� ����
            {
                obstacles.SpawnObstacle();
            }
            else
            {
                obstacles.SpawnGround();
            }

            //������ ���� ���� ��쿡�� ������ �����ǵ��� ��
            if (obstacles.isSpawner)
            {
                obstacles.itemSpawner = obstacles.lastObstacle.GetComponent<ItemSpawner>();
                obstacles.itemSpawner.SpawnItem();
            }
        }
    }

    void PotionSpawn()
    {
        obstacles.SpawnPotion();
        obstacles.itemSpawner = obstacles.lastObstacle.GetComponent<ItemSpawner>();
        obstacles.itemSpawner.SpawnItem();
    }
}
