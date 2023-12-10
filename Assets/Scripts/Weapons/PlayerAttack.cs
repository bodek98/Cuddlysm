using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        _weapon.Attack();
        }
    }
}
