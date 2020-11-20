using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float value = 100f;
    [HideInInspector] public UnityEvent onHit;
    public void TakeDamage(float damage)
    {
        value -= damage;
        if(value<0)
        {
            value = 0;
        }

        onHit.Invoke();
    }
    

}
