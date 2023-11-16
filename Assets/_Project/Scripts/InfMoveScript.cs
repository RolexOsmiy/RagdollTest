using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfMoveScript : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        MoveInDirection(Vector2.right);
    }
    
    private void MoveInDirection(Vector2 direction)
    {
        // Перемещаем объект в определенном направлении
        transform.Translate(direction * speed * Time.deltaTime);

        // Если вы хотите бесконечное перемещение, раскомментируйте следующую строку
        // transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    }
}
