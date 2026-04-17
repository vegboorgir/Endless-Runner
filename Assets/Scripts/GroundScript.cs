using Unity.VisualScripting;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float groundLength = 6f;

    private Transform[] grounds;
    private int lastGroundIndex = 0;
    void Start()
    {
        grounds = new Transform[transform.childCount];
        for (int i = 0; i<transform.childCount; i++)
        {
            grounds[i] = transform.GetChild(i);
        }

    }

    void Update()
    {
        if(player.position.z - grounds[lastGroundIndex].position.z > groundLength)
        {
            int nextIndex = (lastGroundIndex + 1) % grounds.Length;
            grounds[lastGroundIndex].position = grounds[nextIndex].position + new Vector3(0, 0 ,groundLength);
            lastGroundIndex = nextIndex;
        }
    }
}
