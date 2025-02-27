using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //카메라가 플레이어 쫒아가게
        transform.position = new Vector3(player.transform.position.x + offset.x, offset.y, transform.position.z);
    }
}
