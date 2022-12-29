using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed = 1.5f;

    private void Update()
    {
        if (target) { 
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("AI"))
        {
            var o = collision.GetComponent<Tower>();
            if (o)
            {
                o.Damage(10);
            }
            Destroy(this.gameObject);
        }
    }
}
