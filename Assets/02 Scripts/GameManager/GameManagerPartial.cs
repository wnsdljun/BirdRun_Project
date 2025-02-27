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

            //15�ʸ��� ���� ȸ�� ������ �����ϰ� 60�ʿ��� ū ȸ�� ������ ����
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
                //�Ϲ� ���� ����
                ObstacleSpawn();

            _UIManager.ChangePlayerHp(player.HPPercent);
        }
    }

    //���� ����
    void ObstacleSpawn()
    {
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

    //���� �����ϴ� ���� ����
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
