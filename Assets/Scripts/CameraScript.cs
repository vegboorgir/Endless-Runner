using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] UnityEngine.Vector3 OffSet;
    void LateUpdate()
    {
        transform.position = player.position + OffSet;
    }
}
