using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Balance : MonoBehaviour
{
    public float targetRotation;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;

    private void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
