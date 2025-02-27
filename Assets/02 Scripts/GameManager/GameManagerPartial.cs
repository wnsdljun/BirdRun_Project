using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public string potionType = "Small";
    private int lastSpawnTime = 0;
    private int totalScore = 0;
    private int fruitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene();
        //SceneManager.LoadSceneAsync();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            player.SurviveTime += Time.fixedDeltaTime;
            playTime += Time.fixedDeltaTime;

            //15초마다 작은 회복 물약이 등장하고 60초에는 큰 회복 물약이 등장
            if ((int)playTime % 60 == 0 && lastSpawnTime != (int)playTime)
            {
                lastSpawnTime = (int)playTime;
                potionType = "Large";
                PotionSpawn();
            }
            else if ((int)playTime % 15 == 0 && lastSpawnTime != (int)playTime)
            {
                lastSpawnTime = (int)playTime;
                potionType = "Small";
                PotionSpawn();

                player.AddMoveSpeed = 2f;
                _UIManager.DisplaySpeedUpText();
            }
            else
                //일반 지형 생성
                ObstacleSpawn();

            _UIManager.ChangePlayerHp(player.HPPercent);
        }
    }

    //지형 생성
    void ObstacleSpawn()
    {
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

    //포션 등장하는 지형 생성
    void PotionSpawn()
    {
        obstacles.SpawnPotion();
        obstacles.itemSpawner = obstacles.lastObstacle.GetComponent<ItemSpawner>();
        obstacles.itemSpawner.SpawnItem();
    }

    public void AddScore(int score)
    {
        totalScore += score;
        _UIManager.ChangeScore(totalScore);
    }

    public void AddFruitCount()
    {
        fruitCount++;
        _UIManager.ChangeFruit(fruitCount);
    }

    public void ResetGameManager()
    {
        playTime = 0f;
        lastSpawnTime = 0;
        totalScore = 0;
        fruitCount = 0;
    }
}
