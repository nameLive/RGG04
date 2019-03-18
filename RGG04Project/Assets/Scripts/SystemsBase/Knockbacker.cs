using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbacker : MonoBehaviour
{
    [SerializeField]
    float pushBackStrengthRight = 500f;
    [SerializeField]
    float pushBackStrengthUp = 500f;

    private void Start()
    {
        Damager damager = GetComponent<Damager>();

        damager.EventOnDidDamage += Knockback;
    }


    public void Knockback(GameObject ObjectToKnockback)
    {
        Rigidbody2D rb = ObjectToKnockback.GetComponentInParent<Rigidbody2D>();

       if (rb)
        {

            Vector3 directionToObject = ObjectToKnockback.transform.position - gameObject.transform.position;

            directionToObject = Vector3.Normalize(directionToObject);
            
            float rightDotPositionOfObject = Vector3.Dot(directionToObject, gameObject.transform.right);

            int right = rightDotPositionOfObject > 0 ? 1 : -1;

            Vector2 knockbackDirection = new Vector2(pushBackStrengthRight * right, pushBackStrengthUp);

            rb.velocity = new Vector2();

            rb.AddForce(knockbackDirection);

        }
       

    }
}
