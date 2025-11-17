using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform bird;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = new Vector3(bird.position.x, transform.position.y, transform.position.z);
    }
}
