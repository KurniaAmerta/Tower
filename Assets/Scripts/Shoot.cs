using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Tower
{
    [SerializeField] Bullet bullet;

    private void Start()
    {
        StartCoroutine(CorInstantiate());

        IEnumerator CorInstantiate()
        {
            var o = new WaitForSeconds(3f);

            while (true)
            {
                Bullet();
                yield return o;
            }
        }
    }

    public void Bullet()
    {
        if (AiManager.Ins.allTower.Count>0) { 
            var o = Instantiate(bullet, this.transform.position, Quaternion.identity);
            o.target = AiManager.Ins.allTower[0].gameObject.transform;
        }
    }
}
