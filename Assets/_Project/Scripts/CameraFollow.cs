using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Цель для слежения (например, игрок)
    public float smoothSpeed = 0.125f; // Скорость смещения камеры

    void FixedUpdate()
    {
        if (target != null && !Player_Controller.instance.dead)
        {
            // Вычисляем новую позицию для камеры с использованием SmoothDamp
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}