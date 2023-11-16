using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller instance;
    
    [SerializeField] private FixedJoystick walkJoystick;
    [SerializeField] private CustomFixedJoystick aimJoystick;

    [Space] 
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float positionRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform playerPos;

    [Space]
    public Balance balanceHead;
    public Balance hand_1;
    public Balance hand_2;
    public Balance hand_3;
    public Balance hand_4;

    [Space]
    [SerializeField] private Button jumpBtn;

    private bool isOnGround;
    public bool dead = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = 0; j < colliders.Length; j++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[j]);
            }
        }
        
        jumpBtn.onClick.AddListener(Jump);
    }

    private void Update()
    {
        if (dead)
            return;
        
        if (walkJoystick.Horizontal > 0)
        {
            FlipCharacter(1f);
            rb.AddForce(Vector2.right * playerSpeed);
        }
        else if (walkJoystick.Horizontal < 0)
        {
            FlipCharacter(-1f);
            rb.AddForce(Vector2.left * playerSpeed);
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        
        balanceHead.targetRotation = ConvertTo360Degrees(aimJoystick.Horizontal, aimJoystick.Vertical)-75;
        hand_1.targetRotation = ConvertTo360Degrees(aimJoystick.Horizontal, aimJoystick.Vertical);
        hand_2.targetRotation = ConvertTo360Degrees(aimJoystick.Horizontal, aimJoystick.Vertical);
        hand_3.targetRotation = ConvertTo360Degrees(aimJoystick.Horizontal, aimJoystick.Vertical);
        hand_4.targetRotation = ConvertTo360Degrees(aimJoystick.Horizontal, aimJoystick.Vertical);

        if (rb.velocity.x > 2f || rb.velocity.x < -2f)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    
    // Метод для конвертации horizontal и vertical в градус оборота до 360
    private float ConvertTo360Degrees(float horizontal, float vertical)
    {
        // Вычисляем арктангенс (тангенс обратный) для расчета угла в радианах
        float angleRadians = Mathf.Atan2(vertical, horizontal);

        // Переводим радианы в градусы
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        // Если угол отрицательный, преобразуем его в положительный
        if (angleDegrees < 0)
        {
            angleDegrees += 360f;
        }

        return angleDegrees;
    }
    
    private void FlipCharacter(float direction)
    {
        // Получаем текущий масштаб персонажа
        Vector3 scale = transform.localScale;

        // Меняем знак масштаба по оси X
        scale.x = direction;

        // Применяем новый масштаб
        transform.localScale = scale;
    }

    private void Jump()
    {
        if (isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public void Die()
    {
        DisableJointsInChildren();
        dead = true;
    }
    
    // Вызывается, когда вы хотите отключить все Joint2D в дочерних объектах
    public void DisableJointsInChildren()
    {
        Joint2D[] childJoints = GetComponentsInChildren<Joint2D>();

        foreach (Joint2D joint in childJoints)
        {
            joint.enabled = false;
        }
    }
}
