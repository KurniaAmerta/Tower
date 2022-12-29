using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public Transform target;

    public float speed = 1.5f;

    [SerializeField] Animator anim;

    Tower o;

    bool isAttack;

    private void Update()
    {
        if (target && !anim.GetBool("attack"))
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("AI"))
        {
            anim.SetBool("attack", true);

            o = collision.GetComponent<Tower>();
            if (o)
            {
                StartCoroutine(CorAttack());
            }
        }
    }

    IEnumerator CorAttack() {
        var wait = new WaitForSeconds(0.5f);

        while (o && anim.GetBool("attack")) {
            yield return wait;
            o.Damage(5);
        }
    }
}
