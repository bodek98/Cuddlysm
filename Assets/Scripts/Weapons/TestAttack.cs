using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 1;
    private float _nextTimeToAttack = 0;
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        if (Time.time < _nextTimeToAttack) return;
        
        _nextTimeToAttack += _attackDelay;
        _weapon.Attack();
    }
}
