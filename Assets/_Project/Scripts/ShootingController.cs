using System;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    public static ShootingController instance;
    
    [SerializeField] private Button useButton;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootCooldown = 0.5f;

    [SerializeField] private HingeJoint2D joint;
    [SerializeField] private Transform weaponPos;
    [SerializeField] private Transform weapon;
    [SerializeField] private float getWeaponDistance = 3;

    private float lastShootTime;

    public bool _isArmed = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lastShootTime = Time.time;
        useButton.onClick.AddListener(Use);
        ObjectPool.Instance.SetupPoolObject(bulletPrefab, 20);
    }

    public void Shoot()
    {
        if(Player_Controller.instance.dead)
            return;
        
        // Проверяем, можно ли стрелять
        if (Time.time - lastShootTime > shootCooldown)
        {
            // Получаем объект патрона из объектного пула
            GameObject bullet = ObjectPool.Instance.GetObject("Bullet", bulletPrefab);

            if (bullet != null)
            {
                // Устанавливаем позицию и направление патрона
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;

                // Активируем патрон
                bullet.SetActive(true);
            }

            lastShootTime = Time.time;
        }
    }

    private void Use()
    {
        if(Player_Controller.instance.dead)
            return;
        
        if (_isArmed)
        {
            weapon.parent = null;
            joint.enabled = false;
            _isArmed = false;
        }
        else if(!_isArmed && Vector2.Distance(weapon.position,transform.position) < getWeaponDistance)
        {
            weapon.parent = transform;
            weapon.position = weaponPos.position;
            joint.enabled = true;
            _isArmed = true;
        }
    }
}