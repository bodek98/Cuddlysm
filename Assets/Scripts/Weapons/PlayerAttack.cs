using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Weapon _weapon;
    [SerializeField] private bool singleFire = false;
    private bool isMouseButtonPressed = false;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonPressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonPressed = false;
        }

        if (singleFire)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weapon.Attack();
            }
        } else
        {
            if (isMouseButtonPressed)
            {
                _weapon.Attack();
            }
        }
    }
}
