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
        //지형 생성
        //생성된 Obstacle이 20개 이하일 때만 추가로 생성
        if (obstacles.transform.childCount < 20)
        {
            if (!obstacles.isObstacle && Random.value > 0.5f) // 50% 확률로 장애물 생성
            {
                obstacles.SpawnObstacle();
            }
            else
            {
                obstacles.SpawnGround();
            }

            //지형이 생성 됐을 경우에만 아이템 생성되도록 함
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
