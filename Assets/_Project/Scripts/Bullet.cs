using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void OnEnable()
    {
        // Вызываем метод DisableBullet через 3 секунды
        Invoke("DisableBullet", 3f);
    }

    private void OnDisable()
    {
        // Сбрасываем вызов метода DisableBullet при отключении объекта
        CancelInvoke("DisableBullet");
    }

    private void DisableBullet()
    {
        // Отключаем объект
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Env"))
        {
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Target>().Respawn();
        }
        
    }
}
