using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomFixedJoystick : Joystick
{
    private bool _isHold;
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (ShootingController.instance._isArmed)
        {
            Player_Controller.instance.balanceHead.enabled = false;
            Player_Controller.instance.hand_1.enabled = false;
            Player_Controller.instance.hand_2.enabled = false;
            Player_Controller.instance.hand_3.enabled = false;
            Player_Controller.instance.hand_4.enabled = false;
        }
        _isHold = false;
    }

    private void Update()
    {
        if (_isHold && ShootingController.instance._isArmed)
            ShootingController.instance.Shoot();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _isHold = true;
        OnDrag(eventData);
    }
    
    
    public void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        if (ShootingController.instance._isArmed)
        {
            Player_Controller.instance.balanceHead.enabled = true;
            Player_Controller.instance.hand_1.enabled = true;
            Player_Controller.instance.hand_2.enabled = true;
            Player_Controller.instance.hand_3.enabled = true;
            Player_Controller.instance.hand_4.enabled = true;
        }
    }
}