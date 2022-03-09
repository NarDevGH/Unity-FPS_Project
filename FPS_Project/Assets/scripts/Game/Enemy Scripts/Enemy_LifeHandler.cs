using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LifeHandler : MonoBehaviour,IDamageable
{
    private float _health = 100f;

    public void Damage(float ammount)
    {
        _health -= ammount;
        print(_health);

        if (_health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
