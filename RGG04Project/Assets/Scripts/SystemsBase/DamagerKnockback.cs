using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerKnockback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Knockback(GameObject ObjectToKnockback)
    {
        Rigidbody2D rb = ObjectToKnockback.GetComponent<Rigidbody2D>();

        if (rb)
        {

           // Vector3 KnockbackDirection = 

        }

    }
}
