using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private float tiltAngle = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        
        Quaternion targetRotation = Quaternion.Euler(tiltAngle, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);

        Vector3 desiredPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
