using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Target : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CircleCollider2D collider;
    [Range(1, 10)] [SerializeField] private float minScale;
    [Range(1, 10)] [SerializeField] private float maxScale;

    private bool _locker;

    public void Respawn()
    {
        transform.DOShakePosition(0.3f, new Vector3(1, 1, 1));
        if (!_locker)
        {
            _locker = true;
            StartCoroutine(RespawnWithDelay());
        }
        
    }
    
    private IEnumerator RespawnWithDelay()
    {
        var size = Random.Range(minScale, maxScale);
        transform.DOScale(Vector3.zero, 1).OnComplete(() =>
        {
            sr.enabled = false;
            collider.enabled = false;
        });
        
        // Ждем еще N секунд
        yield return new WaitForSeconds(delay);
        // Включаем объект
        sr.enabled = true;
        collider.enabled = true;
        transform.DOScale(new Vector3(size,size), 1).OnComplete(() =>
        {
            _locker = false;
        });
    }
}
