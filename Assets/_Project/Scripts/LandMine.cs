using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public float explosionForce = 100f; // Сила взрыва
    public float explosionRadius = 5f; // Радиус взрыва
    public float destroyDelay = 0.5f; // Задержка перед уничтожением объекта

    void Explode()
    {
        // Найти все коллайдеры в радиусе взрыва
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            // Проверка, есть ли Rigidbody2D на объекте
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Применение силы взрыва к Rigidbody2D
                Vector2 explosionDirection = (rb.transform.position - transform.position).normalized;
                rb.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Отложенное уничтожение объекта
        Destroy(gameObject, destroyDelay);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Controller.instance.Die();
            Explode();
            gameObject.SetActive(false);
        }
    }
}
