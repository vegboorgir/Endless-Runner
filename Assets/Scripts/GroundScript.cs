using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float groundLength = 40f;
    [SerializeField] float regenThreshold = 20f; // Distance from tile end to trigger regen

    private Transform[] grounds;
    private int lastGroundIndex = 0;
    void Start()
    {
        grounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            grounds[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        float distanceToEnd = grounds[lastGroundIndex].position.z + groundLength - player.position.z;

        // Regenerate when player is within regenThreshold of the tile's end
        if (distanceToEnd <= regenThreshold)
        {
            int nextIndex = (lastGroundIndex + 1) % grounds.Length;
            grounds[lastGroundIndex].position = grounds[nextIndex].position + new Vector3(0, 0, groundLength);
            lastGroundIndex = nextIndex;
        }
    }
}
